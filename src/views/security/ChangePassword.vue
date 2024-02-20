<template>
  <div class="css-1lvbox2">
    <div class="css-1px8uci">
      <div class="css-10c5sb">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="css-1iztezc">
          <path fill-rule="evenodd" clip-rule="evenodd" d="M23 6H1v12h22V6zm-2 2h-2v8h2V8z" fill="#76808F"></path>
          <path d="M6.5 12a2 2 0 10-4 0 2 2 0 004 0z" fill="url(#password-g_svg__paint0_linear)"></path>
          <circle r="2" transform="matrix(1 0 0 -1 10 12)" fill="url(#password-g_svg__paint1_linear)"></circle>
          <path d="M17.5 12a2 2 0 10-4 0 2 2 0 004 0z" fill="url(#password-g_svg__paint2_linear)"></path>
          <defs>
            <linearGradient id="password-g_svg__paint0_linear" x1="4.5" y1="14" x2="4.5" y2="10"
                            gradientUnits="userSpaceOnUse"
            >
              <stop stop-color="#1B74E4"></stop>
              <stop offset="1" stop-color="#F8D33A"></stop>
            </linearGradient>
            <linearGradient id="password-g_svg__paint1_linear" x1="2" y1="0" x2="2" y2="4"
                            gradientUnits="userSpaceOnUse"
            >
              <stop stop-color="#1B74E4"></stop>
              <stop offset="1" stop-color="#F8D33A"></stop>
            </linearGradient>
            <linearGradient id="password-g_svg__paint2_linear" x1="15.5" y1="14" x2="15.5" y2="10"
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
          Mật khẩu đăng nhập
        </div>
        <div class="css-fhtmef">
          <div class="css-vurnku">
            Mật khẩu đăng nhập được dùng để đăng nhập vào tài khoản của bạn.
          </div>
        </div>
      </div>
    </div>
    <div class="css-1ds054f">
      <div class="css-kgrsce">
        <button
          class="css-8iajtr"
          @click="openUpdatePasswordForm"
        >
          Thay đổi
        </button>
      </div>
    </div>

    <validation-observer
      ref="updatePasswordForm"
      #default="{invalid}"
    >
      <b-modal
        id="modal-update-password-Form"
        ref="modalUpdatePasswordForm"
        hide-footer
        hide-header
        centered
      >
        <b-form>
          <div class="css-1hsjc7k">
            <div
              class="css-hba76g mt-3"
            >
              Cập nhật mật khẩu
            </div>
            <div class="css-hba76g">
              <div class="css-1dbk341 mt-2">
                <div class="css-key3v2">
                  <validation-provider
                    #default="{ errors }"
                    name="Password Old"
                    vid="passwordOld"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="password-old"
                          v-model="updatePassword.passwordOld"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="password-old">
                          Mật khẩu cũ <span class="required">*</span>
                        </label>
                      </div>
                    </div>
                    <div class="bn-2fa-fieldInfo css-15c6fsr text-danger">
                      {{ errors[0] }}
                    </div>
                  </validation-provider>
                </div>
                <div class="css-key3v2">
                  <validation-provider
                    #default="{ errors }"
                    name="Password New"
                    vid="passwordNew"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="password-new"
                          v-model="updatePassword.passwordNew"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="password-new">
                          Mật khẩu mới <span class="required">*</span>
                        </label>
                      </div>
                    </div>
                    <div class="bn-2fa-fieldInfo css-15c6fsr text-danger">
                      {{ errors[0] }}
                    </div>
                  </validation-provider>
                </div>
                <div class="css-key3v2">
                  <validation-provider
                    #default="{ errors }"
                    name="Confirm Password New"
                    vid="confirmPasswordNew"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="confirm-password-new"
                          v-model="updatePassword.confirmPasswordNew"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="confirm-password-new">
                          Xác nhận mật khẩu mới <span class="required">*</span>
                        </label>
                      </div>
                    </div>
                    <div class="bn-2fa-fieldInfo css-15c6fsr text-danger">
                      {{ errors[0] }}
                    </div>
                  </validation-provider>
                </div>
                <div class="css-4cffwv mt-2">
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
                    Cập nhật
                  </b-button>
                </div>
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
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'ChangePassword',
  components: {
    BModal,
    BForm,
    BButton,
    ValidationObserver,
    ValidationProvider,
  },
  data() {
    return {
      required,
      updatePassword: {
        passwordOld: '',
        passwordNew: '',
        confirmPasswordNew: '',
      },
    }
  },
  methods: {
    openUpdatePasswordForm() {
      this.updatePassword = {
        passwordOld: '',
        passwordNew: '',
        confirmPasswordNew: '',
      }
      this.$refs.modalUpdatePasswordForm.show()
    },
    closeModal() {
      this.$refs.modalUpdatePasswordForm.hide()
    },
    handleSubmit() {
      useJwt.axiosIns.post('/user/updateUserPassword', this.updatePassword)
        .then(res => {
          this.$refs.modalUpdatePasswordForm.hide()
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
            this.$refs.updatePasswordForm.setErrors(error.response.data.error)
          }
        })
    },
  },
}
</script>

<style scoped>

</style>
