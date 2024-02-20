import { Body, Controller, Get, Post, Query, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../guards/jwt.guard';
import { MenuEntity } from './menu.entity';
import { MenuService } from './menu.service';
import { Pagination } from 'nestjs-typeorm-paginate';
import { RolePermissionGuard } from '../guards/role.permission.guard';

@Controller('menu')
export class MenuController {
    constructor(private menuService: MenuService) {
    }

    @Post('create')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public create(@Body() menu: MenuEntity): Promise<any> {
        return this.menuService.create(menu);
    }

    @Post('update')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public update(@Body() menu: MenuEntity): Promise<any> {
        return this.menuService.update(menu);
    }

    @Post('delete')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public delete(@Body() menu: MenuEntity): Promise<any> {
        return this.menuService.delete(menu);
    }

    @Get('getMenuById')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getMenuById(@Query('id') id: number): Promise<MenuEntity> {
        return this.menuService.getMenu(Number(id));
    }

    @Get('getList')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getList(
        @Query('search') search: string = '',
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ): Promise<Pagination<MenuEntity>> {
        return this.menuService.paginate(search, { page: page, limit: limit });
    }

    @Get('getAllMenu')
    @UseGuards(JwtAuthGuard, RolePermissionGuard)
    public getAllMenu(): Promise<MenuEntity[]> {
        return this.menuService.getAllMenu();
    }
}
