import { Injectable, Logger, Scope, UseGuards } from '@nestjs/common';
import { In, Repository } from 'typeorm';
import { OnGatewayDisconnect, OnGatewayInit, WebSocketGateway, WebSocketServer } from "@nestjs/websockets";
import { Socket, Server } from 'socket.io';
import { JwtAuthGuard } from '../auth/guards/jwt.guard';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { WebsocketConnection } from './websocket.entity';

const parser = require('ua-parser-js');

@Injectable({ scope: Scope.DEFAULT })
@WebSocketGateway({ cors: true })
export class WebsocketService implements OnGatewayInit, OnGatewayDisconnect {
    @WebSocketServer()
    public server: Server;
    private readonly logger = new Logger(WebsocketService.name);

    constructor(
        private jwtService: JwtService,
        @InjectRepository(WebsocketConnection)
        private websocketConnectionRepository: Repository<WebsocketConnection>,
    ) {
    }

    afterInit(server: any) {
        this.logger.debug('Init websocket');
    }

    @UseGuards(JwtAuthGuard)
    async handleConnection(client: Socket) {
        if (!client.handshake.query.accessToken) {
            return;
        }

        try {
            const verify = this.jwtService.verify(<string>client.handshake.query.accessToken);
            if (verify && verify.user && verify.user.id) {
                const websocketConnection = new WebsocketConnection();
                const userId = verify.user.id;
                const ua = parser(client.handshake.headers['user-agent']);

                websocketConnection.userId = userId;
                websocketConnection.clientId = client.id;
                websocketConnection.userAgent = client.handshake.headers["user-agent"];
                websocketConnection.issued = client.handshake.issued;
                websocketConnection.browser = ua.browser.name;
                websocketConnection.platform = ua.os.name;
                websocketConnection.address = client.handshake.headers['x-real-ip'] + '';
                websocketConnection.isDisconnect = false;
                websocketConnection.createdAt = websocketConnection.updateAt = new Date();
                websocketConnection.createdBy = websocketConnection.updateBy = userId;

                await this.websocketConnectionRepository.save(websocketConnection);
            }
        } catch (ex) {
            this.logger.error(ex.message);
        }
    }

    /**
     * Disconnect Socket
     * @param client Socket
     */
    async handleDisconnect(client: Socket) {
        const socketConnect = await this.websocketConnectionRepository.findOne({ clientId: client.id });
        if (socketConnect) {
            socketConnect.updateAt = new Date();
            socketConnect.isDisconnect = true;
            await this.websocketConnectionRepository.save(socketConnect);
        }
    }

    /**
     * Bắn thông tin thay đổi danh hiệu xuống client
     * @param userList Danh sách người dùng cần thay đổi danh hiệu
     * @param title Mã danh hiệu
     * @private
     */
    private async pushTitleNotification(userList: any[], title: number) {
        // Lấy ra tất cả các socket của người dùng
        const idUserList = userList.map(x => x.id);
        const socketList = await this.websocketConnectionRepository.find({
            userId: In(idUserList), isDisconnect: false
        });

        if (socketList.length > 0) {
            // Bắn notification cho tất cả người dùng
            const roomList = socketList.map(x => x.clientId);
            this.server.to(roomList).emit('user-title', '');
            this.logger.debug(JSON.stringify(roomList));
        }
    }
}
