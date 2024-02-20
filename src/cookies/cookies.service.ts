import { BadRequestException, forwardRef, HttpStatus, Inject, Injectable } from '@nestjs/common';
import { InjectRepository } from "@nestjs/typeorm";
import { Repository } from "typeorm";
import { REQUEST } from "@nestjs/core";
import { CookieEntity, CookieSource } from "./entities/cookie.entity";
import { IPaginationOptions, Pagination, paginate } from "nestjs-typeorm-paginate";
import { UpdateCookieDto } from "./entities/update-cookie.dto";
import { ImportCookieDto } from "./entities/import-cookie.dto";
import { UserService } from "../user/user.service";
import { isValidCookie, isValidIP } from "../base/const/Utils";

const readXlsxFile = require("read-excel-file/node");
const fs = require("fs");

@Injectable()
export class CookiesService {
    constructor(
        @InjectRepository(CookieEntity)
        private cookieEntityRepository: Repository<CookieEntity>,
        @Inject(REQUEST)
        private request: any,
        @Inject(forwardRef(() => UserService))
        private userService: UserService,
    ) {
    }

    /**
     * Lấy danh sách gói đầu tư của người dùng
     * @param useStatus
     * @param fromDateString
     * @param toDateString
     * @param options
     */
    public async paginate(useStatus: number, fromDateString: string, toDateString: string, options: IPaginationOptions): Promise<Pagination<CookieEntity>> {
        if (options.page === 0) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Sai thông tin số trang'
            });
        }

        const queryBuilder = this.cookieEntityRepository
            .createQueryBuilder('cookie')
            .where('cookie.isDelete = 0')

        if (useStatus || useStatus === 0) {
            queryBuilder.andWhere('cookie.useStatus = :useStatus', { useStatus })
        }

        if (fromDateString && toDateString) {
            let fromDate = new Date(fromDateString);
            let toDate = new Date(toDateString);
            toDate.setDate(toDate.getDate() + 1);

            queryBuilder.andWhere('cookie.createdAt <= :toDate AND cookie.createdAt >= :fromDate', {
                fromDate,
                toDate
            })
        }

        const getRole = await this.userService.getRoles();

        const isSuperAdmin = getRole.find(x => x.roleKey == 'SuperAdmin');
        if (!isSuperAdmin) {

            const isAdmin = getRole.find(x => x.roleKey == 'Admin');
            if (!isAdmin) {
                queryBuilder.andWhere('cookie.userId = :userId', { userId: this.request.user.id });
            }

            queryBuilder.andWhere('cookie.team = :team', { team: this.request.user.team });
        }

        queryBuilder.orderBy('cookie.createdAt', 'DESC');

        return paginate<CookieEntity>(queryBuilder, options)
    }

    /**
     * Khởi tạo giá trị ban đầu cho API
     */
    public initData() {
        const initData = [];

        const listAdd: CookieEntity[] = initData.map(x => {
            const cookie = new CookieEntity();
            cookie.ipAddress = '::1';
            cookie.cookie = x.users.cookie;
            cookie.actId = x.id;
            cookie.fbUserId = x.users.data[0].id;
            cookie.fbUserName = x.name;
            cookie.adTrustDsl = x.adtrust_dsl;
            cookie.amountSpent = Number(x.amount_spent);
            return cookie;
        });

        return {
            success: true,
            data: this.cookieEntityRepository.save(listAdd)
        };
    }

    /**
     * Cập nhật lại trạng thái hoạt động của Cookie
     * @param updateCookieDto
     */
    public async changeCookieStatus(updateCookieDto: UpdateCookieDto) {
        const cookieInfo = await this.cookieEntityRepository.findOne(updateCookieDto.id);
        if (!cookieInfo) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin cookie'
            });
        }

        cookieInfo.useStatus = updateCookieDto.useStatus;
        cookieInfo.runningStatus = updateCookieDto.runningStatus;

        if (updateCookieDto.licenceKey) {
            cookieInfo.licenceKey = updateCookieDto.licenceKey;
        }

        await this.cookieEntityRepository.save(cookieInfo);
        return {
            success: true,
            message: 'Cập nhật trạng thái thành công.'
        }
    }

    public async uploadCookie(files: { cookie?: Express.Multer.File[] }) {
        const message = {
            success: true,
            data: []
        }

        // Kiểm tra xem có upload được file lên không
        if (files.cookie.length == 0) {
            return message;
        }

        // Lấy đường dẫn file exel vừa upload
        const filePath = files.cookie[0].destination + '/' + files.cookie[0].filename;

        // Chuyển đổi file excel thành JSON
        const rawExcelData = await readXlsxFile(filePath, {
            sheet: 1,
            trim: true,
            includeNullValues: true,
        });

        // Bỏ qua row Header
        rawExcelData.shift();

        const importExcelRowData: ImportCookieDto[] = [];

        for (const excelRow of rawExcelData) {
            const [ ipAddress, cookie ] = excelRow;

            const importCookieDto = new ImportCookieDto();
            importCookieDto.ipAddress = ipAddress;
            importCookieDto.cookie = cookie;
            importCookieDto.message = '';
            importCookieDto.success = true;

            if (!ipAddress) {
                importCookieDto.success = false;
                importCookieDto.message += 'Địa chỉ Ip chưa được nhập.\n';
            }

            if (!cookie) {
                importCookieDto.success = false;
                importCookieDto.message += 'Cookie chưa được nhập.\n';
            }

            if (ipAddress && !isValidIP(ipAddress)) {
                importCookieDto.success = false;
                importCookieDto.message += 'Vui lòng nhập đúng địa chỉ IP. Ví dụ: 192.168.1.1.\n';
            }

            if (cookie && !isValidCookie(cookie)) {
                importCookieDto.success = false;
                importCookieDto.message += 'Đây không phải Cookie Facebook. Cookie phải bao gồm \'datr\', \'sb\', \'c_user\', \'xs\', \'fr\'.\n';
            }

            importExcelRowData.push(importCookieDto);

            // Kiểm tra cookie đã tồn tại hay chưa
            /*const existCookie = await this.userService.checkExistCookie(cookie);
            if (existCookie) {
                importCookieDto.success = false;
                importCookieDto.message += 'Cookie đã tồn tại trong hệ thống.';
            }*/

            // Nếu lỗi thì tiếp tục, không lỗi thì thực hiện lưu vào DB
            if (!importCookieDto.success) {
                continue;
            }

            // Lưu cookie trong trường hợp dòng dữ liệu của cookie hợp lệ
            const cookieEntity = new CookieEntity();
            cookieEntity.ipAddress = ipAddress;
            cookieEntity.cookie = cookie;
            cookieEntity.createdBy = this.request.user.id;
            cookieEntity.updateBy = this.request.user.id;
            cookieEntity.userId = this.request.user.id;
            cookieEntity.source = CookieSource.IMPORT;
            cookieEntity.team = this.request.user.team;
            cookieEntity.domain = 'facebook.com';
            await this.cookieEntityRepository.save(cookieEntity);
        }

        // Xóa file đã xử lý
        fs.unlinkSync(filePath)

        // Trả về dữ liệu
        message.data = importExcelRowData;
        return message;
    }

    /**
     * Lưu cookie
     * @param cookie
     */
    public saveCookie(cookie: any) {
        return this.cookieEntityRepository.save(cookie);
    }

    /**
     * Xóa cookie
     * @param cookie
     */
    public async delete(cookie: CookieEntity) {
        const queryBuilder = this.cookieEntityRepository.createQueryBuilder('cookie')
            .where('cookie.id = :id', { id: cookie.id });

        const getRole = await this.userService.getRoles();
        const isSuperAdmin = getRole.find(x => x.roleKey == 'SuperAdmin');
        if (!isSuperAdmin) {

            const isAdmin = getRole.find(x => x.roleKey == 'Admin');
            if (!isAdmin) {
                queryBuilder.andWhere('cookie.userId = :userId', { userId: this.request.user.id });
            }

            queryBuilder.andWhere('cookie.team = :team', { team: this.request.user.team });
        }

        const cookieInfo = await queryBuilder.getOne();

        if (!cookieInfo) {
            throw new BadRequestException({
                error: "You don't have permission to delete this cookie."
            });
        }

        cookieInfo.isDelete = true;
        cookieInfo.updateAt = new Date();

        return this.cookieEntityRepository.save(cookieInfo);
    }
}
