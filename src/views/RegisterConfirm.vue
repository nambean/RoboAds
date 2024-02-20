<template>
  <div class="auth-wrapper auth-v1 px-2">
    <div class="register py-2">
      <!-- Reset Password v1 -->
      <b-card class="p-2">

        <!-- logo -->
        <b-link class="brand-logo">
          <vuexy-logo />
        </b-link>
        <div class="text-left">
          <h2 class="mb-2">
            Xin chào {{ register.fullName }}
          </h2>
          <div class="font-medium-1 mb-1">
            Bạn vui lòng truy cập hòm thư {{ register.userEmail }} và bấm vào LINK KÍCH HOẠT mà AdsTools đã gửi, để hoàn tất đăng kí tài khoản
          </div>
          <div class="font-medium-1">
            Hoặc, liên hệ bộ phận CSKH <a href="mailto:no-reply@ads.vn">no-reply@ads.vn</a>
          </div>
        </div>
        <p class="text-center mt-5 mb-50">
          <span>Bạn đã xác thực tài khoản thành công? </span>
        </p>
        <div class="text-center">
          <b-link :to="{name:'auth-login'}">
            <feather-icon icon="ChevronLeftIcon" />
            Đăng nhập tại đây
          </b-link>
        </div>
      </b-card>
    </div>
  </div>

</template>

<script>
import VuexyLogo from '@core/layouts/components/Logo.vue'
import {
  BCard, BLink,
} from 'bootstrap-vue'

export default {
  name: 'RegisterConfirm',
  components: {
    VuexyLogo,
    BCard,
    BLink,
  },
  data() {
    return {
      register: {
      },
    }
  },
  created() {
    this.register = { ...this.$route.query }
    if (!this.register.fullName || !this.register.userEmail) {
      this.$router.push({ name: 'auth-login' })
    }
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-auth.scss';
</style>
