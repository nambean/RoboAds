import { BadRequestException, HttpStatus, Inject, Injectable } from '@nestjs/common';
import { InjectRepository } from "@nestjs/typeorm";
import { Repository } from "typeorm";
import { UserAdsEntity } from "./entities/user-ads.entity";
import { REQUEST } from "@nestjs/core";
import { AdsCampaignDto } from "./entities/ads-campaign.dto";
import { AdsCampaign } from "./entities/ads.campaign";
import { AdsCampaignHistory } from "./entities/ads-campaign.history";
import { MessageConstant } from "../base/const/MessageConstant";
import { IPaginationOptions, paginate, Pagination } from "nestjs-typeorm-paginate";
import { getMessage } from "../base/const/FacebookMessage";
import { from, map } from "rxjs";
import { UserService } from "../user/user.service";

@Injectable()
export class UserAdsService {
    // private readonly logger = new Logger(UserAdsService.name);
    constructor(
        @InjectRepository(UserAdsEntity)
        private userAdsEntityRepository: Repository<UserAdsEntity>,
        @InjectRepository(AdsCampaign)
        private adsCampaignRepository: Repository<AdsCampaign>,
        @InjectRepository(AdsCampaignHistory)
        private adsCampaignHistoryRepository: Repository<AdsCampaignHistory>,
        @Inject(REQUEST)
        private request: any,
        private userService: UserService,
    ) {
    }

    /**
     * Lưu thông tin tài khoản ads theo cookie
     * @param userAds
     */
    public async sync(userAds: UserAdsDto[]) {
        const userAdsEntityList: UserAdsEntity[] = [];

        for (const userAd of userAds) {
            const existUserAds = await this.userAdsEntityRepository.findOne({
                where: {
                    // cookieId: userAd.cookie_id,
                    accountId: userAd.account_id,
                    actId: userAd.id
                }
            });

            let userAdsEntity = existUserAds ? existUserAds : new UserAdsEntity();

            userAdsEntity.cookieId = userAd.cookie_id;
            userAdsEntity.accountName = userAd.name;
            userAdsEntity.accountStatus = userAd.account_status;
            userAdsEntity.accountId = userAd.account_id;
            userAdsEntity.createTime = new Date(userAd.created_time);
            userAdsEntity.currency = userAd.currency;
            userAdsEntity.thresholdAmount = userAd.adspaymentcycle && userAd.adspaymentcycle.data.reduce((x1, x2) => {
                return x1 + Number(x2.threshold_amount);
            }, 0);
            userAdsEntity.adTrustDsl = userAd.adtrust_dsl;
            userAdsEntity.balance = Number(userAd.balance);
            userAdsEntity.amount = Number(userAd.total_prepay_balance.amount);
            userAdsEntity.amountInHundredths = Number(userAd.total_prepay_balance.amount_in_hundredths);
            userAdsEntity.offsetAmount = Number(userAd.total_prepay_balance.offsetted_amount);
            userAdsEntity.isPrepayAccount = userAd.is_prepay_account;
            userAdsEntity.spend = userAd.insights && userAd.insights.data.reduce((x1, x2) => {
                return x1 + Number(x2.spend);
            }, 0);

            userAdsEntity.owner = userAd.owner;
            userAdsEntity.ownerBusinessId = userAd.owner_business && userAd.owner_business.id;
            userAdsEntity.ownerBusinessName = userAd.owner_business && userAd.owner_business.name;

            const userRole = userAd.user_roles.data.find(x => x.role);
            userAdsEntity.userRoles = userRole ? userRole.role : null;
            userAdsEntity.userPermissions = JSON.stringify(userAd.userpermissions.data);

            userAdsEntity.nextBillDate = new Date(userAd.next_bill_date);
            userAdsEntity.timezoneName = userAd.timezone_name;
            userAdsEntity.timezoneOffsetHoursUtc = userAd.timezone_offset_hours_utc;

            userAdsEntity.disableReason = userAd.disable_reason;
            userAdsEntity.businessCountryCode = userAd.business_country_code;
            userAdsEntity.spendCap = Number(userAd.spend_cap);
            userAdsEntity.amountSpent = Number(userAd.amount_spent);

            if (userAd.all_payment_methods && userAd.all_payment_methods.payment_method_tokens) {
                userAdsEntity.paymentMethodTokens = JSON.stringify(this.removeDuplicates(userAd.all_payment_methods.payment_method_tokens.data, 'type'));
            }

            if (userAd.all_payment_methods && userAd.all_payment_methods.pm_credit_card) {
                userAdsEntity.pmCreditCard = JSON.stringify(userAd.all_payment_methods.pm_credit_card.data);
            }

            if (userAd.all_payment_methods && userAd.all_payment_methods.payment_method_direct_debits) {
                userAdsEntity.paymentMethodDirectDebits = JSON.stringify(userAd.all_payment_methods.payment_method_direct_debits.data);
            }

            if (userAd.all_payment_methods && userAd.all_payment_methods.payment_method_paypal) {
                userAdsEntity.paymentMethodPaypal = JSON.stringify(userAd.all_payment_methods.payment_method_paypal.data);
            }

            userAdsEntity.actId = userAd.id;
            if (!existUserAds) {
                userAdsEntity.createdBy = this.request.user.id;
            }
            userAdsEntity.userId = this.request.user.id;
            userAdsEntity.updateBy = this.request.user.id;
            userAdsEntity.updateAt = new Date();
            userAdsEntity.team = this.request.user.team;

            userAdsEntityList.push(userAdsEntity);
        }

        if (userAdsEntityList.length > 0) {
            await this.userAdsEntityRepository.save(userAdsEntityList);
        }

        return {
            success: true,
            message: MessageConstant.MSG_006
        }
    }

