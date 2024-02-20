import { BadRequestException, forwardRef, HttpStatus, Module } from '@nestjs/common';
import { CookiesService } from './cookies.service';
import { CookiesController } from './cookies.controller';
import { TypeOrmModule } from "@nestjs/typeorm";
import { CookieEntity } from "./entities/cookie.entity";
import { MulterModule } from "@nestjs/platform-express";
import { diskStorage } from "multer";
import { existsSync, mkdirSync } from "fs";
import { v4 as uuidv4 } from 'uuid';
import { UserModule } from "../user/user.module";

const extname = require('ext-name');

@Module({
    imports: [
        forwardRef(() => UserModule),
        TypeOrmModule.forFeature([ CookieEntity ]),
        MulterModule.registerAsync({
            useFactory: () => ({
                dest: './upload',
                fileFilter: (req, file, callback) => {
                    if (!file.originalname.match(/\.(xls|xlsx)$/)) {
                        return callback(new BadRequestException({
                            statusCode: HttpStatus.BAD_REQUEST,
                            message: 'Chỉ cho phép upload file Excel.'
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

                        uploadPath += '/excel';
                        if (!existsSync(uploadPath)) {
                            mkdirSync(uploadPath);
                        }

                        cb(null, uploadPath);
                    },
                    // File modification details
                    filename: (req: any, file: any, cb: any) => {
                        const fileExtName = extname(file.originalname);
                        const fileName = `${file.fieldname.toLowerCase()}-${uuidv4()}.${fileExtName[0].ext}`
                        // Calling the callback passing the random name generated with the original extension name
                        cb(null, fileName);
                    },
                }),
            }),
        }),
    ],
    controllers: [ CookiesController ],
    providers: [ CookiesService ]
})
export class CookiesModule {
}
