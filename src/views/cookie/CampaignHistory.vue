<template>
  <div>
    <div class="mb-1 mt-2 d-inline-flex">
      <h2 class="font-weight-normal">
        Lịch sử chiến dịch - {{ cookieList.length > 0 ? cookieList[0].name : '' }}
      </h2>
    </div>

    <b-card>
      <b-row>
        <b-col
          cols="12"
          md="4"
          lg="3"
          class="mt-1 mt-md-0"
        >
          <date-range-picker
            ref="picker"
            v-model="dateRange"
            class="w-100"
            :opens="'right'"
            :locale-data="{
              firstDay: 1, format: 'dd/mm/yyyy'
            }"
            :append-to-body="true"
            :ranges="dateRangeObject"
          >
            <template
              v-slot:input="picker"
              style="min-width: 350px;"
            >
              <div style="margin-top: 2px">
                <feather-icon icon="CalendarIcon" />
                <span v-if="picker.startDate">
                  {{ formatDateTime(picker.startDate) }} - {{ formatDateTime(picker.endDate) }}
                </span>
                <b class="caret" />
              </div>
            </template>
          </date-range-picker>
        </b-col>
        <b-col
          cols="12"
          md="2"
          class="mt-1 mt-md-0"
        >
          <b-button
            v-ripple.400="'rgba(255, 255, 255, 0.15)'"
            class="btn-m"
            variant="primary"
            @click="reportAction()"
          >
            <span class="align-middle">
              <feather-icon
                icon="ClockIcon"
                class="mr-50 d-md-none d-lg-inline"
              />
              Lịch sử
            </span>
          </b-button>
        </b-col>
      </b-row>
    </b-card>
    <b-row>
      <b-col cols="12">
        <b-card>
          <b-table
            class="table-transaction-history table-border-dashed table-hover-animation text-nowrap"
            responsive
            sticky-header
            :hover="true"
            :fields="fieldsCookieList"
            :items="cookieList"
          >
            <template #cell(stt)="data">
              {{ (campaignSearch.page - 1) * campaignSearch.limit + data.index + 1 }}
            </template>
            <template #cell(results)="data">
              <div class="d-block">
                <div>{{ formatNumber(data.item.results) }}</div>
                <div>{{ data.item.resultIndicatorStr }}</div>
              </div>
            </template>
          </b-table>
        </b-card>
        <div class="mx-2 mb-2">
          <b-row>
            <b-col
              cols="12"
              sm="12"
              class="d-flex align-items-center justify-content-center justify-content-sm-end"
            >
              <b-pagination
                v-if="!isLoading"
                v-model="campaignSearch.page"
                :total-rows="campaignSearch.totalItems"
                :per-page="campaignSearch.limit"
                first-number
                last-number
                class="mb-0 mt-1 mt-sm-0"
                prev-class="prev-item"
                next-class="next-item"
                @change="onChangePagination($event)"
              >
                <template #prev-text>
                  <feather-icon
                    icon="ChevronLeftIcon"
                    size="18"
                  />
                </template>
                <template #next-text>
                  <feather-icon
                    icon="ChevronRightIcon"
                    size="18"
                  />
                </template>
              </b-pagination>
            </b-col>
          </b-row>
        </div>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BTable, BButton, BPagination,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import DateRangePicker from 'vue2-daterange-picker'
import {
  convertObjectToParam, dateRangeObject, formatNumber, formatDateTime,
} from '@core/utils/utils'
import moment from 'moment'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'CampaignHistory',
  components: {
    BRow,
    BCol,
    BCard,
    BTable,
    BButton,
    BPagination,
    DateRangePicker,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      isLoading: true,
      dateRangeObject: dateRangeObject(),
      dateRange: {
        startDate: null,
        endDate: null,
      },
      campaignSearch: {
        cookieId: undefined,
        campaignId: undefined,
        page: 1,
        limit: 10,
        totalItems: 0,
      },
      fieldsCookieList: [
        { key: 'stt', label: 'STT', stickyColumn: true },
        {
          key: 'createdAt',
          label: 'Thời gian xử lý',
          stickyColumn: true,
          formatter: val => moment(val).format('DD/MM/YYYY HH:mm:ss'),
        },
        // { key: 'dateStart', label: 'Bắt đầu' },
        // { key: 'dateStop', label: 'Kết thúc' },
        { key: 'results', label: 'Results' },
        {
          key: 'reach',
          label: 'Reach',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'frequency',
          label: 'Frequency',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'spend',
          label: 'Amount Spent',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'costPerImpressions',
          label: 'cpm',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'linkClick',
          label: 'Link Clicks',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'costPerLinkClick',
          label: 'CPC',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'linkClickThroughRate',
          label: 'CTR',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'clicksAll',
          label: 'Click All',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'ctrAll',
          label: 'CTR All',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        {
          key: 'cpcAll',
          label: 'CPC All',
          formatter: val => formatNumber(val),
          tdClass: 'text-right',
        },
        { key: 'actions', label: '' },
      ],
      allCampaignList: [],
      cookieList: [],
      userData: JSON.parse(localStorage.getItem('userData')),
    }
  },
  created() {
    const {
      cookieId, campaignId, startDate, endDate, page,
    } = this.$route.query

    if (!cookieId || !campaignId) {
      this.$router.push({ name: 'cookie' })
    }

    this.campaignSearch.cookieId = cookieId
    this.campaignSearch.campaignId = campaignId

    if (startDate && endDate) {
      this.dateRange.startDate = new Date(startDate)
      this.dateRange.endDate = new Date(endDate)
    } else {
      const today = new Date()
      today.setHours(0, 0, 0, 0)

      const thisYearStart = new Date(today.getFullYear(), 0, 1, 0, 0, 0, 0)
      const thisYearEnd = new Date(today.getFullYear(), 11, 31, 23, 59, 59, 0)

      this.dateRange.startDate = thisYearStart
      this.dateRange.endDate = thisYearEnd
    }

    if (page) {
      this.campaignSearch.page = Number(page)
    }

    this.getCampaignsHistoryList()
  },
  methods: {
    onChangePagination(value) {
      this.campaignSearch.page = value
      this.getCampaignsHistoryList()
    },
    reportAction() {
      this.campaignSearch.page = 1
      this.getCampaignsHistoryList()
    },
    getCampaignsHistoryList() {
      this.isLoading = true

      if (this.dateRange.startDate && this.dateRange.endDate) {
        this.campaignSearch.startDate = moment(this.dateRange.startDate).format('yyyy-MM-DD')
        this.campaignSearch.endDate = moment(this.dateRange.endDate).format('yyyy-MM-DD')
      }

      const queryParam = convertObjectToParam(this.campaignSearch)
      this.$router.replace({ name: 'campaign-history', query: this.campaignSearch })

      useJwt.axiosIns.get(`/user-ads/getCampaignHistory?${queryParam}`).then(response => {
        this.cookieList = response.data.items
        this.campaignSearch.totalItems = response.data.meta.totalItems
        this.isLoading = false
      })
    },
    formatNumber(val) {
      return formatNumber(val)
    },
    formatDateTime(val) {
      return formatDateTime(val)
    },
    campaignSearchDateOnChange(data) {
      if (data.length > 1) {
        this.campaignSearch.fromDate = data[0].toString()
        this.campaignSearch.toDate = data[1].toString()
      }
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/libs/vue-flatpicker.scss';
@import 'src/@core/scss/vue/libs/vue-select.scss';
</style>

<style lang="scss" scoped>
.b-table-sticky-header {
  max-height: 670px;
}
</style>
