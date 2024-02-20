import { Controller, Get, Query, UseGuards } from '@nestjs/common';
import { FacebookService } from "./facebook.service";
import { JwtAuthGuard } from "../auth/guards/jwt.guard";

@Controller('facebook')
export class FacebookController {
    constructor(private readonly facebookService: FacebookService) {
    }

    /*@Get('accessToken')
    @UseGuards(JwtAuthGuard)
    getLimit(
        @Query('cookie') cookie: string,
    ) {
        return this.facebookService.getFacebookToken(cookie);
    }*/

    @Get('getLimit')
    @UseGuards(JwtAuthGuard)
    getLimit(
        @Query('cookie') cookie: string,
    ) {
        return this.facebookService.getLimit(cookie);
    }
}
