import { forwardRef, Module } from '@nestjs/common';
import { RoleService } from './role.service';
import { RoleController } from './role.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { RoleEntity } from './role.entity';
import { UserModule } from '../../user/user.module';
import { PermissionModule } from '../permission/permission.module';

@Module({
    imports: [
        TypeOrmModule.forFeature([ RoleEntity ]),
        forwardRef(() => UserModule),
        PermissionModule,
    ],
    providers: [ RoleService ],
    controllers: [ RoleController ],
    exports: [ RoleService ]
})
export class RoleModule {
}
