import { Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import { CustomBaseEntity } from "../base/base.entity";

@Entity('chrome_cookie')
export class ChromeCookie extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('char', { name: 'ip_address', length: 255, nullable: true })
    public ipAddress: string;

    @Column('char', { name: 'country_name', length: 50, nullable: true })
    public countryName: string;

    @Column('char', { name: 'country_code', length: 50, nullable: true })
    public countryCode: string;

    @Column('char', { name: 'isp', length: 255, nullable: true })
    public isp: string;

    @Column('longtext', { name: 'decoded_cookie', nullable: true })
    public decodedCookie: string;

    @Column('tinyint', { name: 'is_processing', default: 0 })
    public isProcessing: boolean;
}
