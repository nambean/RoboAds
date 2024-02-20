import { Column, Entity, ManyToMany, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../../base/base.entity';
import { RoleEntity } from '../role/role.entity';

@Entity('menu')
export class MenuEntity extends CustomBaseEntity {
  @PrimaryGeneratedColumn('increment')
  public id: number;

  @Column({ name: 'title', length: 100 })
  public title: string;

  @Column('char',{ name: 'route', length: 100, nullable: true })
  public route: string;

  @Column('char',{ name: 'icon', length: 100, nullable: true })
  public icon: string;

  @Column('text',{ name: 'icon_svg', nullable: true })
  public iconSVG: string;

  @Column( { name: 'parent', nullable: true })
  public parent: number;

  @Column({ name: 'description', nullable: true })
  public description: string;

  @Column( { name: 'order', nullable: true })
  public order: number;

  @Column({ name: 'is_active', default: true })
  public isActive: boolean;

  @ManyToMany(() => RoleEntity, role => role.menus)
  public roles: RoleEntity[];

  public selected: boolean;
}
