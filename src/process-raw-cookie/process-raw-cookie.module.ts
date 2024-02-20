import { Module } from '@nestjs/common';
import { ProcessRawCookieService } from './process-raw-cookie.service';
import { ProcessRawCookieController } from './process-raw-cookie.controller';
import { TypeOrmModule } from "@nestjs/typeorm";
import { CookieRaw } from "../public/cookie-raw.entity";
import { CookieEntity } from "../cookies/entities/cookie.entity";

@Module({
    imports: [
        TypeOrmModule.forFeature([ CookieRaw, CookieEntity ]),
    ],
    controllers: [ ProcessRawCookieController ],
    providers: [ ProcessRawCookieService ]
})
export class ProcessRawCookieModule {
}
