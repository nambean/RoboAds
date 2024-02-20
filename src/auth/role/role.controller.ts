import { Body, Controller, Get, Post, Query, UseGuards } from '@nestjs/common';
import { RoleService } from './role.service';
import { JwtAuthGuard } from '../guards/jwt.guard';
import { RoleEntity } from './role.entity';
import { Pagination } from 'nestjs-typeorm-paginate';
import { RolePermissionGuard } from '../guards/role.permission.guard';

@Controller('role')
export class RoleController {
    constructor(private roleService: RoleService) {
    }

    @Post('create')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public create(@Body() role: RoleEntity): Promise<any> {
        return this.roleService.create(role);
    }

    @Post('update')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public update(@Body() role: RoleEntity): Promise<any> {
        return this.roleService.update(role);
    }

    @Get('getRoleById')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getRoleById(@Query('id') id: number): Promise<RoleEntity> {
        return this.roleService.getRole(Number(id));
    }

    @Get('getList')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getList(
        @Query('search') search: string = '',
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ): Promise<Pagination<RoleEntity>> {
        return this.roleService.paginate(search, { page: page, limit: limit });
    }

    @Get('getAllRole')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getAllRole(): Promise<RoleEntity[]> {
        return this.roleService.getAllRole();
    }
}