    public removeDuplicates(arr: any[], prop: string) {
        let newArray = [];
        let lookupObject = {};

        for (let i in arr) {
            lookupObject[arr[i][prop]] = arr[i];
        }

        for (let i in lookupObject) {
            newArray.push(lookupObject[i]);
        }

        return newArray;
    }

    /**
     * Lưu thông tin campaign và lịch sử của campaign
     * @param adsCampaignDto
     */
    public async adsCampaignSync(adsCampaignDto: AdsCampaignDto[]) {
        const adsCampaignsList: AdsCampaign[] = [];

        for (const campaign of adsCampaignDto) {
            const existAdsCampaign = await this.adsCampaignRepository.findOne({
                where: {
                    cookieId: campaign.cookieId,
                    accountId: campaign.accountId,
                    actAccountId: campaign.actAccountId,
                    campaignId: campaign.campaignId,
                }
            });

            let adsCampaign = existAdsCampaign ? existAdsCampaign : new AdsCampaign();
            adsCampaign.cookieId = campaign.cookieId;
            adsCampaign.accountId = campaign.accountId;
            adsCampaign.actAccountId = campaign.actAccountId;
            adsCampaign.campaignId = campaign.campaignId;
            adsCampaign.name = campaign.name;
            adsCampaign.objective = campaign.objective;
            adsCampaign.dateStart = new Date(campaign.dateStart);
            adsCampaign.dateStop = new Date(campaign.dateStop);
            adsCampaign.attributionSetting = campaign.attributionSetting;
            adsCampaign.reach = Number(campaign.reach);
            adsCampaign.frequency = Number(campaign.frequency);
            adsCampaign.spend = Number(campaign.spend);
            adsCampaign.costPerImpressions = Number(campaign.costPerImpressions);
            adsCampaign.costPerLinkClick = Number(campaign.costPerLinkClick);
            adsCampaign.linkClickThroughRate = Number(campaign.linkClickThroughRate);
            adsCampaign.linkClick = Number(campaign.linkClick);
            adsCampaign.clicksAll = Number(campaign.clicksAll);
            adsCampaign.ctrAll = Number(campaign.ctrAll);
            adsCampaign.cpcAll = Number(campaign.cpcAll);
            adsCampaign.resultIndicator = campaign.resultIndicator;
            adsCampaign.results = Number(campaign.results);
            adsCampaign.costPerResult = Number(campaign.costPerResult);
            if (!existAdsCampaign) {
                adsCampaign.createdBy = this.request.user.id;
            }
            adsCampaign.userId = this.request.user.id;
            adsCampaign.updateBy = this.request.user.id;
            adsCampaign.updateAt = new Date();
            adsCampaign.team = this.request.user.team;

            adsCampaignsList.push(adsCampaign);
        }

        if (adsCampaignsList.length > 0) {
            await this.adsCampaignRepository.save(adsCampaignsList);
        }

        const adCampaignHistory = adsCampaignsList.map(x => {
            delete x.id;

            x.createdAt = x.updateAt = new Date();
            return x;
        });

        if (adsCampaignsList.length > 0) {
            await this.adsCampaignHistoryRepository.save(adCampaignHistory);
        }

        return {
            success: true,
            message: MessageConstant.MSG_006
        }
    }

