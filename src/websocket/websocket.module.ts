import { Module } from '@nestjs/common';
import { JwtModule } from "@nestjs/jwt";
import { ConfigModule, ConfigService } from "@nestjs/config";
import { TypeOrmModule } from "@nestjs/typeorm";
import { WebsocketConnection } from "./websocket.entity";
import { WebsocketService } from "./websocket.service";

@Module({
    imports: [
        JwtModule.registerAsync({
            imports: [ ConfigModule ],
            inject: [ ConfigService ],
            useFactory: async (configService: ConfigService) => ({
                secret: configService.get('JWT_SECRET'),
                signOptions: { expiresIn: '10d' }
            })
        }),
        TypeOrmModule.forFeature([ WebsocketConnection ]),
    ],
    providers: [ WebsocketService ],
    exports: [ WebsocketService ] // 👈 export for DI
})
export class WebsocketModule {
}
