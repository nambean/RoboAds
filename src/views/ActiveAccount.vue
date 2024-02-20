<template>
  <div class="auth-wrapper auth-v1 px-2">
    <div class="auth-inner py-2">
      <!-- Reset Password v1 -->
      <b-card class="mb-0">

        <!-- logo -->
        <b-link class="brand-logo">
          <vuexy-logo />

        </b-link>
        <div class="text-center mb-2">
          <b-button
            :variant="outline"
            class="btn-icon rounded-circle"
          >
            <feather-icon
              :icon="icon"
              size="50"
            />
          </b-button>
        </div>

        <b-card-text class="mb-2">
          {{ message }}
        </b-card-text>

        <p class="text-center mt-2">
          <b-link :to="{name:'auth-login'}">
            <feather-icon icon="ChevronLeftIcon" /> Back to login
          </b-link>
        </p>

      </b-card>
    </div>
  </div>

</template>

<script>
import VuexyLogo from '@core/layouts/components/Logo.vue'
import {
  BCard, BCardText, BLink, BButton,
} from 'bootstrap-vue'

export default {
  components: {
    VuexyLogo,
    BCard,
    BCardText,
    BLink,
    BButton,
  },
  data() {
    return {
      message: '',
      icon: '',
      outline: '',
    }
  },
  created() {
    const postData = { ...this.$route.query }

    this.$http.post('/user/confirm', postData).then(data => {
      this.outline = 'outline-success'
      this.icon = 'CheckIcon'
      this.message = data.data.message
    }).catch(error => {
      this.outline = 'outline-danger'
      this.icon = 'XIcon'
      this.message = error.response.data.error
    })
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-auth.scss';
</style>
