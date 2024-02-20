import { Injectable } from '@nestjs/common';
import { lastValueFrom, map } from "rxjs";
import { HttpService } from "@nestjs/axios";

@Injectable()
export class FacebookService {
    constructor(
        private httpService: HttpService,
    ) {
    }


    public async getLimit(cookie: string) {
        const config = {
            headers: {
                'Cookie': cookie,
            }
        };

        const getFbAdsPage = await lastValueFrom(
            this.httpService.get(`https://business.facebook.com/ads/manager/account_settings/`, config).pipe(
                map((response) => {
                    return response.data;
                }),
            ),
        );

        const accountId = this.getAccountId(getFbAdsPage);
        const accessToken = this.getAccessToken(getFbAdsPage);

        if (!accountId || !accessToken) {
            return {
                success: false,
                message: 'Không tìm thấy thông tin FB Ads từ Cookies'
            }
        }

        const urlRequest = `https://graph.facebook.com/v12.0/act_${accountId}?fields=adspaymentcycle,currency,name,adtrust_dsl,amount_spent,is_personal,user_role,users{id,is_active,name,permissions,role,roles}&access_token=${accessToken}`

        const facebookAdLimit = await lastValueFrom(
            this.httpService.get(urlRequest, config).pipe(
                map((response) => {
                    return response.data;
                }),
            ),
        );

        return {
            success: true,
            data: facebookAdLimit,
            accessToken
        };
    }

    public getAccountId(html: string): string {
        const checkData = /accountID:"([^"]+)"/gm.exec(html);
        if (!checkData || checkData.length < 1) return '';
        return checkData[1];
    }

    public getAccessToken(html: string): string {
        const checkData = /access_token:"([^"]+)"/gm.exec(html);
        if (!checkData || checkData.length < 1) return '';
        return checkData[1];
    }

    /*public async getFacebookToken(cookie: string) {
        cookie = "sb=R8zsXdhx2LbSZW2HveuCGxHl; m_pixel_ratio=2; x-referer=eyJyIjoiL3N0b3J5LnBocD9zdG9yeV9mYmlkPXBmYmlkMDJIQndWSEZ1RDJlY1pSNkVweHg4SEtSMm13MzFnYUI4SnNmNFlzZGZTblBGNno3UmdxbVdjZlByemRaeWptQzlvbCZpZD0xNTIxODIyMTE4MTIxOTUzJmVhdj1BZmFBeThuOWZyenpxRERyVG4tWk1JVUpjbTdQNXNYbllxMjB6RlBBZ19QZ1pod09qX2p5X1NSeVlVYjd4ZG1nR3k0Jm1fZW50c3RyZWFtX3NvdXJjZT1mZWVkX21vYmlsZSZhbmNob3JfY29tcG9zZXI9ZmFsc2UmcGFpcHY9MCIsImgiOiIvc3RvcnkucGhwP3N0b3J5X2ZiaWQ9cGZiaWQwMkhCd1ZIRnVEMmVjWlI2RXB4eDhIS1IybXczMWdhQjhKc2Y0WXNkZlNuUEY2ejdSZ3FtV2NmUHJ6ZFp5am1DOW9sJmlkPTE1MjE4MjIxMTgxMjE5NTMmZWF2PUFmYUF5OG45ZnJ6enFERHJUbi1aTUlVSmNtN1A1c1huWXEyMHpGUEFnX1BnWmh3T2pfanlfU1J5WVViN3hkbWdHeTQmbV9lbnRzdHJlYW1fc291cmNlPWZlZWRfbW9iaWxlJmFuY2hvcl9jb21wb3Nlcj1mYWxzZSZwYWlwdj0wIiwicyI6Im0ifQ%3D%3D; dpr=2; c_user=100007492989639; datr=nBKRZOt4I7oRWUPO6dz8G_Qm; xs=1%3AdLjEhwbjyl3i_g%3A2%3A1687229082%3A-1%3A6312%3A%3AAcUtM9ppHBbFf8ViAvIp-VYNc7UIKocv0NQXBmlWKg; cppo=1; usida=eyJ2ZXIiOjEsImlkIjoiQXJ3amkwY3NkdWFhdCIsInRpbWUiOjE2ODcyNDQ3MTF9; wd=1680x202; fr=03fB894jVQjzF5L8K.AWWk6PlQjzTKJZA4UFU62ZxqV8Y.BkkU1-.9A.AAA.0.0.BkkVIS.AWUR_xQoZ7E; presence=C%7B%22t3%22%3A%5B%5D%2C%22utc3%22%3A1687245336101%2C%22v%22%3A1%7D";

        const config = {
            headers: {
                'Cookie': cookie,
            }
        };

        let htmlContentManagement = await lastValueFrom(
            this.httpService.get(`https://business.facebook.com/content_management`, config).pipe(
                map((response) => {
                    return response.data;
                }),
            ),
        );

        let accessToken = this.getAccessToken(htmlContentManagement);

        if (!accessToken) {
            htmlContentManagement = await lastValueFrom(
                this.httpService.get('https://business.facebook.com/business_locations', config).pipe(
                    map((response) => {
                        return response.data;
                    }),
                ),
            );
        }

        accessToken = this.getAccessToken(htmlContentManagement);

        return {
            success: true,
            accessToken
        }
    }

    public getAccessToken(html: string): string {
        const checkData = /\["(EAA.*?)","436761779744620"/.exec(html);
        if (!checkData || checkData.length < 1) return '';
        return checkData[1];
    }*/
}
