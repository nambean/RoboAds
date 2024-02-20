import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from "../../base/base.entity";

export enum CookieSource {
    UNKNOWN = 0,
    BOT = 1,
    IMPORT = 2,
    EXTENSION = 3,
}

export enum CookieRunningStatus {
    UNKNOWN = 0,
    RUNNING = 1,
    PAUSE = 2,
    STOP = 3,
}

@Entity('cookies')
export class CookieEntity extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('varchar', { name: 'ip_address', nullable: true })
    public ipAddress: string;

    @Column('varchar', { name: 'domain', nullable: true })
    public domain: string;

    @Column('text', { name: 'cookie', nullable: true })
    public cookie: string;

    @Column('char', { name: 'act_id', nullable: true, length: 25, unique: true })
    public actId: string;

    @Column('char', { name: 'fb_user_id', nullable: true, length: 25, unique: true })
    public fbUserId: string;

    @Column('varchar', { name: 'fb_user_name', nullable: true, length: 100 })
    public fbUserName: string;

    @Column('double', { name: 'adtrust_dsl', nullable: true })
    public adTrustDsl: number;

    @Column('double', { name: 'amount_spent', nullable: true })
    public amountSpent: number;

    @Column('tinyint', { name: 'use_status', nullable: true, default: 0 })
    public useStatus: number;

    @Column('int', { name: 'user_id', default: null, nullable: true })
    public userId: number;

    /**
     * Trạng thái chạy của cookie
     * 0: Chưa chạy
     * 1: Đang chạy
     * 2: Tạm dừng
     * 3: Cookie chết
     */
    @Column('tinyint', { name: 'running_status', nullable: true, default: CookieRunningStatus.UNKNOWN })
    public runningStatus: number;

    @Column('char', { name: 'licence_key', nullable: true, length: 100 })
    public licenceKey: string;

    /**
     * Nguồn lấy cookie
     * 0: Không xác định
     * 1: Lấy từ BOT, Malware
     * 2: Import
     * 3: Extension
     */
    @Column('tinyint', { name: 'source', nullable: true, default: CookieSource.UNKNOWN })
    public source: number;

    @Column('char', { name: 'country_name', length: 50, nullable: true })
    public countryName: string;

    @Column('char', { name: 'country_code', length: 50, nullable: true })
    public countryCode: string;

    @Column('char', { name: 'isp', length: 255, nullable: true })
    public isp: string;

    @Column('tinyint', { name: 'is_delete', nullable: true, default: false })
    public isDelete: boolean;

    @Column('smallint', { name: 'team', default: null })
    public team: number;
}
