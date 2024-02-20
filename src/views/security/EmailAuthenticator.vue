<template>
  <div class="css-1lvbox2">
    <div class="css-1px8uci">
      <div class="css-10c5sb">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="css-1iztezc">
          <path fill="#76808F" d="M2 20h20V5H2z"></path>
          <path d="M12 17L22 7V4H2v3l10 10z" fill="url(#mail-g_svg__paint0_linear)"></path>
          <defs>
            <linearGradient id="mail-g_svg__paint0_linear" x1="12" y1="17" x2="12" y2="4"
                            gradientUnits="userSpaceOnUse"
            >
              <stop stop-color="#1B74E4"></stop>
              <stop offset="1" stop-color="#F8D33A"></stop>
            </linearGradient>
          </defs>
        </svg>
      </div>
      <div class="css-1pysja1">
        <div class="css-1a5j38j">
          Xác minh qua địa chỉ email
        </div>
        <div class="css-fhtmef">
          Bảo vệ tài khoản và giao dịch của bạn.
        </div>
      </div>
    </div>
    <div class="css-1ds054f">
      <div class="css-w9d9dr">
        <div class="css-10nf7hq">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="none" class="css-e2t8e1">
            <path opacity="0.15" d="M7.966 0a8 8 0 11-5.64 2.36A7.926 7.926 0 017.966 0z" fill="currentColor"></path>
            <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M6.624 11L4 8.425l.746-.73 1.878 1.839L11.254 5l.746.736L6.624 11z" fill="currentColor"
            ></path>
          </svg>
          <div class="css-eeq2ml">
            {{ hiddenEmail }}
          </div>
        </div>
      </div>
      <div class="css-kgrsce">
        <button
          class="css-8iajtr"
          @click="changeEmail()"
        >
          Thay đổi
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'

export default {
  name: 'EmailAuthenticator',
  props: {
    hiddenEmail: {
      type: String,
      default: '',
    },
  },
  methods: {
    changeEmail() {
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
