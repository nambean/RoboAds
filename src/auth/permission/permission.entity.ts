import { CustomBaseEntity } from '../../base/base.entity';
import { Column, Entity, ManyToMany, PrimaryGeneratedColumn } from 'typeorm';
import { RoleEntity } from '../role/role.entity';

@Entity('permission')
export class PermissionEntity extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('char', { name: 'permission_url', length: 255 })
    public permissionURL: string;

    @Column({ name: 'permission_name', length: 100 })
    public permissionName: string;

    @Column({ name: 'module_name', length: 100 })
    public moduleName: string;

    @Column({ name: 'description', nullable: true })
    public description: string;

    @Column({ name: 'is_active', default: true })
    public isActive: boolean;

    @ManyToMany(() => RoleEntity, role => role.permissions)
    public roles: RoleEntity[];

    public selected: boolean;
}
