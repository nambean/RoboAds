<template>
  <div class="css-1lvbox2">
    <div class="css-1px8uci">
      <div class="css-10c5sb">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="css-1iztezc"><path transform="matrix(1 0 0 -1 5 23)" fill="url(#devices-phone-g_svg__paint0_linear)" d="M0 0h14v22H0z"></path><path fill="#76808F" d="M5 21h14v2H5zM5 1h14v2H5z"></path><defs><linearGradient id="devices-phone-g_svg__paint0_linear" x1="7" y1="0" x2="7" y2="22" gradientUnits="userSpaceOnUse"><stop stop-color="#1B74E4"></stop><stop offset="1" stop-color="#F8D33A"></stop></linearGradient></defs></svg>
      </div>
      <div class="css-1pysja1">
        <div class="css-1a5j38j">
          Mã PIN
        </div>
        <div class="css-fhtmef">
          <div class="css-vurnku">
            Mã PIN để thực hiện các giao dịch.
          </div>
        </div>
      </div>
    </div>
    <div class="css-1ds054f">
      <div class="css-kgrsce">
        <button
          class="css-8iajtr"
          @click="openUpdatePINForm()"
        >
          Thay đổi
        </button>
      </div>
    </div>

    <validation-observer
      ref="updatePINForm"
      #default="{invalid}"
    >
      <b-modal
        id="modal-update-pin-Form"
        ref="modalUpdatePINForm"
        hide-footer
        hide-header
        centered
      >
        <b-form>
          <div class="css-1hsjc7k">
            <div
              class="css-hba76g mt-3"
            >
              Cập nhật PIN
            </div>
            <div class="css-hba76g">
              <div class="css-1dbk341 mt-2">
                <div class="css-key3v2">
                  <validation-provider
                    #default="{ errors }"
                    name="PIN Old"
                    vid="pinOld"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="pin-old"
                          v-model="updatePIN.pinOld"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="pin-old">
                          PIN cũ <span class="required">*</span>
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
                    name="PIN New"
                    vid="pinNew"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="pin-new"
                          v-model="updatePIN.pinNew"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="pin-new">
                          PIN mới <span class="required">*</span>
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
                    name="Confirm PIN New"
                    vid="confirmPinNew"
                    rules="required"
                  >
                    <div class="css-enhan1">
                      <div class="bn-2fa-field css-1pnngcv">
                        <input
                          id="confirm-pin-new"
                          v-model="updatePIN.confirmPinNew"
                          class="css-16fg16t"
                          type="password"
                        >
                        <label class="bn-input-label css-k4png4" for="confirm-pin-new">
                          Xác nhận PIN mới <span class="required">*</span>
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
  name: 'ChangePIN',
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
      updatePIN: {
        pinOld: '',
        pinNew: '',
        confirmPinNew: '',
      },
    }
  },
  methods: {
    openUpdatePINForm() {
      this.updatePIN = {
        pinOld: '',
        pinNew: '',
        confirmPinNew: '',
      }
      this.$refs.modalUpdatePINForm.show()
    },
    closeModal() {
      this.$refs.modalUpdatePINForm.hide()
    },
    handleSubmit() {
      useJwt.axiosIns.post('/user/updateUserPIN', this.updatePIN)
        .then(res => {
          this.$refs.modalUpdatePINForm.hide()
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
            this.$refs.updatePINForm.setErrors(error.response.data.error)
          }
        })
    },
  },
}
</script>

<style scoped>

</style>
