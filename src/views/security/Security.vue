<template>
  <div class="container px-0">
    <b-card class="px-md-2">
      <h4 class="my-2">
        Xác thực hai yếu tố (2FA</h4>
      <GoogleAuthenticator
        :hidden-email="hiddenEmail"
        :is-google-enabled="isEnableGoogle2FA"
      />
      <SmsAuthenticator
        :hidden-email="hiddenEmail"
        :is-sms-enabled="isEnableSMS2FA"
      />
      <EmailAuthenticator :hidden-email="hiddenEmail" />
      <h4 class="mt-4 mb-2">
        Bảo mật tài khoản
      </h4>
      <ChangePassword />
      <ChangePIN />
    </b-card>
  </div>
</template>

<script>
import {
  BCard,
} from 'bootstrap-vue'

import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import GoogleAuthenticator from '@/views/security/GoogleAuthenticator.vue'
import SmsAuthenticator from '@/views/security/SmsAuthenticator.vue'
import EmailAuthenticator from '@/views/security/EmailAuthenticator.vue'
import ChangePassword from '@/views/security/ChangePassword.vue'
import ChangePIN from '@/views/security/ChangePIN.vue'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'Security',
  components: {
    ChangePIN,
    ChangePassword,
    EmailAuthenticator,
    SmsAuthenticator,
    GoogleAuthenticator,
    BCard,
  },
  data() {
    return {
      hiddenEmail: '',
      isEnableEmail2FA: false,
      isEnableGoogle2FA: false,
      isEnableSMS2FA: false,
    }
  },
  created() {
    useJwt.axiosIns.get('/authenticator/getAuthenticatorStatus')
      .then(res => {
        this.hiddenEmail = res.data.hiddenEmail
        this.isEnableGoogle2FA = res.data.isEnableGoogle2FA
      })
      .catch(error => {
        if (error.response.data.error.message) {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Thất bại',
              icon: 'XIcon',
              variant: 'danger',
              text: error.response.data.error.message,
            },
          })
        }
      })
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-security.scss';
</style>
