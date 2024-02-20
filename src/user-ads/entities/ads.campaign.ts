import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';
import { CustomBaseEntity } from '../../base/base.entity';

@Entity('ads_campaign')
export class AdsCampaign extends CustomBaseEntity {
    @PrimaryGeneratedColumn('increment')
    public id: number;

    @Column('int', { name: 'cookie_id', nullable: true })
    public cookieId: number;

    @Column('char', { name: 'account_id', nullable: true, length: 50 })
    public accountId: string;

    @Column('char', { name: 'act_account_id', nullable: true, length: 50 })
    public actAccountId: string;

    @Column('char', { name: 'campaign_id', nullable: true, length: 50 })
    public campaignId: string;

    @Column('varchar', { name: 'name', nullable: true, length: 500 })
    public name: string;

    @Column('char', { name: 'objective', nullable: true, length: 50 })
    public objective: string;

    @Column('date', { name: 'date_start', nullable: true })
    public dateStart: Date;

    @Column('date', { name: 'date_stop', nullable: true })
    public dateStop: Date;

    @Column('char', { name: 'attribution_setting', nullable: true, length: 50 })
    public attributionSetting: string;

    @Column('double', { name: 'budget', nullable: true, default: 0 })
    public budget: number;

    @Column('int', { name: 'reach', nullable: true, default: 0 })
    public reach: number;

    @Column('double', { name: 'frequency', nullable: true, default: 0 })
    public frequency: number;

    @Column('double', { name: 'spend', nullable: true, default: 0 })
    public spend: number;

    @Column('double', { name: 'cost_per_impressions', nullable: true, default: 0 })
    public costPerImpressions: number;

    @Column('double', { name: 'cost_per_link_click', nullable: true, default: 0 })
    public costPerLinkClick: number;

    @Column('double', { name: 'link_click_through_rate', nullable: true, default: 0 })
    public linkClickThroughRate: number;

    @Column('int', { name: 'link_click', nullable: true, default: 0 })
    public linkClick: number;

    @Column('int', { name: 'clicks_all', nullable: true, default: 0 })
    public clicksAll: number;

    @Column('double', { name: 'ctr_all', nullable: true, default: 0 })
    public ctrAll: number;

    @Column('double', { name: 'cpc_all', nullable: true, default: 0 })
    public cpcAll: number;

    @Column('char', { name: 'result_indicator', nullable: true, length: 100 })
    public resultIndicator: string;

    public resultIndicatorStr: string;

    @Column('double', { name: 'results', nullable: true, default: 0 })
    public results: number;

    @Column('double', { name: 'cost_per_result', nullable: true, default: 0 })
    public costPerResult: number;

    @Column('int', { name: 'user_id', default: null, nullable: true })
    public userId: number;

    @Column('smallint', { name: 'team', default: null })
    public team: number;
}
