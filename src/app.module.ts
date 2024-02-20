import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { HttpModule } from "@nestjs/axios";
import { ConfigModule } from "@nestjs/config";
import { TypeOrmModule } from "@nestjs/typeorm";
import { UserEntity } from "./user/user.entity";
import { RoleEntity } from "./auth/role/role.entity";
import { PermissionEntity } from "./auth/permission/permission.entity";
import { MenuEntity } from "./auth/menu/menu.entity";
import { UserModule } from "./user/user.module";
import { RoleModule } from "./auth/role/role.module";
import { AuthModule } from "./auth/auth.module";
import { WebsocketModule } from "./websocket/websocket.module";
import { WebsocketConnection } from "./websocket/websocket.entity";
import { FacebookModule } from './facebook/facebook.module';
import { CookiesModule } from './cookies/cookies.module';
import { CookieEntity } from "./cookies/entities/cookie.entity";
import { PublicModule } from './public/public.module';
import { CookieRaw } from "./public/cookie-raw.entity";
import { UserAdsModule } from './user-ads/user-ads.module';
import { UserAdsEntity } from "./user-ads/entities/user-ads.entity";
import { AdsCampaign } from "./user-ads/entities/ads.campaign";
import { AdsCampaignHistory } from "./user-ads/entities/ads-campaign.history";
import { ProcessRawCookieModule } from './process-raw-cookie/process-raw-cookie.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { ServeStaticModule } from "@nestjs/serve-static";
import { join } from "path";
import { MenuModule } from "./auth/menu/menu.module";
import { ChromeCookie } from "./public/chrome-cookie.entity";
import { ScheduleModule } from '@nestjs/schedule';

@Module({
    imports: [
        HttpModule.register({}),
        ConfigModule.forRoot({ isGlobal: true }),
        TypeOrmModule.forRoot({
            type: 'mysql',
            host: process.env.DB_HOST,
            port: Number(process.env.DB_PORT),
            username: process.env.DB_USERNAME,
            password: process.env.DB_PASSWORD,
            database: process.env.DB_DATABASE,
            entities: [
                UserEntity, RoleEntity, PermissionEntity, MenuEntity, WebsocketConnection, CookieEntity, CookieRaw,
                UserAdsEntity, AdsCampaign, AdsCampaignHistory, ChromeCookie
            ],
            synchronize: true
        }),
        ServeStaticModule.forRoot({
            serveRoot: '/upload',
            rootPath: join(__dirname, '..', 'upload'),
        }),
        ScheduleModule.forRoot(),
        UserModule,
        RoleModule,
        AuthModule,
        MenuModule,
        WebsocketModule,
        FacebookModule,
        CookiesModule,
        PublicModule,
        UserAdsModule,
        ProcessRawCookieModule,
        DashboardModule
    ],
    controllers: [ AppController ],
    providers: [ AppService ],
})
export class AppModule {
}
