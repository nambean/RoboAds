import { Controller, Get } from '@nestjs/common';
import { ProcessRawCookieService } from './process-raw-cookie.service';

@Controller('process-raw-cookie')
export class ProcessRawCookieController {
    constructor(private readonly processRawCookieService: ProcessRawCookieService) {
    }

    @Get('process')
    public processCookie() {
        return this.processRawCookieService.processCookie();
    }
}
