import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../base/base.entity';

@Entity('websocket_connection')
export class WebsocketConnection extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('int', { name: 'user_id', default: null, nullable: true })
    public userId: number;

    @Column('char', { name: 'client_id', length: 50 })
    public clientId: string;

    @Column('char', { name: 'user_agent', length: 255, nullable: true })
    public userAgent: string;

    @Column('char', { name: 'platform', length: 50, nullable: true })
    public platform: string;

    @Column('char', { name: 'browser', length: 50, nullable: true })
    public browser: string;

    @Column('char', { name: 'address', length: 255 })
    public address: string;

    @Column('bigint', { name: 'issued', default: 0 })
    public issued: number;

    @Column({ name: 'is_disconnect', default: true })
    public isDisconnect: boolean;
}
