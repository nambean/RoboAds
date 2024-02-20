<template>
  <div>
    <div :class="isVerticalMenuActive ? (isVerticalMenuCollapsed ? 'ads-check-container menu-collapsed' : 'ads-check-container menu-expanded'): 'ads-check-container menu-disabled'">
      <div class="row">
        <b-col cols="12">
          <div class="card">
            <div class="card-body">
              <b-row>
                <b-col
                  cols="12"
                  md="3"
                  class="px-50"
                >
                  <b-form-input
                    v-model="adsAccountSearch.search"
                    placeholder="Tìm kiếm..."
                  />
                </b-col>
                <b-col
                  cols="12"
                  md="3"
                  class="mt-1 mt-md-0 px-50"
                >
                  <date-range-picker
                    ref="picker"
                    v-model="dateRange"
                    :opens="'left'"
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
                        <feather-icon
                          icon="CalendarIcon"
                          size="16"
                        />
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
                  md="3"
                  class="mt-1 mt-md-0 px-50"
                >
                  <div class="d-flex justify-content-end">
                    <b-dropdown
                      class="mr-50"
                      variant="outline-success"
                      :text="'$ ' + selectedCurrency"
                    >
                      <b-dropdown-item @click="calculatorByExchangeRates('MIX')">
                        MIX
                      </b-dropdown-item>
                      <b-dropdown-item @click="calculatorByExchangeRates('USD')">
                        USD
                      </b-dropdown-item>
                      <b-dropdown-item @click="calculatorByExchangeRates('VND')">
                        VND
                      </b-dropdown-item>
                    </b-dropdown>
                    <b-button
                      v-ripple.400="'rgba(255, 255, 255, 0.15)'"
                      class="btn-md text-right"
                      variant="primary"
                      @click="getAdsAccountList()"
                    >
                      <span class="align-middle">
                        <feather-icon
                          icon="SearchIcon"
                          class="mr-50 d-md-none d-lg-inline"
                        />
                        Tra cứu
                      </span>
                    </b-button>
                  </div>
                </b-col>
              </b-row>
            </div>
          </div>
        </b-col>
      </div>
      <div class="table-container">
        <div class="table-wrapper">
          <div
            class="table-header font-weight-bolder"
            :style="{ minWidth: tableMinWidth + 'px', width: '100%'}"
          >
            <div class="table-column column-status">
              Trạng thái
            </div>
            <div class="table-column column-account">
              Tài khoản
            </div>
            <div class="table-column w-130">
              Số dư
            </div>
            <div class="table-column w-130">
              Ngưỡng
            </div>
            <div class="table-column w-130">
              Limit
            </div>
            <div class="table-column w-130">
              Tổng tiêu
            </div>
            <div class="table-column w-130">
              Tiền tệ
            </div>
            <div class="table-column w-170">
              Quyền sở hữu
            </div>
            <div class="table-column w-170">
              PT thanh toán
            </div>
            <div class="table-column w-170">
              Ngày lập hóa đơn
            </div>
            <div class="table-column w-200">
              Lý do bị khóa
            </div>
            <div class="table-column w-200">
              Ngày tạo tài khoản
            </div>
            <div class="table-column w-170">
              <div class="text-sort is-asc-null">
                Loại tài khoản
              </div>
            </div>
            <div class="table-column w-200">
              Múi giờ tài khoản
            </div>
            <div class="table-column w-170">
              Được tạo từ BM
            </div>
            <div class="table-column w-170">
              Quốc gia TKQC
            </div>
          </div>

          <template
            v-for="adsAccount in adsAccountList"
          >
            <div
              class="table-data cursor-pointer"
              :style="{ minWidth: tableMinWidth + 'px', width: '100%'}"
              @click="getCampaigns(adsAccount)"
            >
              <div
                class="table-cell ad763 ab187 column-status"
                :class="convertStatus(adsAccount.accountStatus).color"
              >
                <div class="d-flex align-items-center">
                  <div class="ebd55" />
                  <div class="ml-50 font-weight-bold">
                    {{ convertStatus(adsAccount.accountStatus).label }}
                  </div>
                </div>
              </div>
              <div class="table-cell ab187 b4c1b column-account">
                <div class="flex-between w-full">
                  <div>
                    <span class="font-weight-bold">{{ adsAccount.accountName }}</span><br>
                    <span class="font-small-3 mt-25 font-weight-normal">{{ adsAccount.accountId }}</span>
                  </div>
                </div>
              </div>
              <div class="table-cell ab187 w-130">
                <div class="text-nowrap hover-underline">
                  {{ formatNumber(adsAccount.balance / 100) }}
                </div>
              </div>
              <div class="table-cell ab187 w-130">
                <div class="text-nowrap hover-underline">
                  {{ formatNumber(adsAccount.thresholdAmount / 100) }}
                </div>
              </div>
              <div class="table-cell ab187 w-130">
                <div class="text-nowrap">
                  <div v-if="adsAccount.adTrustDsl !== -1">
                    {{ formatNumber(adsAccount.adTrustDsl) }}
                  </div>
                  <div v-if="adsAccount.adTrustDsl === -1">
                    No Limit
                  </div>
                </div>
              </div>
              <div class="table-cell ab187 w-130">
                <div class="text-nowrap">
                  {{ formatNumber(adsAccount.spend) }}
                </div>
              </div>
              <div class="table-cell ab187 w-130">
                <div class="text-nowrap">
                  {{ adsAccount.currency }}
                </div>
              </div>
              <div class="table-cell ab187 w-170">
                <div class="text-nowrap">
                  {{ convertUserRole(adsAccount.userRoles) }}
                </div>
              </div>
              <div class="table-cell dd2ae ab187 w-170">
                <div>
                  <div class="flex flex-row">
                    <template
                      v-for="(pm, index) in JSON.parse(adsAccount.paymentMethodTokens)"
                    >
                      <div v-if="index === 0">
                        {{ pm.type }}
                      </div>
                    </template>
                    <template
                      v-for="(pm, index) in JSON.parse(adsAccount.pmCreditCard)"
                    >
                      <div v-if="index === 0">
                        {{ pm.display_string }}
                      </div>
                    </template>
                    <template
                      v-for="(pm, index) in JSON.parse(adsAccount.paymentMethodPaypal)"
                    >
                      <div v-if="index === 0">
                        {{ pm.email_address }}
                      </div>
                    </template>
                  </div>
                </div>
              </div>
              <div class="table-cell ab187 w-170">
                <div class="text-nowrap">
                  {{ adsAccount.nextBillDate ? formatDateTime(adsAccount.nextBillDate) : '' }}
                </div>
              </div>
              <div class="table-cell ab187 w-200">
                <div class="text-nowrap">
                  {{ convertDisableReason(adsAccount.disableReason) }}
                </div>
              </div>
              <div class="table-cell ab187 w-200">
                <div class="text-nowrap">
                  {{ adsAccount.createTime ? formatDateTime(adsAccount.createTime): '' }}
                </div>
              </div>
              <div class="table-cell ab187 w-170">
                <div class="text-nowrap">
                  {{ convertAccountType(adsAccount.ownerBusinessId) }}
                </div>
              </div>
              <div class="table-cell ab187 w-200">
                <div class="text-nowrap">
                  <div>{{ `${ adsAccount.timezoneName ? adsAccount.timezoneName: '' }  |  ${ adsAccount.timezoneOffsetHoursUtc ? adsAccount.timezoneOffsetHoursUtc: '' }` }}</div>
                </div>
              </div>
              <div class="table-cell ab187 w-170">
                <div class="text-nowrap">
                  {{ adsAccount.ownerBusinessId ? adsAccount.ownerBusinessId : '-' }}
                </div>
              </div>
              <div class="table-cell ab187 w-170">
                <div class="text-nowrap">
                  {{ adsAccount.businessCountryCode ? adsAccount.businessCountryCode: '-' }}
                </div>
              </div>
            </div>
          </template>
          <div :style="{ minWidth: tableMinWidth + 'px', height: '100%', background: 'rgb(255, 255, 255)', width: '100%'}" />
          <div
            class="table-footer font-weight-bold"
            :style="{ minWidth: tableMinWidth + 'px'}"
          >
            <div class="table-cell column-status" />
            <div class="table-cell column-account">
              <div>{{ adsAccountList.length }} Tài khoản quảng cáo</div>
            </div>
            <div class="table-cell w-130">
              {{ summary.balance }}
            </div>
            <div class="table-cell w-130">
              {{ summary.thresholdAmount }}
            </div>
            <div class="table-cell w-130">
              {{ summary.adTrustDsl }}
            </div>
            <div class="table-cell w-130">
              {{ summary.spend }}
            </div>
            <div class="table-cell w-130" />
            <div class="table-cell w-170" />
            <div class="table-cell w-170" />
            <div class="table-cell w-170" />
            <div class="table-cell w-200" />
            <div class="table-cell w-200" />
            <div class="table-cell w-170" />
            <div class="table-cell w-200" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {
  BButton, BCol, BDropdown, BDropdownItem, BFormInput, BRow,
} from 'bootstrap-vue'
import Ripple from 'vue-ripple-directive'
import DateRangePicker from 'vue2-daterange-picker'
import {
  calculatorFixedHeaderTableHeight,
  convertObjectToParam,
  dateRangeObject,
  formatDateTime,
  formatNumber,
} from '@core/utils/utils'
import moment from 'moment'
import { useWindowSize } from '@vueuse/core'
import { onUnmounted, watch } from '@vue/composition-api'
import useVerticalNavMenu from '@core/layouts/layout-vertical/components/vertical-nav-menu/useVerticalNavMenu'
import useAppConfig from '@core/app-config/useAppConfig'
import useVerticalLayout from '@core/layouts/layout-vertical/useVerticalLayout'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'AdsAccount',
  components: {
    BRow,
    BCol,
    BButton,
    BFormInput,
    DateRangePicker,
    BDropdown,
    BDropdownItem,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      isLoading: true,
      dateRange: {
        startDate: null,
        endDate: null,
      },
      dateRangeObject: dateRangeObject(),
      adsAccountSearch: {
        search: null,
        cookieId: undefined,
        page: 0,
        limit: 50,
        totalItems: 0,
      },
      fieldsAdsAccountList: [
        {
          key: 'accountStatus', label: 'Trạng thái', stickyColumn: true, isRowHeader: true,
        },
        {
          key: 'accountName', label: 'Tài khoản', stickyColumn: true, isRowHeader: true,
        },
        {
          key: 'balance', label: 'Số dư', formatter: val => formatNumber(val / 100), class: 'text-right',
        },
        {
          key: 'thresholdAmount', label: 'Ngưỡng', formatter: val => formatNumber(val / 100), class: 'text-right',
        },
        {
          key: 'adTrustDsl', label: 'Limit', class: 'text-right',
        },
        {
          key: 'spend', label: 'Tổng tiêu', formatter: val => formatNumber(val), class: 'text-right',
        },
        { key: 'currency', label: 'Tiền tệ', class: 'text-right pl-3' },
        { key: 'userRoles', label: 'Quyền sở hữu', class: 'text-right pl-3' },
        {
          key: 'nextBillDate',
          label: 'Ngày lập hóa đơn',
          formatter: val => moment(val).format('DD/MM/YYYY'),
          class: 'text-right',
        },
        {
          key: 'createTime',
          label: 'Ngày tạo tài khoản',
          formatter: val => moment(val).format('DD/MM/YYYY'),
          class: 'text-right',
        },
        {
          key: 'accountType', label: 'Loại tài khoản', class: 'text-right pl-3',
        },
        {
          key: 'timeZone', label: 'Múi giờ tài khoản', class: 'text-right pl-3',
        },
        {
          key: 'bmCreateBy', label: 'Được tạo từ BM', class: 'text-right pl-3',
        },
        { key: 'actions', label: '' },
      ],
      adsAccountList: [],
      positiveHeight: 150,
      tableMinWidth: 0,
      selectedCurrency: 'MIX',
      summary: {
        balance: 'MIX',
        thresholdAmount: 'MIX',
        adTrustDsl: 'MIX',
        spend: 'MIX',
      },
      rates: {},
    }
  },
  setup(props) {
    const {
      isVerticalMenuCollapsed,
    } = useVerticalNavMenu(props)

    const {
      navbarType,
      footerType,
    } = useAppConfig()

    // Vertical Menu
    const {
      resizeHandler,
      isVerticalMenuActive,
    } = useVerticalLayout(navbarType, footerType)

    // Resize handler
    resizeHandler()
    window.addEventListener('resize', resizeHandler)
    onUnmounted(() => {
      window.removeEventListener('resize', resizeHandler)
    })

    return {
      formatDateTime,
      formatNumber,
      calculatorFixedHeaderTableHeight,
      isVerticalMenuCollapsed,
      isVerticalMenuActive,
    }
  },
  async created() {
    const { height: windowHeight } = useWindowSize()
    watch(windowHeight, () => {
      calculatorFixedHeaderTableHeight(this.positiveHeight)
    })

    const {
      cookieId, startDate, endDate, page, search,
    } = this.$route.query

    if (!cookieId) {
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

    if (page) {
      this.adsAccountSearch.page = Number(page)
    }

    if (search) {
      this.adsAccountSearch.search = search
    }

    const exchangeRates = await this.getExchangeRate()
    this.rates = exchangeRates.rates

    this.adsAccountSearch.cookieId = cookieId
    this.getAdsAccountList()
  },
  methods: {
    calculatorByExchangeRates(currency) {
      this.selectedCurrency = currency

      if (currency === 'MIX') {
        this.summary = {
          balance: 'MIX',
          thresholdAmount: 'MIX',
          adTrustDsl: 'MIX',
          spend: 'MIX',
        }
        return
      }

      if (!this.rates[currency]) {
        return
      }

      this.summary.spend = this.calculatorSumaryByField(this.adsAccountList, this.rates, currency, 'spend')
      this.summary.balance = this.calculatorSumaryByField(this.adsAccountList, this.rates, currency, 'balance')
      this.summary.adTrustDsl = this.calculatorSumaryByField(this.adsAccountList, this.rates, currency, 'adTrustDsl')
      this.summary.thresholdAmount = this.calculatorSumaryByField(this.adsAccountList, this.rates, currency, 'thresholdAmount')
    },
    calculatorSumaryByField(list, rates, currency, field) {
      const summary = list.reduce((x1, x2) => {
        const currencyRate = rates[x2.currency]
        const selectedRate = rates[currency]
        return x1 + (x2[field] === -1 ? 0 : (x2[field] / currencyRate) * selectedRate)
      }, 0)

      if (Number.isNaN(summary)) {
        return 'MIX'
      }

      if (field === 'balance' || field === 'thresholdAmount') {
        return formatNumber(Math.floor(summary / 100))
      }

      if (currency === 'USD') {
        return formatNumber(summary)
      }

      return formatNumber(Math.floor(summary))
    },
    getExchangeRate() {
      return new Promise((resole, reject) => {
        const requestURL = 'https://api.exchangerate.host/latest?base=USD'
        const request = new XMLHttpRequest()
        request.open('GET', requestURL)
        request.responseType = 'json'

        request.onload = () => {
          resole(request.response)
        }

        request.onerror = () => {
          reject(request)
        }

        request.send()
      })
    },
    calculatorTableWidth() {
      const tableColumn = document.getElementsByClassName('table-column')
      let tableWidth = 0
      tableColumn.forEach(x => {
        tableWidth += x.offsetWidth
      })
      this.tableMinWidth = tableWidth
    },
    onChangePagination(value) {
      this.adsAccountSearch.page = value
      this.getAdsAccountList()
    },
    getAdsAccountList() {
      this.isLoading = true

      if (this.dateRange.startDate && this.dateRange.endDate) {
        this.adsAccountSearch.startDate = moment(this.dateRange.startDate).format('yyyy-MM-DD')
        this.adsAccountSearch.endDate = moment(this.dateRange.endDate).format('yyyy-MM-DD')
      }

      const queryParam = convertObjectToParam(this.adsAccountSearch)
      this.$router.replace({ name: 'ads-account', query: this.adsAccountSearch })

      useJwt.axiosIns.get(`/user-ads/getAdsAccount?${queryParam}`).then(response => {
        this.adsAccountList = response.data.items
        this.adsAccountSearch.totalItems = response.data.meta.totalItems
        this.isLoading = false
        calculatorFixedHeaderTableHeight(this.positiveHeight)
        this.calculatorTableWidth()
      })
    },
    getCampaigns(item) {
      this.$router.push({ name: 'campaign', query: { cookieId: item.cookieId, accountId: item.accountId } })
    },
    convertStatus(status) {
      const dataReturn = {
        label: 'Chưa xác định',
      }

      if (status === 1) {
        dataReturn.label = 'Hoạt động'
        dataReturn.color = 'text-success'
      } else if (status === 2) {
        dataReturn.label = 'Vô hiệu hóa'
        dataReturn.color = 'text-danger'
      } else if (status === 3) {
        dataReturn.label = 'Cần thanh toán'
        dataReturn.color = 'text-warning'
      } else if (status === 101) {
        dataReturn.label = 'Đóng'
      }

      return dataReturn
    },
    convertUserRole(role) {
      if (role === 'ADMIN') return 'Quản trị viên'
      if (role === 'REPORTS_ONLY') return 'Nhà phân tích'
      return role
    },
    convertDisableReason(disableReason) {
      if (disableReason === 3) return 'RISK PAYMENT'
      if (disableReason === 9) return 'UNUSED ACCOUNT'
      return '-'
    },
    convertAccountType(owner) {
      if (owner) return 'Business'
      return 'Cá nhân thường'
    },
  },
}
</script>
<style>
</style>

<style lang="scss">
@import 'src/@core/scss/vue/libs/vue-flatpicker.scss';
@import 'src/@core/scss/vue/libs/vue-select.scss';
@import 'src/@core/scss/vue/pages/ads-account.scss';
</style>

<style lang="scss" scoped>
.cookie-class {
  white-space: nowrap;
  max-width: 250px;
  overflow: hidden;
  text-overflow: ellipsis;
}

.vue-daterange-picker {
  width: 100%;
}

.ebd55 {
  background: currentColor;
  border-radius: 50%;
  -ms-flex-negative: 0;
  flex-shrink: 0;
  height: 7px;
  width: 7px;
}

.sticky-column {
  position: sticky;
  background-color: #fff;
  z-index: 1;
}
</style>
