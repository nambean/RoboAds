import { Column, Entity, JoinTable, ManyToMany, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../base/base.entity';
import { RoleEntity } from "../auth/role/role.entity";
import { MenuEntity } from "../auth/menu/menu.entity";

@Entity('user')
export class UserEntity extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('char', { name: 'user_name', unique: true, length: 50 })
    public userName: string;

    @Column('char', { name: 'user_email', unique: true, length: 200, default: null })
    public userEmail: string;

    @Column({ name: 'picture', nullable: true })
    public picture: string;

    @Column('char', { name: 'password', length: 60 })
    public password: string;

    @Column('char', { name: 'pin', length: 60, nullable: true })
    public pin: string;

    @Column({ name: 'full_name', nullable: true })
    public fullName: string;

    @Column({ name: 'is_active', default: true })
    public isActive: boolean;

    @Column('tinyint', { name: 'gender', nullable: true, default: 0 })
    public gender: number;

    @Column('date', { name: 'date_of_birth', nullable: true, default: null })
    public dateOfBirth: string;

    @Column('char', { name: 'uuid_active', length: 36, nullable: true })
    public uuidActive: string;

    @Column('text', { name: 'meta_data', nullable: true })
    public metaData: string;

    @Column('smallint', { name: 'team', default: null })
    public team: number;

    @ManyToMany(() => RoleEntity, role => role.users)
    @JoinTable({
        name: 'user_role',
        joinColumn: {
            name: 'user_id',
            referencedColumnName: 'id'
        },
        inverseJoinColumn: {
            name: 'role_id',
            referencedColumnName: 'id'
        }
    })
    public roles: RoleEntity[];

    public menus: MenuEntity[];

    public userPIN: string;
}
