<template>
  <div>
    <b-row class="mb-1">
      <b-col>
        <date-range-picker
          ref="picker"
          v-model="dateRange"
          :opens="'left'"
          :locale-data="{
            firstDay: 1, format: 'dd/mm/yyyy'
          }"
          :append-to-body="true"
          :ranges="dateRangeObject"
          class="float-right"
          @update="getAllDashboardData()"
        >
          <template
            v-slot:input="picker"
            style="min-width: 350px;"
          >
            <div style="margin-top: 2px">
              <feather-icon icon="CalendarIcon" />
              <span v-if="picker.startDate">
                {{ formatDate(picker.startDate) }} - {{ formatDate(picker.endDate) }}
              </span>
              <b class="caret" />
            </div>
          </template>
        </date-range-picker>
      </b-col>
    </b-row>
    <b-row
      class="match-height"
    >
      <b-col
        v-for="(item, index) in userStats"
        :key="index"
        cols="12"
        md="6"
        lg="4"
        class="cursor-pointer"
      >
        <statistic-card-info-with-area-chart
          :color="item.color"
          :title="item.title"
          :value="item.value"
          :percent="item.percent"
          :is-grow="item.isGrow"
        />
      </b-col>
    </b-row>
    <b-row class="match-height">
      <b-col
        cols="12"
      >
        <div class="card">
          <div class="card-body">
            <h5
              class="card-title header-title text-uppercase cursor-pointer"
            >
              Doanh số
            </h5>
            <div>
              <vue-apex-charts
                class="apex-charts"
                type="area"
                height="295"
                :options="salesChart.options"
                :series="salesChart.series"
              />
            </div>
          </div>
        </div>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import {
  BRow, BCol,
} from 'bootstrap-vue'
import {
  convertObjectToParam,
  convertToInternationalCurrencySystem,
  dateRangeObject,
  formatDateTime,
} from '@core/utils/utils'
import DateRangePicker from 'vue2-daterange-picker'
import VueApexCharts from 'vue-apexcharts'
import StatisticCardInfoWithAreaChart from '@core/components/statistics-cards/StatisticCardInfoWithAreaChart.vue'
import moment from 'moment'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'AdminDashboard',
  components: {
    VueApexCharts,
    StatisticCardInfoWithAreaChart,
    BRow,
    BCol,
    DateRangePicker,
  },
  data() {
    const today = new Date()
    today.setHours(0, 0, 0, 0)
    const thisMonthStart = new Date(today.getFullYear(), today.getMonth(), 1, 0, 0, 0, 0)
    const thisMonthEnd = new Date(today.getFullYear(), today.getMonth() + 1, 0, 23, 59, 59, 0)

    const fromDate = new Date()
    fromDate.setHours(0, 0, 0, 0)
    const toDate = new Date()
    toDate.setHours(23, 59, 59, 0)
    return {
      salesChart: {
        series: [],
        options: {
          chart: {
            type: 'area',
            zoom: { enabled: false },
            toolbar: { show: false },
            stacked: true,
          },
          grid: { show: true },
          dataLabels: { enabled: false },
          stroke: {
            curve: 'smooth', width: 1.8,
          },
          legend: {
            show: true,
            position: 'top',
            horizontalAlign: 'right',
          },
          xaxis: {
            type: 'datetime',
            tooltip: {
              enabled: false,
            },
            axisBorder: {
              show: false,
            },
            labels: {
              formatter(val) {
                return formatDateTime(val)
              },
            },
          },
          yaxis: {
            labels: {
              formatter(val) {
                return convertToInternationalCurrencySystem(val)
              },
            },
            title: { text: 'VND' },
          },
          tooltip: {
            x: {
              show: true,
              formatter(val) {
                return formatDateTime(val)
              },
            },
            theme: 'dark',
          },
          fill: {
            type: 'gradient',
            gradient: {
              type: 'vertical',
              shadeIntensity: 1,
              inverseColors: false,
              opacityFrom: 0.45,
              opacityTo: 0.05,
              stops: [45, 100],
            },
          },
        },
      },
      commissionChart: {},
      transactionChart: {},
      dateRangeObject: dateRangeObject(),
      dateRange: {
        startDate: thisMonthStart,
        endDate: thisMonthEnd,
      },
      userStats: [
        {
          title: 'User hoạt động', value: 0, isGrow: true, percent: 0, color: 'warning',
        },
        {
          title: 'Số lượng Cookie', value: 0, isGrow: true, percent: 0, color: 'info',
        },
        {
          title: 'Tài khoản Ads', value: 0, isGrow: true, percent: 0, color: 'success',
        },
      ],
    }
  },
  created() {
    this.getAllDashboardData()
  },
  methods: {
    getAllDashboardData() {
      this.getUserDataInfo()
    },
    getUserDataInfo() {
      if (this.dateRange.startDate && this.dateRange.endDate) {
        this.dateRange.startDate = moment(this.dateRange.startDate).format('yyyy-MM-DD')
        this.dateRange.endDate = moment(this.dateRange.endDate).format('yyyy-MM-DD')
      }
      useJwt.axiosIns.get(`/dashboard/getDashboardInfo?${convertObjectToParam(this.dateRange)}`).then(res => {
        this.userStats = res.data
      })
    },
    formatDate(date) {
      return formatDateTime(date)
    },
  },
}
</script>

<style type="text/css" scoped>
</style>
