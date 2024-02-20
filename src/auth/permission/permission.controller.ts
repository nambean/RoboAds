import { Body, Controller, Get, Post, Query, UseGuards } from '@nestjs/common';
import { PermissionService } from './permission.service';
import { JwtAuthGuard } from '../guards/jwt.guard';
import { PermissionEntity } from './permission.entity';
import { Pagination } from 'nestjs-typeorm-paginate';
import { RolePermissionGuard } from '../guards/role.permission.guard';

@Controller('permission')
export class PermissionController {
    constructor(private permissionService: PermissionService) {
    }

    @Post('create')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public create(@Body() permission: PermissionEntity): Promise<any> {
        return this.permissionService.create(permission);
    }

    @Post('update')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public update(@Body() permission: PermissionEntity): Promise<any> {
        return this.permissionService.update(permission);
    }

    @Post('delete')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public delete(@Body() permission: PermissionEntity): Promise<any> {
        return this.permissionService.delete(permission);
    }

    @Get('getPermissionById')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getPermissionById(@Query('id') id: number): Promise<PermissionEntity> {
        return this.permissionService.getPermission(Number(id));
    }

    @Get('getList')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getList(
        @Query('search') search: string = '',
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ): Promise<Pagination<PermissionEntity>> {
        return this.permissionService.paginate(search, { page: page, limit: limit });
    }
}
