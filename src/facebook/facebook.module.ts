import { Module } from '@nestjs/common';
import { FacebookService } from './facebook.service';
import { FacebookController } from './facebook.controller';
import { HttpModule } from "@nestjs/axios";

@Module({
    imports: [
        HttpModule.register({}),
    ],
    providers: [ FacebookService ],
    controllers: [ FacebookController ]
})
export class FacebookModule {
}
