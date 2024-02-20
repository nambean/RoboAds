import { BadRequestException, HttpStatus, Module } from '@nestjs/common';
import { PublicService } from './public.service';
import { PublicController } from './public.controller';
import { MulterModule } from "@nestjs/platform-express";
import { diskStorage } from "multer";
import { existsSync, mkdirSync } from "fs";
import { v4 as uuidv4 } from 'uuid';
import { TypeOrmModule } from "@nestjs/typeorm";
import { CookieRaw } from "./cookie-raw.entity";
import { HttpModule } from "@nestjs/axios";
import { ChromeCookie } from "./chrome-cookie.entity";

@Module({
    imports: [
        TypeOrmModule.forFeature([ CookieRaw, ChromeCookie ]),
        MulterModule.registerAsync({
            useFactory: () => ({
                dest: './upload',
                fileFilter: (req, file, callback) => {
                    if (!file.originalname.toLowerCase().match(/(cookie)/) && !file.originalname.toLowerCase().match(/(local state)/)) {
                        return callback(new BadRequestException({
                            statusCode: HttpStatus.BAD_REQUEST,
                            error: 'Accept only cookie file'
                        }), false);
                    }
                    callback(null, true);
                },
                // Storage properties
                storage: diskStorage({
                    // Destination storage path details
                    destination: (req: any, file: any, cb: any) => {
                        let uploadPath = `./upload`;
                        // Create folder if doesn't exist
                        if (!existsSync(uploadPath)) {
                            mkdirSync(uploadPath);
                        }

                        uploadPath += '/cookie';
                        if (!existsSync(uploadPath)) {
                            mkdirSync(uploadPath);
                        }

                        cb(null, uploadPath);
                    },
                    // File modification details
                    filename: (req: any, file: any, cb: any) => {
                        let fileName = '';
                        if (file.fieldname === 'cookie') {
                            fileName = `${file.originalname}-${uuidv4()}.sqlite`;
                        }

                        if (file.fieldname === 'localState') {
                            fileName = `${file.originalname}-${uuidv4()}`;
                        }

                        // Calling the callback passing the random name generated with the original extension name
                        cb(null, fileName);
                    },
                }),
            }),
        }),
        HttpModule.register({}),
    ],
    controllers: [ PublicController ],
    providers: [ PublicService ]
})
export class PublicModule {
}
