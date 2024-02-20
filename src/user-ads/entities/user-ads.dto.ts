class UserAdsDto {
    public cookie_id: number;
    public account_id: string;
    public created_time: string;
    public currency: string;
    public adspaymentcycle: AdsPaymentCycle;
    public balance: string;
    public total_prepay_balance: TotalPrepayBalance;
    public is_prepay_account: boolean;
    public insights: Insights;
    public adtrust_dsl: number;
    public next_bill_date: string;
    public owner: string;
    public owner_business: IdNameInformation;
    public user_roles: UserRoles;
    public name: string;
    public account_status: number;
    public business: IdNameInformation;
    public timezone_offset_hours_utc: number;
    public timezone_name: string;
    public userpermissions: UserPermissions;
    public id: string;
    public disable_reason: number;
    public business_country_code: string;
    public spend_cap: string;
    public amount_spent: string;
    public all_payment_methods: AllPaymentMethods;
}

class AllPaymentMethods {
    public payment_method_tokens: any;
    public pm_credit_card: any;
    public payment_method_direct_debits: any;
    public payment_method_paypal: any;
}

class TotalPrepayBalance {
    public amount: string;
    public amount_in_hundredths: string;
    public currency: string;
    public offsetted_amount: string;
}

class AdsPaymentCycle {
    public data: DataAdsPaymentCycle[];
}

class DataAdsPaymentCycle {
    public threshold_amount: number;
}

class Insights {
    public data: DataInsights[];
}

class DataInsights {
    public spend: string;
    public date_start: string;
    public date_stop: string;
}

class IdNameInformation {
    public id: string;
    public name: string;
}

class UserRoles {
    public data: DataUserRoles[];
}

class DataUserRoles {
    public user: IdNameInformation;
    public business: IdNameInformation;
    public role: string;
    public tasks: string[];
    public status: string;
}

class UserPermissions {
    public data: DataUserPermissions[];
}

class DataUserPermissions {
    public user: IdNameInformation;
    public role: string;
}
