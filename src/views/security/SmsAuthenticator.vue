<template>
  <div class="css-1lvbox2">
    <div class="css-1px8uci">
      <div class="css-10c5sb">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="css-1iztezc">
          <path d="M17 2H5v20h12V2z" fill="url(#authenticator-sms-g_svg__paint0_linear)"></path>
          <path d="M5 2h12v2H5V2zM5 20h12v2H5v-2zM20 14.001v-7H7v7h3v4l4-4h6z" fill="#76808F"></path>
          <defs>
            <linearGradient id="authenticator-sms-g_svg__paint0_linear" x1="5" y1="12" x2="17" y2="12"
                            gradientUnits="userSpaceOnUse"
            >
              <stop stop-color="#1B74E4"></stop>
              <stop offset="1" stop-color="#F8D33A"></stop>
            </linearGradient>
          </defs>
        </svg>
      </div>
      <div class="css-1pysja1">
        <div class="css-1a5j38j">Xác minh qua số điện thoại</div>
        <div class="css-fhtmef">Bảo vệ tài khoản và giao dịch của bạn.</div>
      </div>
    </div>
    <div class="css-1ds054f">
      <div
        v-if="isSMSEnabled"
        class="css-cdv4zx"
      >
        <div class="css-10nf7hq">
          <svg data-v-614b9148="" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="none" class="css-e2t8e1"><path data-v-614b9148="" opacity="0.15" d="M7.966 0a8 8 0 11-5.64 2.36A7.926 7.926 0 017.966 0z" fill="currentColor"></path><path data-v-614b9148="" fill-rule="evenodd" clip-rule="evenodd" d="M6.624 11L4 8.425l.746-.73 1.878 1.839L11.254 5l.746.736L6.624 11z" fill="currentColor"></path></svg>
          <div class="css-eeq2ml">
            Đã thiết lập
          </div>
        </div>
      </div>
      <div
        v-if="!isSMSEnabled"
        class="css-cdv4zx"
      >
        <div class="css-10nf7hq">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="none" class="css-b327i8"><rect opacity="0.15" width="16" height="16" rx="8" fill="currentColor"></rect><path fill-rule="evenodd" clip-rule="evenodd" d="M12 11.117l-.883.878-3.122-3.117L4.883 12l-.878-.883L7.117 8 4 4.883l.883-.878 3.112 3.117L11.117 4l.873.883L8.878 8 12 11.117z" fill="currentColor"></path></svg>
          <div class="css-eeq2ml">
            Chưa thiết lập
          </div>
        </div>
      </div>
      <div class="css-kgrsce">
        <button
          id="security2fa_sms_btn_enable"
          class="css-8iajtr"
          @click="enableSMS2FA()"
        >
          Kích hoạt
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'

export default {
  name: 'SmsAuthenticator',
  props: {
    hiddenEmail: {
      type: String,
      default: '',
    },
    isSMSEnabled: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    enableSMS2FA() {
      this.$toast({
        component: ToastificationContent,
        position: 'top-right',
        props: {
          title: '',
          icon: 'CheckIcon',
          variant: 'danger',
          text: 'Chức năng đang phát triển',
        },
      })
    },
  },
}
</script>

<style scoped>

</style>
