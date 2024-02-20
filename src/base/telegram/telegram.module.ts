import { Module } from '@nestjs/common';
import { TelegramService } from "./telegram.service";
import { HttpModule } from "@nestjs/axios";

@Module({
    imports: [
        HttpModule.register({
            timeout: 5000,
            maxRedirects: 5,
        })
    ],
    providers: [ TelegramService ],
    exports: [ TelegramService ], // 👈 export for DI
})
export class TelegramModule {
}