    /**
     * Tìm tài khoản Ads theo cookie
     * @param searchParam
     * @param options
     */
    public async getAdsAccount(searchParam: { search: string, fromDate: string; toDate: string; cookieId: number }, options: IPaginationOptions) {
        if (options.page === 0) {
            /*throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Sai thông tin số trang'
            });*/
            options.page = 1;
            options.limit = 10000;
        }

        if (!searchParam.cookieId) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin.'
            });
        }

        const queryBuilder = this.userAdsEntityRepository
            .createQueryBuilder('userAds')
            .where('1 = 1')

        queryBuilder.andWhere('userAds.cookieId = :cookieId', { cookieId: Number(searchParam.cookieId) });

        const search = `%${searchParam.search}%`;
        if(searchParam.search && searchParam.search.length > 0) {
            queryBuilder.andWhere('userAds.accountId LIKE :search OR userAds.accountName LIKE :search', { search });
        }

        if (searchParam.fromDate && searchParam.toDate) {
            let fromDate = new Date(searchParam.fromDate);
            let toDate = new Date(searchParam.toDate);
            toDate.setDate(toDate.getDate() + 1);

            queryBuilder.andWhere('userAds.createdAt <= :toDate AND userAds.createdAt >= :fromDate', {
                fromDate,
                toDate
            });
        }

        const getRole = await this.userService.getRoles();
        const isSuperAdmin = getRole.find(x => x.roleKey == 'SuperAdmin');
        if (!isSuperAdmin) {

            const isAdmin = getRole.find(x => x.roleKey == 'Admin');
            if (!isAdmin) {
                queryBuilder.andWhere('userAds.userId = :userId', { userId: this.request.user.id });
            }

            queryBuilder.andWhere('userAds.team = :team', { team: this.request.user.team });
        }

        // queryBuilder.orderBy('userAds.createdAt', 'DESC');

        return paginate<UserAdsEntity>(queryBuilder, options)
    }

    /**
     * Tìm camp theo cookie id và account id
     * @param searchParam
     */
    public async getCampaign(searchParam: { accountId: string, fromDate: string; toDate: string, cookieId: number, campaignIds: string }) {
        if (!searchParam.cookieId || !searchParam.accountId) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin.'
            });
        }

        const queryBuilder = this.adsCampaignRepository
            .createQueryBuilder('campaign')
            .where('1 = 1')

        queryBuilder.andWhere('campaign.cookieId = :cookieId', { cookieId: Number(searchParam.cookieId) });
        queryBuilder.andWhere('campaign.accountId = :accountId', { accountId: searchParam.accountId });

        const campaignList = searchParam.campaignIds.split(',');

        if (searchParam.campaignIds && searchParam.campaignIds.length > 0 && campaignList.length > 0) {
            queryBuilder.andWhere('campaign.id IN (:campaignIds)', { campaignIds: campaignList })
        }

        if (searchParam.fromDate && searchParam.toDate) {
            let fromDate = new Date(searchParam.fromDate);
            let toDate = new Date(searchParam.toDate);
            toDate.setDate(toDate.getDate() + 1);

            queryBuilder.andWhere('campaign.createdAt <= :toDate AND campaign.createdAt >= :fromDate', {
                fromDate,
                toDate
            });
        }

        const getRole = await this.userService.getRoles();
        const isSuperAdmin = getRole.find(x => x.roleKey == 'SuperAdmin');
        if (!isSuperAdmin) {

            const isAdmin = getRole.find(x => x.roleKey == 'Admin');
            if (!isAdmin) {
                queryBuilder.andWhere('campaign.userId = :userId', { userId: this.request.user.id });
            }

            queryBuilder.andWhere('campaign.team = :team', { team: this.request.user.team });
        }

        queryBuilder.orderBy('campaign.createdAt', 'DESC');

        const dataCampaign = await queryBuilder.getMany();
        const facebookMessage = getMessage();

        dataCampaign.forEach(x => {
            x.resultIndicatorStr = facebookMessage[x.resultIndicator];
        });

        return dataCampaign;
    }

    /**
     * Tìm lịch sử campaign theo cookie id, campaign id
     * @param searchParam
     * @param options
     */
    public async getCampaignHistory(searchParam: { fromDate: string; toDate: string; cookieId: number, campaignId: string }, options: IPaginationOptions) {
        if (options.page === 0) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Sai thông tin số trang'
            });
        }

        if (!searchParam.cookieId) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin.'
            });
        }

        if (!searchParam.campaignId) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin.'
            });
        }

        const queryBuilder = this.adsCampaignHistoryRepository
            .createQueryBuilder('campaignHis')
            .where('1 = 1')

        queryBuilder.andWhere('campaignHis.cookieId = :cookieId', { cookieId: Number(searchParam.cookieId) });
        queryBuilder.andWhere('campaignHis.campaignId = :campaignId', { campaignId: searchParam.campaignId });

        if (searchParam.fromDate && searchParam.toDate) {
            let fromDate = new Date(searchParam.fromDate);
            let toDate = new Date(searchParam.toDate);
            toDate.setDate(toDate.getDate() + 1);

            queryBuilder.andWhere('campaignHis.createdAt <= :toDate AND campaignHis.createdAt >= :fromDate', {
                fromDate,
                toDate
            });
        }

        const getRole = await this.userService.getRoles();
        const isSuperAdmin = getRole.find(x => x.roleKey == 'SuperAdmin');
        if (!isSuperAdmin) {

            const isAdmin = getRole.find(x => x.roleKey == 'Admin');
            if (!isAdmin) {
                queryBuilder.andWhere('campaignHis.userId = :userId', { userId: this.request.user.id });
            }

            queryBuilder.andWhere('campaignHis.team = :team', { team: this.request.user.team });
        }

        queryBuilder.orderBy('campaignHis.createdAt', 'DESC');

        const facebookMessage = getMessage();

        return from(paginate<AdsCampaignHistory>(queryBuilder, options)).pipe(
            map((campaign: Pagination<AdsCampaignHistory>) => {
                campaign.items.forEach(x => {
                    x.resultIndicatorStr = facebookMessage[x.resultIndicator];
                });

                return campaign;
            })
        );
    }

    public allCampaign(searchParam: { cookieId: number, accountId: string }) {
        return this.adsCampaignRepository
            .createQueryBuilder('campaign')
            .select('campaign.id')
            .addSelect('campaign.name')
            .where('1 = 1')
            .andWhere('campaign.cookieId = :cookieId', { cookieId: Number(searchParam.cookieId) })
            .andWhere('campaign.accountId = :accountId', { accountId: searchParam.accountId })
            .getMany();
    }
}
