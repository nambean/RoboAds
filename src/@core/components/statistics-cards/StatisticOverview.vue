<template>
  <div class="d-flex border-bottom px-2 py-1">
    <div class="flex-grow-1">
      <h4
        v-if="!isNaN(stats)"
        class="mt-0 mb-25 fs-22"
      >
        {{ formatNumber(stats) }}
      </h4>
      <h4
        v-if="isNaN(stats)"
        class="mt-0 mb-25 fs-22"
      >
        {{ stats }}
      </h4>
      <span class="text-muted">{{ title }}</span>
    </div>
    <feather-icon
      :class="iconClass"
      :icon="icon"
      size="32"
    />
  </div>
</template>

<script>
import { formatNumber } from '@core/utils/utils'

export default {
  name: 'StatisticOverview',
  props: {
    title: {
      type: String,
      default: 'Total Visitors',
    },
    stats: {
      type: Number,
      default: 0,
    },
    icon: {
      type: String,
      default: 'AwardIcon',
    },
    iconClass: {
      type: String,
      default: '',
    },
  },
  methods: {
    formatNumber(val) {
      return formatNumber(Number(val))
    },
  },
}
</script>

<style scoped>

</style>
