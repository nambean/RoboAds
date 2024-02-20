import {
    BadRequestException,
    Body,
    Controller,
    Get,
    HttpStatus,
    Post,
    Query, UploadedFiles,
    UseGuards,
    UseInterceptors
} from '@nestjs/common';
import { CookiesService } from './cookies.service';
import { JwtAuthGuard } from "../auth/guards/jwt.guard";
import { CookieEntity, CookieRunningStatus } from "./entities/cookie.entity";
import { Pagination } from "nestjs-typeorm-paginate";
import { UpdateCookieDto } from "./entities/update-cookie.dto";
import { FileFieldsInterceptor } from "@nestjs/platform-express";
import { RolePermissionGuard } from "../auth/guards/role.permission.guard";

@Controller('cookies')
export class CookiesController {
    constructor(private readonly cookiesService: CookiesService) {
    }

    @Post('upload')
    @UseInterceptors(
        FileFieldsInterceptor([
            { name: 'cookie', maxCount: 1 },
        ]),
    )
    @UseGuards(JwtAuthGuard)
    public uploadCookie(
        @UploadedFiles() files: {
            cookie?: Express.Multer.File[],
        },
    ) {
        return this.cookiesService.uploadCookie(files);
    }

    @Get('getList')
    @UseGuards(JwtAuthGuard)
    public getList(
        @Query('useStatus') useStatus: number = null,
        @Query('startDate') fromDate: string,
        @Query('endDate') toDate: string,
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ): Promise<Pagination<CookieEntity>> {
        return this.cookiesService.paginate(useStatus, fromDate, toDate, { page, limit });
    }

    @Post('initData')
    @UseGuards(JwtAuthGuard)
    public initData() {
        return this.cookiesService.initData();
    }

    @Post('runningCookie')
    @UseGuards(JwtAuthGuard)
    public runningCookie(
        @Body() updateCookieDto: UpdateCookieDto
    ) {
        if (!updateCookieDto) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin cookie'
            });
        }

        updateCookieDto.runningStatus = CookieRunningStatus.RUNNING;
        updateCookieDto.useStatus = 1;

        return this.cookiesService.changeCookieStatus(updateCookieDto);
    }

    @Post('pauseRunningCookie')
    @UseGuards(JwtAuthGuard)
    public pauseRunningCookie(
        @Body() updateCookieDto: UpdateCookieDto
    ) {
        if (!updateCookieDto) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin cookie'
            });
        }

        updateCookieDto.runningStatus = CookieRunningStatus.PAUSE;
        updateCookieDto.useStatus = 1;
        updateCookieDto.licenceKey = null;

        return this.cookiesService.changeCookieStatus(updateCookieDto);
    }

    @Post('stopRunningCookie')
    @UseGuards(JwtAuthGuard)
    public stopRunningCookie(
        @Body() updateCookieDto: UpdateCookieDto
    ) {
        if (!updateCookieDto) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                message: 'Không tồn tại thông tin cookie'
            });
        }

        // updateCookieDto.runningStatus = CookieRunningStatus.STOP;
        updateCookieDto.useStatus = 1;
        updateCookieDto.licenceKey = null;

        return this.cookiesService.changeCookieStatus(updateCookieDto);
    }

    @Post('delete')
    @UseGuards(JwtAuthGuard)
    public delete(@Body() cookie: CookieEntity) {
        return this.cookiesService.delete(cookie);
    }
}
