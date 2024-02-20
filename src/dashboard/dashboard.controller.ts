import { Controller, Get, Query, UseGuards } from '@nestjs/common';
import { DashboardService } from './dashboard.service';
import { JwtAuthGuard } from "../auth/guards/jwt.guard";

@Controller('dashboard')
export class DashboardController {
    constructor(private readonly dashboardService: DashboardService) {
    }

    @Get('getDashboardInfo')
    @UseGuards(JwtAuthGuard)
    public getDashboardInfo(
        @Query('startDate') startDate: string,
        @Query('endDate') endDate: string,
    ) {
        return this.dashboardService.getDashboardInfo(startDate, endDate);
    }
}
