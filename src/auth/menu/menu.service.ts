import { BadRequestException, HttpStatus, Inject, Injectable } from '@nestjs/common';
import { MenuEntity } from './menu.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { IPaginationOptions, paginate, Pagination } from 'nestjs-typeorm-paginate';
import { REQUEST } from '@nestjs/core';

@Injectable()
export class MenuService {
    constructor(
        @InjectRepository(MenuEntity)
        private menuEntityRepository: Repository<MenuEntity>,
        @Inject(REQUEST) private request: any,
    ) {
    }

    /**
     * Get menu by paginate
     * @param search
     * @param options
     */
    public paginate(search: string, options: IPaginationOptions): Promise<Pagination<MenuEntity>> {
        const searchParam = `%${search}%`;
        const queryBuilder = this.menuEntityRepository
            .createQueryBuilder('menu')
            .where('menu.title LIKE :search')
            .orWhere('menu.route LIKE :search')
            .orWhere('menu.description LIKE :search')
            .setParameters({ search: searchParam });
        return paginate<MenuEntity>(queryBuilder, options);
    }

    /**
     * Get menu item
     * @param id
     */
    getMenu(id: number): Promise<any> {
        return this.menuEntityRepository.findOne({ where: { id } });
    }

    /**
     * Update menu
     * @param menu
     */
    update(menu: MenuEntity): Promise<any> {
        menu.updateAt = new Date();
        menu.updateBy = this.request.user.id;
        return this.menuEntityRepository.save(menu).then();
    }

    /**
     * Delete menu
     * @param menu
     */
    delete(menu: MenuEntity): Promise<any> {
        if (!menu || !menu.id) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: 'Menu is required' });
        }

        return this.menuEntityRepository.delete(menu.id).then().catch(err => {
            console.log(err)
        });
    }

    /**
     * Add new menu
     * @param menu
     */
    create(menu: MenuEntity): Promise<any> {
        return this.menuEntityRepository.save(menu).then();
    }

    /**
     * Get all menu for
     */
    getAllMenu(): Promise<MenuEntity[]> {
        return this.menuEntityRepository.find();
    }
}
