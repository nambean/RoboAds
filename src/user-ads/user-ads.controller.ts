import { Body, Controller, Get, Post, Query, UseGuards } from '@nestjs/common';
import { UserAdsService } from './user-ads.service';
import { JwtAuthGuard } from "../auth/guards/jwt.guard";
import { AdsCampaignDto } from "./entities/ads-campaign.dto";

@Controller('user-ads')
export class UserAdsController {
    constructor(private readonly userAdsService: UserAdsService) {
    }

    @Post('sync')
    @UseGuards(JwtAuthGuard)
    public sync(@Body() userAds: UserAdsDto[]) {
        return this.userAdsService.sync(userAds)
    }

    @Post('adsCampaignSync')
    @UseGuards(JwtAuthGuard)
    public adsCampaignSync(@Body() adsCampaignDto: AdsCampaignDto[]) {
        return this.userAdsService.adsCampaignSync(adsCampaignDto)
    }

    @Get('getAdsAccount')
    @UseGuards(JwtAuthGuard)
    public getAdsAccount(
        @Query('search') search: string,
        @Query('cookieId') cookieId: number,
        @Query('startDate') fromDate: string,
        @Query('endDate') toDate: string,
        @Query('page') page: number = 0,
        @Query('limit') limit: number = 10
    ) {
        return this.userAdsService.getAdsAccount({ search, cookieId, fromDate, toDate }, { page, limit });
    }

    @Get('allCampaign')
    @UseGuards(JwtAuthGuard)
    public allCampaign(
        @Query('cookieId') cookieId: number,
        @Query('accountId') accountId: string,
    ) {
        return this.userAdsService.allCampaign({ cookieId, accountId });
    }

    @Get('getCampaign')
    @UseGuards(JwtAuthGuard)
    public getCampaign(
        @Query('cookieId') cookieId: number,
        @Query('accountId') accountId: string,
        @Query('startDate') fromDate: string,
        @Query('endDate') toDate: string,
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10,
        @Query('campaignIds') campaignIds: string,
    ) {
        return this.userAdsService.getCampaign({ cookieId, accountId, fromDate, toDate, campaignIds });
    }

    @Get('getCampaignHistory')
    @UseGuards(JwtAuthGuard)
    public getCampaignHistory(
        @Query('cookieId') cookieId: number,
        @Query('campaignId') campaignId: string,
        @Query('startDate') fromDate: string,
        @Query('endDate') toDate: string,
        @Query('page') page: number = 1,
        @Query('limit') limit: number = 10
    ) {
        return this.userAdsService.getCampaignHistory({ cookieId, fromDate, toDate, campaignId }, { page, limit });
    }
}
