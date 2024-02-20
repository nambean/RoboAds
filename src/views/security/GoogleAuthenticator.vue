<template>
  <div class="css-1lvbox2">
    <div class="css-1px8uci">
      <div class="css-10c5sb">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="css-1iztezc">
          <path d="M12 18a8 8 0 100-16v8l-5.657 5.657A7.975 7.975 0 0012 18z"
                fill="url(#authenticator-google-g_svg__paint0_linear)"
          ></path>
          <path d="M12 10l-5.657 5.657A8 8 0 0112 2v8zM13 20h7v2h-7zM4 20h7v2H4z" fill="#76808F"></path>
          <defs>
            <linearGradient id="authenticator-google-g_svg__paint0_linear" x1="13.172" y1="18" x2="13.172" y2="2"
                            gradientUnits="userSpaceOnUse"
            >
              <stop offset="0.333" stop-color="#1B74E4"></stop>
              <stop offset="1" stop-color="#FBDA3C"></stop>
            </linearGradient>
          </defs>
        </svg>
      </div>
      <div class="css-1pysja1">
        <div class="css-1a5j38j">
          Google Authenticator (Đề xuất)
        </div>
        <div class="css-fhtmef">
          <div class="css-vurnku">
            Bảo vệ tài khoản và giao dịch của bạn.
          </div>
        </div>
      </div>
    </div>
    <div class="css-1ds054f">
      <div
        v-if="isGoogleEnabled"
        class="css-cdv4zx"
      >
        <div class="css-10nf7hq">
          <svg data-v-614b9148="" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="none" class="css-e2t8e1">
            <path data-v-614b9148="" opacity="0.15" d="M7.966 0a8 8 0 11-5.64 2.36A7.926 7.926 0 017.966 0z"
                  fill="currentColor"
            ></path>
            <path data-v-614b9148="" fill-rule="evenodd" clip-rule="evenodd"
                  d="M6.624 11L4 8.425l.746-.73 1.878 1.839L11.254 5l.746.736L6.624 11z" fill="currentColor"
            ></path>
          </svg>
          <div class="css-eeq2ml">
            Đã thiết lập
          </div>
        </div>
      </div>
      <div
        v-if="!isGoogleEnabled"
        class="css-cdv4zx"
      >
        <div class="css-10nf7hq">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="none" class="css-b327i8">
            <rect opacity="0.15" width="16" height="16" rx="8" fill="currentColor"></rect>
            <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M12 11.117l-.883.878-3.122-3.117L4.883 12l-.878-.883L7.117 8 4 4.883l.883-.878 3.112 3.117L11.117 4l.873.883L8.878 8 12 11.117z"
                  fill="currentColor"
            ></path>
          </svg>
          <div class="css-eeq2ml">
            Chưa thiết lập
          </div>
        </div>
      </div>
      <div class="css-kgrsce">
        <button
          v-if="!isGoogleEnabled"
          class="css-8iajtr"
          @click="openModalEnableGoogle2FA"
        >
          Kích hoạt
        </button>

        <button
          v-if="isGoogleEnabled"
          class="css-8iajtr"
          @click="openModalDisableGoogle2FA"
        >
          Hủy bỏ
        </button>
      </div>
    </div>

    <validation-observer
      ref="enableGoogle2FAForm"
      #default="{invalid}"
    >
      <b-modal
        id="modal-enable-google-2fa"
        ref="modalEnableGoogle2FA"
        title="Kích hoạt Google Authenticator"
        hide-footer
        hide-header
        centered
      >
        <b-form>
          <div class="css-1hsjc7k">
            <div
              v-if="isGoogleEnabled"
              class="css-hba76g mt-3 mb-5"
            >
              Hủy kích hoạt Google Authentication
            </div>
            <div
              v-if="!isGoogleEnabled"
              class="css-hba76g"
            >
              Quét mã QR này bằng ứng dụng Authenticator
            </div>
            <div class="css-1lx7oj">
              <div
                v-if="!isGoogleEnabled"
                class="css-81xrsn"
              >
                <div class="css-wh32zt">
                  <qrcode-vue
                    :value="otpauth"
                    :size="100"
                  />
                </div>
              </div>
              <div
                v-if="!isGoogleEnabled"
                class="css-1imckl0"
              >
                {{ enableGoogle.googleSecret }}
              </div>
              <div
                v-if="!isGoogleEnabled"
                class="css-fhtmef"
              >
                Nếu bạn không thể quét mã QR, hãy nhập mã này vào ứng dụng.
              </div>
              <div class="css-1dbk341 mt-2">
                <div class="css-key3v2">
                  <validation-provider
                    #default="{ errors }"
                    name="Email OTP"
                    vid="emailOTP"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          v-model="enableGoogle.emailOTP"
                          class="css-16fg16t"
                        >
                        <div class="bn-input-suffix css-vurnku">
                          <div
                            class="css-1mme6xj"
                            @click="sendBindGoogle2FA"
                          >
                            Lấy mã
                          </div>
                        </div>
                        <label class="bn-input-label css-k4png4">Mã xác minh email</label></div>
                    </div>
                    <div class="bn-2fa-fieldInfo  css-15c6fsr">
                      Nhập mã xác minh 6 chữ số đã được gửi đến email
                      <span class="css-vurnku">{{ hiddenEmail }}</span>
                    </div>
                    <div class="bn-2fa-fieldInfo  css-15c6fsr text-danger">
                      {{ errors[0] }}
                    </div>
                  </validation-provider>
                </div>
                <div
                  v-if="!isGoogleEnabled"
                  class="css-key3v2 mt-2"
                >
                  <validation-provider
                    #default="{ errors }"
                    name="Google OTP"
                    vid="googleOTP"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          v-model="enableGoogle.googleOTP"
                          class="css-16fg16t"
                        >
                        <label class="bn-input-label css-k4png4">Mã xác thực</label>
                      </div>
                    </div>
                    <div class="bn-2fa-fieldInfo css-15c6fsr">
                      Hãy nhập mã gồm 6 chữ số từ Google Authenticator.
                    </div>
                    <div class="bn-2fa-fieldInfo css-15c6fsr text-danger">
                      {{ errors[0] }}
                    </div>
                  </validation-provider>
                </div>
              </div>
              <div class="css-4cffwv mt-3 mb-2">
                <b-button
                  class="custom-button modal-button-cancel"
                  variant="secondary"
                  @click="closeModal"
                >
                  Hủy
                </b-button>
                <b-button
                  class="custom-button modal-button-ok"
                  variant="primary"
                  :disabled="invalid"
                  @click="handleSubmit"
                >
                  {{ isGoogleEnabled ? 'Hủy kích hoạt' : 'Kích hoạt' }}
                </b-button>
              </div>
            </div>
          </div>
        </b-form>
      </b-modal>
    </validation-observer>
  </div>
