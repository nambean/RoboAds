import { Column, Entity, JoinTable, ManyToMany, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../../base/base.entity';
import { UserEntity } from '../../user/user.entity';
import { PermissionEntity } from '../permission/permission.entity';
import { MenuEntity } from '../menu/menu.entity';

@Entity('role')
export class RoleEntity extends CustomBaseEntity {
  @PrimaryGeneratedColumn('increment')
  public id: number;

  @Column('char', { name: 'role_key', length: 100 })
  public roleKey: string;

  @Column({ name: 'role_name', length: 100 })
  public roleName: string;

  @Column({ name: 'description', nullable: true })
  public description: string;

  @Column({ name: 'is_active', default: true })
  public isActive: boolean;

  @Column({ name: 'is_default', default: false })
  public isDefault: boolean;

  @ManyToMany(() => UserEntity, user => user.roles)
  public users: UserEntity[];

  @ManyToMany(() => PermissionEntity, permission => permission.roles)
  @JoinTable({
    name: 'role_permission',
    joinColumn: {
      name: 'role_id',
      referencedColumnName: 'id',
    },
    inverseJoinColumn: {
      name: 'permission_id',
      referencedColumnName: 'id',
    },
  })
  public permissions: PermissionEntity[];

  @ManyToMany(() => MenuEntity, menu => menu.roles)
  @JoinTable({
    name: 'role_menu',
    joinColumn: {
      name: 'role_id',
      referencedColumnName: 'id',
    },
    inverseJoinColumn: {
      name: 'menu_id',
      referencedColumnName: 'id',
    },
  })
  public menus: MenuEntity[];
}
