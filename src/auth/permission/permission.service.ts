import { BadRequestException, HttpStatus, Inject, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PermissionEntity } from './permission.entity';
import { IPaginationOptions, Pagination, paginate } from 'nestjs-typeorm-paginate';
import { REQUEST } from '@nestjs/core';

@Injectable()
export class PermissionService {
    constructor(
        @InjectRepository(PermissionEntity)
        private permissionEntityRepository: Repository<PermissionEntity>,
        @Inject(REQUEST) private request: any,
    ) {
    }

    /**
     * Get permission by paginate
     * @param search
     * @param options
     */
    public paginate(search: string, options: IPaginationOptions): Promise<Pagination<PermissionEntity>> {
        const searchParam = `%${search}%`;
        const queryBuilder = this.permissionEntityRepository
            .createQueryBuilder('permission')
            .where('permission.permissionURL LIKE :search')
            .orWhere('permission.permissionName LIKE :search')
            .orWhere('permission.moduleName LIKE :search')
            .orWhere('permission.description LIKE :search')
            .setParameters({ search: searchParam });
        return paginate<PermissionEntity>(queryBuilder, options);
    }

    /**
     * Get permission item
     * @param id
     */
    getPermission(id: number): Promise<any> {
        return this.permissionEntityRepository.findOne({ where: { id } });
    }

    /**
     * Update permission
     * @param permission
     */
    update(permission: PermissionEntity): Promise<any> {
        this.validate(permission);

        permission.updateAt = new Date();
        permission.updateBy = this.request.user.id;
        return this.permissionEntityRepository.save(permission).then();
    }

    /**
     * Delete permission
     * @param permission
     */
    delete(permission: PermissionEntity): Promise<any> {
        if (!permission || !permission.id) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: 'Permission is required' });
        }

        return this.permissionEntityRepository.delete(permission.id).then();
    }

    /**
     * Add new permission
     * @param permission
     */
    create(permission: PermissionEntity): Promise<any> {
        this.validate(permission);

        permission.updateBy = permission.createdBy = this.request.user.id;
        return this.permissionEntityRepository.save(permission).then();
    }

    public getAllPermission(): Promise<PermissionEntity[]> {
        return this.permissionEntityRepository.find();
    }

    public validate(permission: PermissionEntity) {
        const error: any = {};

        if (!permission.moduleName) {
            error.moduleName = 'The Module name field is required';
        }

        if (!permission.permissionName) {
            error.permissionName = 'The Permission name field is required';
        }

        if (!permission.permissionURL) {
            error.permissionURL = 'The Permission URL field is required';
        }

        if (!error || Object.keys(error).length > 0) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        }
    }
}
