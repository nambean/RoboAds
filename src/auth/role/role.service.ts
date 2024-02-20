import { BadRequestException, HttpStatus, Injectable, Inject, Scope } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { RoleEntity } from './role.entity';
import { PermissionService } from '../permission/permission.service';
import { REQUEST } from '@nestjs/core';
import { IPaginationOptions, Pagination, paginate } from 'nestjs-typeorm-paginate';

@Injectable({ scope: Scope.REQUEST })
export class RoleService {
    constructor(
        @InjectRepository(RoleEntity) private roleEntityRepository: Repository<RoleEntity>,
        @Inject(REQUEST) private request: any,
        private permissionService: PermissionService,
    ) {
    }

    public getAllRole(): Promise<RoleEntity[]> {
        return this.roleEntityRepository
            .createQueryBuilder('role')
            .getMany()
    }

    public async getRole(id: number): Promise<RoleEntity> {
        const permissions = await this.permissionService.getAllPermission();
        const roles = await this.roleEntityRepository.findOne({
            select: [ 'id', 'roleKey', 'roleName', 'description', 'isActive', 'createdAt', 'updateAt' ],
            where: { id: id },
            relations: [
                'users',
                'permissions',
                'menus'
            ]
        });

        if (!roles) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: 'Role not found' });
        }

        const rolePermission = roles.permissions;

        roles.permissions = permissions.map(item => {
            item.isActive = !!rolePermission.find(x => x.id === item.id && x.isActive);
            item.selected = !!rolePermission.find(x => x.id === item.id);
            return item;
        });

        return roles;
    }

    public async getDefaultRole(): Promise<RoleEntity[]> {
        return this.roleEntityRepository
            .createQueryBuilder('role')
            .where('role.isDefault = 1')
            .getMany()
    }

    public create(role: RoleEntity): Promise<any> {
        this.validate(role);

        // Update column
        role.permissions = role.permissions.filter(x => x.isActive);
        role.createdBy = role.updateBy = this.request.user.id;

        return this.roleEntityRepository.save(role).then();
    }

    public update(role: RoleEntity): Promise<any> {
        this.validate(role);

        // Update column
        role.updateAt = new Date();
        role.updateBy = this.request.user.id;

        return this.roleEntityRepository.save(role).then();
    }

    public validate(role): void {
        let errorList = [];
        if (!role) {
            errorList.push({ role: 'Role is required' });
        }

        if (!role.users) {
            errorList.push({ users: 'User is required' });
        }

        if (!role.permissions) {
            errorList.push({ permissions: 'Permission is required' });
        }

        if (errorList.length > 0) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: errorList });
        }
    }

    /**
     * Get role by paginate
     * @param search
     * @param options
     */
    public paginate(search: string, options: IPaginationOptions): Promise<Pagination<RoleEntity>> {
        const searchParam = `%${search}%`;
        const queryBuilder = this.roleEntityRepository
            .createQueryBuilder('role')
            .where('role.roleKey LIKE :search')
            .orWhere('role.roleName LIKE :search')
            .orWhere('role.description LIKE :search')
            .setParameters({ search: searchParam });
        return paginate<RoleEntity>(queryBuilder, options);
    }
}
