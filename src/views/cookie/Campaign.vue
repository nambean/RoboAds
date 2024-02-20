<template>
  <div>
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
          md="5"
          class="mt-1 mt-md-0"
        >
          <v-select
            v-model="campaignSearch.campaignIds"
            placeholder="Chiến dịch"
            :options="allCampaignList"
            :reduce="item => item.id"
            label="name"
            item-value="id"
            multiple
          />
        </b-col>
        <b-col
          cols="12"
          md="2"
          class="mt-1 mt-md-0"
        >
          <div class="d-flex justify-content-end">
            <b-button
              v-ripple.400="'rgba(255, 255, 255, 0.15)'"
              class="btn-md"
              variant="primary"
              @click="getCampaignsList()"
            >
            <span class="align-middle">
              <feather-icon
                icon="FilterIcon"
                class="mr-50 d-md-none d-lg-inline"
              />
              Thống kê
            </span>
            </b-button>
          </div>
        </b-col>
      </b-row>
      <div class="mt-2">
        <b-table
          class="table-border-dashed table-hover-animation text-nowrap"
          responsive
          sticky-header
          :hover="true"
          :fields="fieldsCookieList"
          :items="cookieList"
          @row-clicked="routeToCampaignHistory"
        >
          <template #cell(stt)="data">
            {{ (pagination.page - 1) * pagination.limit + data.index + 1 }}
          </template>
          <template #cell(results)="data">
            <div class="d-block">
              <div>{{ formatNumber(data.item.results) }}</div>
              <div>{{ data.item.resultIndicatorStr }}</div>
            </div>
          </template>
        </b-table>
      </div>
    </b-card>
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BTable, BButton,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import Ripple from 'vue-ripple-directive'
import moment from 'moment'
import DateRangePicker from 'vue2-daterange-picker'
import {
  convertObjectToParam, dateRangeObject, formatNumber, formatDateTime, calculatorFixedHeaderTableHeight,
} from '@core/utils/utils'
import { useWindowSize } from '@vueuse/core'
import { watch } from '@vue/composition-api'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'Campaign',
  components: {
    BRow,
    BCol,
    BCard,
    BTable,
    BButton,
    vSelect,
    DateRangePicker,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      dateRange: {
        startDate: null,
        endDate: null,
      },
      dateRangeObject: dateRangeObject(),
      campaignSearch: {
        cookieId: undefined,
        accountId: undefined,
        campaignIds: [],
      },
      fieldsCookieList: [
        { key: 'stt', label: 'STT', stickyColumn: true },
        { key: 'name', label: 'Tên chiến dịch', stickyColumn: true },
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
      pagination: {
        page: 1,
        limit: 10,
        totalItems: 0,
      },
      userData: JSON.parse(localStorage.getItem('userData')),
    }
  },
  setup() {
    return { formatDateTime, formatNumber, calculatorFixedHeaderTableHeight }
  },
  async created() {
    const { height: windowHeight } = useWindowSize()
    watch(windowHeight, () => {
      calculatorFixedHeaderTableHeight(150)
    })

    const {
      cookieId, accountId, startDate, endDate,
    } = this.$route.query

    if (!cookieId || !accountId) {
      this.$router.push({ name: 'cookie' })
    }

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

    this.campaignSearch.cookieId = cookieId
    this.campaignSearch.accountId = accountId

    await this.getAllCampaign()
    this.getCampaignsList()
  },
  methods: {
    getCampaignsList() {
      if (this.dateRange.startDate && this.dateRange.endDate) {
        this.campaignSearch.startDate = moment(this.dateRange.startDate).format('yyyy-MM-DD')
        this.campaignSearch.endDate = moment(this.dateRange.endDate).format('yyyy-MM-DD')
      }

      const routerParam = this._.clone(this.campaignSearch)
      delete routerParam.campaignIds

      if (this.campaignSearch.campaignIds.length > 0) {
        routerParam.campaignIdList = JSON.stringify(this.campaignSearch.campaignIds)
      }

      const queryParam = convertObjectToParam(this.campaignSearch)
      this.$router.replace({ name: 'campaign', query: routerParam })

      useJwt.axiosIns.get(`/user-ads/getCampaign?${queryParam}`).then(response => {
        this.cookieList = response.data
      })
    },
    async getAllCampaign() {
      const queryParam = convertObjectToParam(this.campaignSearch)
      await useJwt.axiosIns.get(`/user-ads/allCampaign?${queryParam}`).then(response => {
        this.allCampaignList = response.data.map(x => {
          // eslint-disable-next-line no-param-reassign
          x.name = `${x.name.split(' ').slice(0, 4).join(' ')}...`
          return x
        })

        const { campaignIdList } = this.$route.query
        if (campaignIdList && campaignIdList.length > 0) {
          this.campaignSearch.campaignIds = JSON.parse(campaignIdList)
        }

        calculatorFixedHeaderTableHeight(150)
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
    routeToCampaignHistory(item) {
      this.$router.push({ name: 'campaign-history', query: { cookieId: item.cookieId, campaignId: item.campaignId } })
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
