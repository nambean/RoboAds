import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../../base/base.entity';

@Entity('user_ads_account')
export class UserAdsEntity extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('int', { name: 'cookie_id', nullable: true })
    public cookieId: number;

    @Column('varchar', { name: 'account_name', nullable: true, length: 255 })
    public accountName: string;

    @Column('char', { name: 'account_id', nullable: true, length: 50 })
    public accountId: string;

    @Column('double', { name: 'amount', nullable: true })
    public amount: number;

    @Column('double', { name: 'amount_in_hundredths', nullable: true })
    public amountInHundredths: number;

    @Column('double', { name: 'offset_amount', nullable: true })
    public offsetAmount: number;

    @Column('tinyint', { name: 'is_prepay_account', default: false })
    public isPrepayAccount: boolean;

    @Column('char', { name: 'act_id', nullable: true, length: 50 })
    public actId: string;

    @Column('int', { name: 'user_id', default: null, nullable: true })
    public userId: number;

    /**
     * Trạng thái tài khoản
     */
    @Column('int', { name: 'account_status', nullable: true })
    public accountStatus: number;

    /**
     * Số dư
     */
    @Column('double', { name: 'balance', nullable: true })
    public balance: number;

    /**
     * Ngưỡng
     */
    @Column('double', { name: 'threshold_amount', nullable: true })
    public thresholdAmount: number;

    /**
     * Limit
     */
    @Column('double', { name: 'ad_trust_dsl', nullable: true })
    public adTrustDsl: number;

    /**
     * Tổng tiêu
     */
    @Column('double', { name: 'spend', nullable: true })
    public spend: number;

    /**
     * Tiền tệ
     */
    @Column('char', { name: 'currency', nullable: true, length: 10 })
    public currency: string;

    /**
     * Ngày lập hóa đơn
     */
    @Column('date', { name: 'next_bill_date', default: null, nullable: true })
    public nextBillDate: Date;

    /**
     * Ngày tạo tài khoản
     */
    @Column('datetime', { name: 'create_time', nullable: true })
    public createTime: Date;

    /**
     * Múi giờ TK
     */
    @Column('char', { name: 'timezone_name', nullable: true, length: 50 })
    public timezoneName: string;

    /**
     * Múi giờ TK
     */
    @Column('tinyint', { name: 'timezone_offset_hours_utc', nullable: true })
    public timezoneOffsetHoursUtc: number;

    @Column('char', { name: 'owner', nullable: true, length: 50 })
    public owner: string;

    @Column('char', { name: 'owner_business_id', nullable: true, length: 50 })
    public ownerBusinessId: string;

    @Column('varchar', { name: 'owner_business_name', nullable: true, length: 500 })
    public ownerBusinessName: string;

    @Column('char', { name: 'user_roles', nullable: true, length: 50 })
    public userRoles: string;

    @Column('text', { name: 'user_permissions', nullable: true })
    public userPermissions: string;

    @Column('char', { name: 'business_id', nullable: true, length: 50 })
    public businessId: string;

    @Column('varchar', { name: 'business_name', nullable: true, length: 500 })
    public businessName: string;

    @Column('tinyint', { name: 'disable_reason', nullable: true, default: 0})
    public disableReason: number;

    @Column('char', { name: 'business_country_code', nullable: true, length: 10})
    public businessCountryCode: string;

    @Column('double', { name: 'spend_cap', nullable: true })
    public spendCap: number;

    @Column('double', { name: 'amount_spent', nullable: true })
    public amountSpent: number;

    @Column('varchar', { name: 'payment_method_tokens', nullable: true, length: 500})
    public paymentMethodTokens: string;

    @Column('varchar', { name: 'pm_credit_card', nullable: true, length: 500})
    public pmCreditCard: string;

    @Column('varchar', { name: 'payment_method_direct_debits', nullable: true, length: 500})
    public paymentMethodDirectDebits: string;

    @Column('varchar', { name: 'payment_method_paypal', nullable: true, length: 500})
    public paymentMethodPaypal: string;

    @Column('smallint', { name: 'team', default: null })
    public team: number;
}
