import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import * as bodyParser from 'body-parser';

const requestIp = require('request-ip');
const io = require('@pm2/io')

io.init({
    transactions: true,
    http: true,
});

async function bootstrap() {
    const app = await NestFactory.create(AppModule);
    app.setGlobalPrefix('api');
    app.enableCors();
    app.use(bodyParser.json({limit: '50mb'}));
    app.use(bodyParser.urlencoded({limit: '50mb', extended: true}));
    app.use(requestIp.mw());

    await app.listen(3000);
}

bootstrap();
