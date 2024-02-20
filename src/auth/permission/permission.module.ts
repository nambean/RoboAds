import { forwardRef, Module } from '@nestjs/common';
import { PermissionService } from './permission.service';
import { PermissionController } from './permission.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { PermissionEntity } from './permission.entity';
import { UserModule } from '../../user/user.module';

@Module({
    imports: [
        TypeOrmModule.forFeature([ PermissionEntity ]),
        forwardRef(() => UserModule)
    ],
    providers: [ PermissionService ],
    controllers: [ PermissionController ],
    exports: [ PermissionService ]
})
export class PermissionModule {
}
