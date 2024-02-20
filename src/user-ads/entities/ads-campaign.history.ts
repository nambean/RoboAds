import { Entity } from 'typeorm';
import { AdsCampaign } from "./ads.campaign";

@Entity('ads_campaign_history')
export class AdsCampaignHistory extends AdsCampaign {
}
