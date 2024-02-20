import { BadRequestException, HttpStatus, Injectable, Logger } from '@nestjs/common';
import { InjectRepository } from "@nestjs/typeorm";
import { WebsocketConnection } from "../websocket/websocket.entity";
import { Repository } from "typeorm";
import { CookieEntity } from "../cookies/entities/cookie.entity";
import { UserAdsEntity } from "../user-ads/entities/user-ads.entity";
import * as moment from 'moment';

@Injectable()
export class DashboardService {
    private readonly logger = new Logger(DashboardService.name);

    constructor(
        @InjectRepository(WebsocketConnection)
        private websocketConnectionRepository: Repository<WebsocketConnection>,
        @InjectRepository(CookieEntity)
        private cookieEntityRepository: Repository<CookieEntity>,
        @InjectRepository(UserAdsEntity)
        private userAdsEntityRepository: Repository<UserAdsEntity>,
    ) {
    }

    public async getDashboardInfo(startDate: string, endDate: string) {
        if (!startDate || !endDate) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                error: 'Sai thông tin ngày tháng.'
            });
        }

        const fromDate: Date = new Date(startDate);
        const toDate: Date = new Date(endDate);

        return [
            await this.periodActiveUser(fromDate, toDate),
            await this.periodCookie(fromDate, toDate),
            await this.periodUserAds(fromDate, toDate),
        ];
    }

    /**
     * Cùng kì: Số lượng tài khoản quảng cáo được thêm
     * @param fromDate
     * @param toDate
     * @private
     */
    public async periodUserAds(fromDate, toDate) {
        const periodDateDiff = moment(fromDate).diff(moment(toDate), 'days');
        const periodDateFrom = moment(fromDate).add(periodDateDiff, 'd');
        const periodDateTo = fromDate;

        // User hoạt động
        const current = await this.userAdsEntityRepository
            .createQueryBuilder('users')
            .where('1 = 1')
            .andWhere('users.createdAt <= :toDate AND users.createdAt >= :fromDate', {
                fromDate,
                toDate
            })
            .getMany();

        const period = await this.userAdsEntityRepository
            .createQueryBuilder('users')
            .where('1 = 1')
            .andWhere('users.createdAt <= :periodDateTo AND users.createdAt >= :periodDateFrom', {
                periodDateFrom: periodDateFrom.toDate(),
                periodDateTo
            })
            .getMany();

        const periodSum = Math.abs(period.length - current.length);
        const percent = (periodSum > 0 && period.length > 0 ? periodSum / period.length : 0) * 100;

        return {
            title: 'Tài khoản Ads',
            value: current.length,
            isGrow: current.length >= period.length,
            percent,
            color: 'success'
        }
    }

    /**
     * Cùng kì: User số lượng cookie được thêm
     * @param fromDate
     * @param toDate
     * @private
     */
    public async periodCookie(fromDate, toDate) {
        const periodDateDiff = moment(fromDate).diff(moment(toDate), 'days');
        const periodDateFrom = moment(fromDate).add(periodDateDiff, 'd');
        const periodDateTo = fromDate;

        // User hoạt động
        const current = await this.cookieEntityRepository
            .createQueryBuilder('cookie')
            .where('1 = 1')
            .andWhere('cookie.createdAt <= :toDate AND cookie.createdAt >= :fromDate', {
                fromDate,
                toDate
            })
            .getMany();

        const period = await this.cookieEntityRepository
            .createQueryBuilder('cookie')
            .where('1 = 1')
            .andWhere('cookie.createdAt <= :periodDateTo AND cookie.createdAt >= :periodDateFrom', {
                periodDateFrom: periodDateFrom.toDate(),
                periodDateTo
            })
            .getMany();

        const periodSum = Math.abs(period.length - current.length);
        const percent = (periodSum > 0 && period.length > 0 ? periodSum / period.length : 0) * 100;

        return {
            title: 'Số lượng Cookie',
            value: current.length,
            isGrow: current.length >= period.length,
            percent,
            color: 'info'
        }
    }

    /**
     * Cùng kì: User hoạt động
     * @param fromDate
     * @param toDate
     * @private
     */
    public async periodActiveUser(fromDate, toDate) {
        const periodDateDiff = moment(fromDate).diff(moment(toDate), 'days');
        const periodDateFrom = moment(fromDate).add(periodDateDiff, 'd');
        const periodDateTo = fromDate;

        // User hoạt động
        const current = await this.websocketConnectionRepository
            .createQueryBuilder('websocket')
            .where('1 = 1')
            .andWhere('websocket.createdAt <= :toDate AND websocket.createdAt >= :fromDate', {
                fromDate,
                toDate
            })
            .andWhere('websocket.isDisconnect = false')
            .groupBy('websocket.userId')
            .getMany();

        const period = await this.websocketConnectionRepository
            .createQueryBuilder('websocket')
            .where('1 = 1')
            .andWhere('websocket.createdAt <= :periodDateTo AND websocket.createdAt >= :periodDateFrom', {
                periodDateFrom: periodDateFrom.toDate(),
                periodDateTo
            })
            .andWhere('websocket.isDisconnect = false')
            .groupBy('websocket.userId')
            .getMany();

        const periodSum = Math.abs(period.length - current.length);
        const percent = (periodSum > 0 && period.length > 0 ? periodSum / period.length : 0) * 100;

        return {
            title: 'User hoạt động',
            value: current.length,
            isGrow: current.length >= period.length,
            percent,
            color: 'warning'
        }
    }
}