</template>

<script>
import {
  BModal, BForm, BButton,
} from 'bootstrap-vue'
import {
  ValidationObserver,
  ValidationProvider,
} from 'vee-validate'
import { required } from '@validations'
import QrcodeVue from 'qrcode.vue'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'GoogleAuthenticator',
  components: {
    BModal,
    BForm,
    BButton,
    QrcodeVue,
    ValidationObserver,
    ValidationProvider,
  },
  props: {
    hiddenEmail: {
      type: String,
      default: '',
    },
    isGoogleEnabled: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      required,
      otpauth: '',
      enableGoogle: {
        emailOTP: '',
        googleSecret: '',
        googleOTP: '',
      },
    }
  },
  methods: {
    openModalEnableGoogle2FA() {
      useJwt.axiosIns.post('/authenticator/generateSecret')
        .then(res => {
          this.secret = res.data.data.secret
          this.otpauth = res.data.data.otpauth
          this.enableGoogle.googleSecret = res.data.data.secret
          this.enableGoogle.emailOTP = ''
          this.enableGoogle.googleOTP = ''
          this.$refs.modalEnableGoogle2FA.show()
        })
    },
    sendBindGoogle2FA() {
      useJwt.axiosIns.post('/authenticator/sendBindGoogle2FA')
        .then(res => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: res.data.message,
            },
          })
        })
    },
    handleSubmit() {
      const url = this.isGoogleEnabled ? '/authenticator/disableGoogle2FA' : '/authenticator/activeGoogle2FA'
      useJwt.axiosIns.post(url, this.enableGoogle)
        .then(res => {
          this.$refs.modalEnableGoogle2FA.hide()
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: res.data.message,
            },
          })
          setTimeout(() => {
            window.location.reload()
          }, 500)
        })
        .catch(error => {
          if (error.response.data && error.response.data.error.message) {
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
          } else {
            this.$refs.enableGoogle2FAForm.setErrors(error.response.data.error)
          }
        })
    },
    openModalDisableGoogle2FA() {
      setTimeout(() => {
        this.enableGoogle.emailOTP = ''
        this.$refs.modalEnableGoogle2FA.show()
      }, 100)
    },
    closeModal() {
      this.$refs.modalEnableGoogle2FA.hide()
    },
  },
}
</script>

<style lang="scss">

</style>
