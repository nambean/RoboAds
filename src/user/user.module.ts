import { Module } from '@nestjs/common';
import { UserService } from './user.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from './user.entity';
import { AuthModule } from '../auth/auth.module';
import { UserController } from './user.controller';
import { TelegramModule } from "../base/telegram/telegram.module";
import { CookieEntity } from "../cookies/entities/cookie.entity";
import { RoleEntity } from "../auth/role/role.entity";
import { HttpModule } from "@nestjs/axios";

@Module({
    imports: [
        TypeOrmModule.forFeature([ UserEntity, CookieEntity, RoleEntity ]),
        AuthModule,
        TelegramModule,
        HttpModule.register({}),
    ],
    providers: [ UserService ],
    controllers: [ UserController ],
    exports: [ UserService ]
})
export class UserModule {
}
