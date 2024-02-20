<template>
  <div class="container">
    <div class="mb-1 mt-2 d-inline-flex">
      <h2 class="font-weight-normal">
        Danh sách cookie
      </h2>
    </div>

    <b-card>
      <b-row>
        <b-col
          cols="12"
          md="3"
          class="mt-1 mt-md-0"
        >
          <date-range-picker
            ref="picker"
            v-model="dateRange"
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
          md="3"
          class="mt-1 mt-md-0"
        >
          <v-select
            v-model="cookieSearch.useStatus"
            placeholder="Trạng thái sử dụng"
            :options="useStatusList"
            :reduce="item => item.value.toString()"
            label="text"
            item-value="value"
          />
        </b-col>
        <b-col
          cols="12"
          md="4"
          lg="3"
          class="mt-1 mt-md-0"
        >
          <div class="d-flex">
            <b-button
              v-ripple.400="'rgba(255, 255, 255, 0.15)'"
              class="btn-md"
              variant="success"
              @click="$refs.excelFile.click()"
            >
              <span class="align-middle">
                <feather-icon
                  icon="CloudIcon"
                  class="mr-50 d-md-none d-lg-inline"
                />
                Import
              </span>
            </b-button>
            <b-button
              v-ripple.400="'rgba(255, 255, 255, 0.15)'"
              class="btn-md ml-1"
              variant="primary"
              @click="getCookieList()"
            >
              <span class="align-middle">
                <feather-icon
                  icon="SearchIcon"
                  class="mr-50 d-md-none d-lg-inline"
                />
                Tra cứu
              </span>
            </b-button>
            <input
              ref="excelFile"
              class="d-none"
              type="file"
              accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
              @change="onChangeImportFile()"
            />
          </div>
        </b-col>
      </b-row>
    </b-card>
    <b-row>
      <b-col cols="12">
        <b-card>
          <b-table
            class="table-transaction-history table-border-dashed table-hover-animation"
            responsive
            :hover="true"
            :fields="fieldsCookieList"
            :items="cookieList"
          >
            <template #cell(cookie)="data">
              <div v-if="data.item.domain" class="font-weight-bolder mb-50">
                Domain: {{ data.item.domain }}
              </div>
              <div class="cookie-class">
                {{ data.item.cookie }}
              </div>
            </template>
            <template #cell(stt)="data">
              {{ (cookieSearch.page - 1) * cookieSearch.limit + data.index + 1 }}
            </template>
            <template #cell(runningStatus)="data">
              <b-badge
                class="px-1 py-50"
                pill
                :variant="parseRunningStatus(data.item.runningStatus).variant"
              >
                {{ parseRunningStatus(data.item.runningStatus).label }}
              </b-badge>
            </template>
            <template #cell(useStatus)="data">
              <b-badge
                class="px-1 py-50"
                pill
                :variant="parseUseStatus(data.item.useStatus).variant"
              >
                {{ parseUseStatus(data.item.useStatus).label }}
              </b-badge>
            </template>
            <template #cell(actions)="data">
              <b-button
                class="btn-sm"
                variant="outline-primary"
                style="height: 30px"
                @click="getUserAdsAccount(data.item)"
              >
                <feather-icon
                  icon="UsersIcon"
                />
              </b-button>
              <b-button
                class="btn-sm ml-50"
                variant="outline-danger"
                style="height: 30px"
                @click="deleteCookie(data.item)"
              >
                <feather-icon
                  icon="TrashIcon"
                />
              </b-button>
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
                v-model="cookieSearch.page"
                :total-rows="cookieSearch.totalItems"
                :per-page="cookieSearch.limit"
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
    <b-modal
      ref="modalImportExcelResult"
      title="Kết quả Import"
      ok-only
      size="lg"
    >
      <b-row>
        <b-col cols="12">
          <b-table
            class="table-transaction-history table-border-dashed table-hover-animation"
            responsive
            sticky-header
            :hover="true"
            :fields="fieldsImportExcelResult"
            :items="importExcelResult"
          >
            <template #cell(stt)="data">
              {{ data.index + 1 }}
            </template>
            <template #cell(cookie)="data">
              <div class="cookie-class">
                {{ data.value }}
              </div>
            </template>
            <template #cell(success)="data">
              <b-badge
                class="px-1 py-50"
                pill
                :variant="data.value ? 'light-success' : 'light-danger'"
              >
                {{ data.value ? 'Thành công' : 'Thất bại' }}
              </b-badge>
            </template>
          </b-table>
        </b-col>
      </b-row>
    </b-modal>
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BTable, BBadge, BPagination, BButton,
} from 'bootstrap-vue'
import vSelect from 'vue-select'
import Ripple from 'vue-ripple-directive'
import DateRangePicker from 'vue2-daterange-picker'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import {
  convertObjectToParam, formatNumber, formatDateTime, dateRangeObject,
} from '@core/utils/utils'
import moment from 'moment'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'Cookie',
  components: {
    BRow,
    BCol,
    BCard,
    BTable,
    BBadge,
    BPagination,
    BButton,
    vSelect,
    DateRangePicker,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      isLoading: true,
      dateRangeObject: dateRangeObject(),
      cookieSearch: {
        useStatus: undefined,
        page: 1,
        limit: 10,
        totalItems: 0,
      },
      dateRange: {
        startDate: null,
        endDate: null,
      },
      fieldsCookieList: [
        { key: 'stt', label: 'STT' },
        { key: 'ipAddress', label: 'Địa chỉ IP' },
        { key: 'cookie', label: 'Cookie' },
        { key: 'useStatus', label: 'Trạng thái' },
        { key: 'runningStatus', label: 'Hoạt động' },
        {
          key: 'createdAt', label: 'Thời gian thêm', formatter: val => moment(val).format('DD/MM/YYYY HH:mm'),
        },
        { key: 'actions', label: '' },
      ],
      fieldsImportExcelResult: [
        { key: 'stt', label: 'STT' },
        { key: 'ipAddress', label: 'IP' },
        { key: 'cookie', label: 'Cookie' },
        { key: 'message', label: 'Message', tdClass: 'white-space-pre-line' },
        {
          key: 'success', label: 'Trạng thái', thClass: 'text-nowrap text-center', tdClass: 'text-center',
        },
      ],
      importExcelResult: [],
      useStatusList: [
        { text: 'Chưa sử dụng', value: 0 },
        { text: 'Đã sử dụng', value: 1 },
      ],
      cookieList: [],
      userData: JSON.parse(localStorage.getItem('userData')),
    }
  },
  created() {
    const {
      startDate, endDate, page,
    } = this.$route.query

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
      this.cookieSearch.page = Number(page)
    }

    this.getCookieList()
  },
  methods: {
    onChangePagination(value) {
      this.cookieSearch.page = value
      this.getCookieList()
    },
    getCookieList() {
      this.isLoading = true

      if (this.dateRange.startDate && this.dateRange.endDate) {
        this.cookieSearch.startDate = moment(this.dateRange.startDate).format('yyyy-MM-DD')
        this.cookieSearch.endDate = moment(this.dateRange.endDate).format('yyyy-MM-DD')
      }

      const queryParam = convertObjectToParam(this.cookieSearch)
      this.$router.replace({ name: 'cookie', query: this.cookieSearch })

      useJwt.axiosIns.get(`/cookies/getList?${queryParam}`).then(response => {
        this.cookieList = response.data.items
        this.cookieSearch.totalItems = response.data.meta.totalItems
        this.isLoading = false
      })
    },
    formatNumber(val) {
      return formatNumber(val)
    },
    formatDateTime(val) {
      return formatDateTime(val)
    },
    getUserAdsAccount(item) {
      if (!item.useStatus) {
        this.$toast({
          component: ToastificationContent,
          position: 'top-right',
          props: {
            title: 'Thất bại',
            icon: 'XIcon',
            variant: 'danger',
            text: 'Cookie này chưa được sử dụng',
          },
        })
        return
      }
      this.$router.push({ name: 'ads-account', query: { cookieId: item.id } })
    },
    deleteCookie(item) {
      this.$swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        customClass: {
          confirmButton: 'btn btn-primary',
          cancelButton: 'btn btn-outline-danger ml-1',
        },
        buttonsStyling: false,
      }).then(result => {
        if (!result.value) {
          return
        }

        useJwt.axiosIns.post('/cookies/delete', item).then(() => {
          this.getCookieList()
        }).catch(error => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Failure',
              icon: 'XIcon',
              variant: 'danger',
              text: `${error.response.data.error}`,
            },
          })
        })
      })
    },
    onChangeImportFile() {
      const excelFile = this.$refs.excelFile.files[0]
      if (!excelFile) {
        return
      }

      // Xóa file
      this.$refs.excelFile.value = null

      const formData = new FormData()
      formData.append('cookie', excelFile)

      useJwt.axiosIns.post('/cookies/upload', formData).then(response => {
        if (response.data && response.data.data.length === 0) {
          return
        }

        this.importExcelResult = response.data.data
        this.$refs.modalImportExcelResult.show()
        this.getCookieList()
      })
    },
    parseRunningStatus(runningStatus) {
      const returnData = {
        variant: '',
        label: '',
      }

      if (runningStatus === 0) {
        returnData.variant = 'light-warning'
        returnData.label = 'Chưa hoạt động'
      }

      if (runningStatus === 1) {
        returnData.variant = 'light-success'
        returnData.label = 'Đang chạy'
      }

      if (runningStatus === 2) {
        returnData.variant = 'light-warning'
        returnData.label = 'Tạm dừng'
      }

      if (runningStatus === 3) {
        returnData.variant = 'light-danger'
        returnData.label = 'Ngưng hoạt động'
      }

      return returnData
    },
    parseUseStatus(useStatus) {
      const returnData = {
        variant: 'light-warning',
        label: 'Chưa sử dụng',
      }

      if (useStatus === 1) {
        returnData.variant = 'light-success'
        returnData.label = 'Đã sử dụng'
      }

      return returnData
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/libs/vue-flatpicker.scss';
@import 'src/@core/scss/vue/libs/vue-select.scss';
</style>

<style lang="scss" scoped>
.cookie-class {
  white-space: nowrap;
  max-width: 250px;
  overflow: hidden;
  text-overflow: ellipsis;
}

.b-table-sticky-header {
  max-height: 430px;
}

.vue-daterange-picker {
  width: 100%;
}
</style>
