import { Injectable } from '@nestjs/common';
import { HttpService } from "@nestjs/axios";
import { Observable } from "rxjs";

@Injectable()
export class TelegramService {
    private telegramBot: string = "";
    private chanelId: string = "";

    constructor(
        private httpService: HttpService
    ) {
    }

    /**
     * Gửi tin nhắn qua telegram
     * @param message
     * @param chanelId
     */
    public sendMessage(message: string, chanelId?: string): Observable<any> {
        const chanelToSend = chanelId ? chanelId : this.chanelId;

        if (chanelId) {
            this.chanelId = chanelId;
        }

        return this.httpService
            .get(encodeURI(`https://api.telegram.org/${this.telegramBot}/sendMessage?chat_id=${chanelToSend}&text=${message}`))
    }
}
