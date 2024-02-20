<template>
  <div class="auth-wrapper auth-v1 px-2">
    <div class="auth-inner py-2">

      <!-- Login v1 -->
      <b-card class="mb-0">
        <b-link class="brand-logo">
          <vuexy-logo/>
        </b-link>

        <b-alert
          variant="danger"
          :show="errorMessage.length > 0"
          class="mb-0"
        >
          <div class="alert-body">
            <feather-icon
              icon="InfoIcon"
              class="mr-50"
            />
            {{ errorMessage }}
          </div>
        </b-alert>

        <!-- form -->
        <validation-observer
          ref="loginForm"
          #default="{invalid}"
        >
          <b-form
            class="auth-login-form mt-2"
            @submit.prevent="login"
          >
            <!-- Username -->
            <b-form-group
              label-for="user-name"
              label="Tài khoản"
            >
              <validation-provider
                #default="{ errors }"
                name="Username"
                vid="userName"
                rules="required"
              >
                <b-form-input
                  id="user-name"
                  v-model="userName"
                  name="login-user-name"
                  :state="errors.length > 0 ? false:null"
                  autofocus
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>

            <!-- password -->
            <b-form-group>
              <div class="d-flex justify-content-between">
                <label for="password">Mật khẩu</label>
<!--                <b-link :to="{name:'auth-forgot-password'}">
                  <small>Quên mật khẩu?</small>
                </b-link>-->
              </div>
              <validation-provider
                #default="{ errors }"
                name="Password"
                vid="password"
                rules="required"
              >
                <b-input-group
                  class="input-group-merge"
                  :class="errors.length > 0 ? 'is-invalid':null"
                >
                  <b-form-input
                    id="password"
                    v-model="password"
                    :type="passwordFieldType"
                    class="form-control-merge"
                    :state="errors.length > 0 ? false:null"
                    name="login-password"
                    placeholder="Password"
                  />

                  <b-input-group-append is-text>
                    <feather-icon
                      class="cursor-pointer"
                      :icon="passwordToggleIcon"
                      @click="togglePasswordVisibility"
                    />
                  </b-input-group-append>
                </b-input-group>
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>

            <!-- Username -->
            <b-form-group
              v-if="isEnable2FA"
              label-for="otp-google-2fa"
              label="Mã 2FA"
            >
              <validation-provider
                #default="{ errors }"
                name="2FA Code"
                vid="otpGoogle2FA"
                rules="required"
              >
                <b-form-input
                  id="otp-google-2fa"
                  v-model="otpGoogle2FA"
                  name="login-user-name"
                  :state="errors.length > 0 ? false:null"
                  type="number"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>

            <!-- checkbox -->
            <b-form-group>
              <b-form-checkbox
                id="remember-me"
                v-model="status"
                name="checkbox-1"
              >
                Ghi nhớ đăng nhập
              </b-form-checkbox>
            </b-form-group>

            <!-- submit button -->
            <b-button
              variant="primary"
              type="submit"
              block
              :disabled="invalid"
            >
              Đăng nhập
            </b-button>
          </b-form>
        </validation-observer>

<!--        <b-card-text class="text-center mt-2">
          <span>Chưa có tài khoản </span>
          <b-link :to="{name:'auth-register'}">
            <span>Đăng kí</span>
          </b-link>
        </b-card-text>-->
      </b-card>
      <!-- /Login v1 -->
    </div>
  </div>
</template>

<script>
import { ValidationProvider, ValidationObserver } from 'vee-validate'
import {
  BButton,
  BForm,
  BFormInput,
  BFormGroup,
  BCard,
  BLink,
  // BCardText,
  BInputGroup,
  BInputGroupAppend,
  BFormCheckbox,
  BAlert,
} from 'bootstrap-vue'
import VuexyLogo from '@core/layouts/components/Logo.vue'
import { required } from '@validations'
import { togglePasswordVisibility } from '@core/mixins/ui/forms'
import useJwt from '@/auth/jwt/useJwt'

export default {
  components: {
    // BSV
    BButton,
    BForm,
    BFormInput,
    BFormGroup,
    BCard,
    BLink,
    VuexyLogo,
    // BCardText,
    BInputGroup,
    BInputGroupAppend,
    BFormCheckbox,
    ValidationProvider,
    ValidationObserver,
    BAlert,
  },
  mixins: [togglePasswordVisibility],
  data() {
    return {
      userName: '',
      password: '',
      otpGoogle2FA: '',
      status: '',
      // validation rules
      required,
      errorMessage: '',
      isEnable2FA: '',
    }
  },
  computed: {
    passwordToggleIcon() {
      return this.passwordFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
  },
  created() {
    let saveUserInfo = localStorage.getItem('saveUserInfo')
    if (saveUserInfo) {
      saveUserInfo = JSON.parse(saveUserInfo)
      this.status = saveUserInfo.status
      this.userName = saveUserInfo.userName
      this.password = saveUserInfo.password
    }

    const sessionExpired = localStorage.getItem('sessionExpired')
    if (sessionExpired) {
      this.errorMessage = sessionExpired
      localStorage.removeItem('sessionExpired')
    }
  },
  methods: {
    login() {
      localStorage.removeItem('saveUserInfo')
      if (this.status) {
        const saveUserInfo = {
          status: this.status,
          userName: this.userName,
          password: this.password,
        }
        localStorage.setItem('saveUserInfo', JSON.stringify(saveUserInfo))
      }

      this.$refs.loginForm.validate()
        .then(success => {
          if (success) {
            const loginData = {
              userName: this.userName,
              password: this.password,
            }

            if (this.isEnable2FA) {
              loginData.otpGoogle2FA = this.otpGoogle2FA
            }

            this.$http.post('/user/login', loginData)
              .then(response => {
                const { userInfo } = response.data
                const { menus } = response.data
                const { isEnable2FA } = response.data

                if (isEnable2FA) {
                  this.isEnable2FA = isEnable2FA
                  return
                }
                const homeUrl = menus[0].route

                useJwt.setToken(response.data.accessToken)
                localStorage.setItem('userData', JSON.stringify(userInfo))
                localStorage.setItem('menus', JSON.stringify(menus))
                localStorage.setItem('homeUrl', homeUrl)

                this.$router.push(homeUrl)
              })
              .catch(error => {
                this.$refs.loginForm.setErrors(error.response.data.error)
              })
          }
        })
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-auth.scss';
</style>
