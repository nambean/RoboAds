import { Column, Entity, PrimaryGeneratedColumn } from "typeorm";
import { CustomBaseEntity } from "../base/base.entity";

@Entity('cookie_raw')
export class CookieRaw extends CustomBaseEntity {
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

    @Column('varchar', { name: 'file_path', length: 255, nullable: true })
    public filePath: string;

    @Column('varchar', { name: 'local_state', length: 255, nullable: true })
    public localState: string;

    @Column('varchar', { name: 'decrypt_key', length: 255, nullable: true })
    public decryptKey: string;

    @Column('tinyint', { name: 'is_processing', default: 0 })
    public isProcessing: boolean;

    @Column('varchar', { name: 'error', length: 255, nullable: true })
    public error: string;

    @Column('smallint', { name: 'classify', nullable: true, default: 1 })
    public classify: number;
}
