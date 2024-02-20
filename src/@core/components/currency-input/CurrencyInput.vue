<template>
  <div>
    <b-form-input
      v-model="displayValue"
      class="form-control"
      type="text"
      autocomplete="new-password"
      @blur="isInputActive = false"
      @focus="isInputActive = true"
    />
  </div>
</template>

<script>
import {
  BFormInput,
} from 'bootstrap-vue'

export default {
  name: 'CurrencyInput',
  components: {
    BFormInput,
  },
  props: {
    value: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      isInputActive: false,
    }
  },
  computed: {
    displayValue: {
      get() {
        if (!this.value) {
          return null
        }

        if (this.isInputActive) {
          return this.value.toString()
        }

        return this.value.toString().replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, '$1,')
      },
      set(modifiedValue) {
        let newValue = parseFloat(modifiedValue.replace(/[^\d.]/g, ''))
        if (Number.isNaN(newValue)) {
          newValue = 0
        }
        this.$emit('input', newValue)
      },
    },
  },
}
</script>
