import { Module } from '@nestjs/common';
import { DashboardService } from './dashboard.service';
import { DashboardController } from './dashboard.controller';
import { TypeOrmModule } from "@nestjs/typeorm";
import { WebsocketConnection } from "../websocket/websocket.entity";
import { CookieEntity } from "../cookies/entities/cookie.entity";
import { UserAdsEntity } from "../user-ads/entities/user-ads.entity";

@Module({
    imports: [
        TypeOrmModule.forFeature([WebsocketConnection, CookieEntity, UserAdsEntity])
    ],
    controllers: [ DashboardController ],
    providers: [ DashboardService ]
})
export class DashboardModule {
}
