import { Module } from '@nestjs/common';
import { UserAdsService } from './user-ads.service';
import { UserAdsController } from './user-ads.controller';
import { TypeOrmModule } from "@nestjs/typeorm";
import { UserAdsEntity } from "./entities/user-ads.entity";
import { AdsCampaign } from "./entities/ads.campaign";
import { AdsCampaignHistory } from "./entities/ads-campaign.history";
import { UserModule } from "../user/user.module";

@Module({
    imports: [
        TypeOrmModule.forFeature([ UserAdsEntity, AdsCampaign, AdsCampaignHistory ]),
        UserModule,
    ],
    controllers: [ UserAdsController ],
    providers: [ UserAdsService ]
})
export class UserAdsModule {
}
