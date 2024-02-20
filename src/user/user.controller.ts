import { Body, Controller, Get, HttpCode, Post, Query, Req, UseGuards, } from '@nestjs/common';
import { UserService } from './user.service';
import { UserLoginDto } from "./dto/user-login.dto";
import { JwtAuthGuard } from "../auth/guards/jwt.guard";
import { RolePermissionGuard } from "../auth/guards/role.permission.guard";
import { Observable } from "rxjs";
import { Pagination } from "nestjs-typeorm-paginate";
import { UserEntity } from "./user.entity";

const requestIp = require('request-ip');

@Controller('user')
export class UserController {
    constructor(
        private userService: UserService,
    ) {
    }

    @Post('login')
    @HttpCode(200)
    public login(@Req() req: any, @Body() user: UserLoginDto): Promise<any> {
        const ipAddress = requestIp.getClientIp(req);
        return this.userService.login(ipAddress, user);
    }

    @Get('getCurrent')
    @UseGuards(JwtAuthGuard)
    public getMemberSystem() {
        return this.userService.getCurrent();
    }

    @Get('getListUser')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getListUser(
        @Query('search') search: string = '',
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ): Promise<Observable<Pagination<UserEntity>>> {
        return this.userService.paginate(search, { page: page, limit: limit });
    }

    @Get('getUserById')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getUserById(
        @Query('id') id: number = 1
    ): Promise<UserEntity> {
        return this.userService.getUserInfoById(id);
    }

    @Post('adminCreateUser')
    @HttpCode(200)
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public adminCreateUser(@Body() user: UserEntity): Promise<UserEntity> {
        return this.userService.adminCreateUser(user);
    }

    @Post('adminUpdateUser')
    @HttpCode(200)
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public update(@Body() user: UserEntity): Promise<UserEntity> {
        return this.userService.adminUpdateUser(user);
    }
}
