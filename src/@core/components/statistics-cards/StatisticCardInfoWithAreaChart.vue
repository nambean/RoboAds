<template>
  <b-card no-body>
    <b-card-body>
      <div class="d-flex">
        <div class="flex-grow-1">
          <span class="text-uppercase text-muted font-weight-bold">
            {{ title }}
          </span>
          <h3 class="mb-0 mt-75">
            {{ value }}
          </h3>
        </div>
        <div class="flex-shrink-0">
          <div class="pl-1">
            <vue-apex-charts
              type="area"
              height="45"
              width="90"
              :options="chartOptionsComputed"
              :series="chartDataComputed"
            />
          </div>
          <div :class="isGrow ? 'text-success': 'text-danger'">
            <feather-icon
              v-if="isGrow"
              icon="ArrowUpIcon"
            />
            <feather-icon
              v-if="!isGrow"
              icon="ArrowDownIcon"
            />
            <label
              class="font-weight-bolder fs-12 ml-25"
              :class="isGrow ? 'text-success': 'text-danger'"
            >
              {{ percent.toFixed(2) }}%
            </label>
          </div>
        </div>
      </div>
    </b-card-body>
  </b-card>
</template>

<script>
import {
  BCard, BCardBody,
} from 'bootstrap-vue'
import VueApexCharts from 'vue-apexcharts'
import { $themeColors } from '@themeConfig'
import { areaChartOptions } from './chartOptions'

export default {
  components: {
    VueApexCharts,
    BCard,
    BCardBody,
  },
  props: {
    title: {
      type: String,
      default: '',
    },
    color: {
      type: String,
      default: 'success',
    },
    chartData: {
      type: Array,
      default: () => [],
    },
    chartOptions: {
      type: Object,
      default: null,
    },
    percent: {
      type: Number,
      default: 0,
    },
    isGrow: {
      type: Boolean,
      default: false,
    },
    value: {
      type: Number,
      default: 0,
    },
  },
  computed: {
    chartDataComputed() {
      if (this.chartData.length === 0) {
        return [{
          name: this.statisticTitle,
          data: [0, ...Array.from({ length: 8 }, () => Math.floor(Math.random() * 100))],
          // data: [25, 66, 41, 85, 63, 25, 44, 12, 36, 9, 54],
        }]
      }

      return this.chartData
    },
    chartOptionsComputed() {
      if (this.chartOptions === null) {
        const options = JSON.parse(JSON.stringify(areaChartOptions))
        options.tooltip.enabled = false
        options.theme.monochrome.color = $themeColors[this.color]
        return options
      }
      return this.chartOptions
    },
  },
}
</script>
