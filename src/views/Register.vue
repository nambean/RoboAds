<template>
  <div class="auth-wrapper auth-v1 px-2">
    <div class="register py-2">

      <!-- Register v1 -->
      <b-card class="mb-0">
        <b-link class="brand-logo">
          <vuexy-logo />
        </b-link>

        <!-- form -->
        <validation-observer ref="registerForm">
          <b-form
            class="auth-register-form mt-2"
            @submit.prevent="validationForm"
          >
            <!-- username -->
            <b-form-group>
              <label for="username">User name <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="Username"
                vid="userName"
                rules="required"
              >
                <b-form-input
                  id="username"
                  v-model="regInfo.userName"
                  :state="errors.length > 0 ? false:null"
                  name="register-username"
                  placeholder="johndoe"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>

            <b-form-group>
              <label for="full-name">Full name <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="Full name"
                rules="required"
                vid="fullName"
              >
                <b-form-input
                  id="full-name"
                  v-model="regInfo.fullName"
                  :state="errors.length > 0 ? false: null"
                  name="full-name"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>
            <b-row>
              <b-col
                cols="12"
                md="6"
              >
                <b-form-group>
                  <label for="password">Password <span class="required">*</span></label>
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
                        v-model="regInfo.password"
                        :type="passwordFieldType"
                        :state="errors.length > 0 ? false:null"
                        class="form-control-merge"
                        name="register-password"
                        placeholder="············"
                      />
                      <b-input-group-append is-text>
                        <feather-icon
                          :icon="passwordToggleIcon"
                          class="cursor-pointer"
                          @click="togglePasswordVisibility"
                        />
                      </b-input-group-append>
                    </b-input-group>
                    <small class="text-danger">{{ errors[0] }}</small>
                  </validation-provider>
                </b-form-group>
              </b-col>
              <b-col
                cols="12"
                md="6"
              >
                <b-form-group>
                  <label for="confirm-password">Confirm Password <span class="required">*</span></label>
                  <validation-provider
                    #default="{ errors }"
                    name="Confirm Password"
                    vid="confirmPassword"
                    rules="required|confirmed:password"
                  >
                    <b-input-group
                      class="input-group-merge"
                      :class="errors.length > 0 ? 'is-invalid':null"
                    >
                      <b-form-input
                        id="confirm-password"
                        v-model="regInfo.confirmPassword"
                        :type="confirmPasswordFieldType"
                        :state="errors.length > 0 ? false:null"
                        class="form-control-merge"
                        name="confirm-password"
                        placeholder="············"
                      />
                      <b-input-group-append is-text>
                        <feather-icon
                          :icon="confirmPasswordToggleIcon"
                          class="cursor-pointer"
                          @click="toggleConfirmPasswordVisibility"
                        />
                      </b-input-group-append>
                    </b-input-group>
                    <small class="text-danger">{{ errors[0] }}</small>
                  </validation-provider>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row>
              <b-col
                cols="12"
                md="6"
              >
                <b-form-group>
                  <label for="pin">PIN <span class="required">*</span></label>
                  <validation-provider
                    #default="{ errors }"
                    name="PIN"
                    vid="pin"
                    rules="required"
                  >
                    <b-input-group
                      class="input-group-merge"
                      :class="errors.length > 0 ? 'is-invalid':null"
                    >
                      <b-form-input
                        id="pin"
                        v-model="regInfo.pin"
                        :type="pinFieldType"
                        :state="errors.length > 0 ? false:null"
                        class="form-control-merge"
                        name="pin"
                        placeholder="············"
                        autocomplete="new-password"
                      />
                      <b-input-group-append is-text>
                        <feather-icon
                          :icon="pinToggleIcon"
                          class="cursor-pointer"
                          @click="togglePinVisibility"
                        />
                      </b-input-group-append>
                    </b-input-group>
                    <small class="text-danger">{{ errors[0] }}</small>
                  </validation-provider>
                </b-form-group>
              </b-col>
              <b-col
                cols="12"
                md="6"
              >
                <b-form-group>
                  <label for="confirm-pin">Confirm PIN <span class="required">*</span></label>
                  <validation-provider
                    #default="{ errors }"
                    name="Confirm PIN"
                    vid="confirmPin"
                    rules="required|confirmed:pin"
                  >
                    <b-input-group
                      class="input-group-merge"
                      :class="errors.length > 0 ? 'is-invalid':null"
                    >
                      <b-form-input
                        id="confirm-pin"
                        v-model="regInfo.confirmPin"
                        :type="confirmPinFieldType"
                        :state="errors.length > 0 ? false:null"
                        class="form-control-merge"
                        name="confirm-password"
                        placeholder="············"
                      />
                      <b-input-group-append is-text>
                        <feather-icon
                          :icon="confirmPinToggleIcon"
                          class="cursor-pointer"
                          @click="toggleConfirmPinVisibility"
                        />
                      </b-input-group-append>
                    </b-input-group>
                    <small class="text-danger">{{ errors[0] }}</small>
                  </validation-provider>
                </b-form-group>
              </b-col>
            </b-row>
            <b-form-group>
              <label for="email">Email <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="Email"
                rules="email|required"
                vid="userEmail"
              >
                <b-form-input
                  id="email"
                  v-model="regInfo.userEmail"
                  :state="errors.length > 0 ? false: null"
                  name="register-email"
                  placeholder="john@example.com"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>
            <b-form-group>
              <label for="phone">Phone <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="Phone"
                rules="required"
                vid="phone"
              >
                <b-form-input
                  id="phone"
                  v-model="regInfo.phone"
                  :state="errors.length > 0 ? false: null"
                  type="number"
                  name="phone"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>
            <b-form-group>
              <label for="id-passport">ID/Passport <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="ID/Passport"
                rules="required"
                vid="idPassport"
              >
                <b-form-input
                  id="id-passport"
                  v-model="regInfo.idPassport"
                  :state="errors.length > 0 ? false: null"
                  type="number"
                  name="idPassport"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>
            <b-form-group>
              <label for="user-referral">Referral Code <span class="required">*</span></label>
              <validation-provider
                #default="{ errors }"
                name="Referral Code"
                rules="required"
                vid="userReferral"
              >
                <b-form-input
                  id="user-referral"
                  v-model="regInfo.userReferral"
                  :state="errors.length > 0 ? false: null"
                  name="userReferral"
                />
                <small class="text-danger">{{ errors[0] }}</small>
              </validation-provider>
            </b-form-group>
            <!-- checkbox -->
            <b-form-group>
              <b-form-checkbox
                id="register-privacy-policy"
                v-model="regInfo.status"
                name="checkbox-1"
              >
                I agree to <b-link>privacy policy & terms</b-link>
              </b-form-checkbox>
            </b-form-group>

            <!-- submit button -->
            <b-button
              variant="primary"
              block
              type="submit"
              :disabled="!regInfo.status"
            >
              Submit
            </b-button>
          </b-form>
        </validation-observer>

        <b-card-text class="text-center mt-2">
          <span>Already a member ? </span>
          <b-link :to="{name:'auth-login'}">
            <span>Login</span>
          </b-link>
        </b-card-text>
      </b-card>
    <!-- /Register v1 -->
    </div>

  </div>
</template>

<script>
import { ValidationProvider, ValidationObserver } from 'vee-validate'
import {
  BCard, BLink, BCardText, BForm,
  BButton, BFormInput, BFormGroup, BInputGroup, BInputGroupAppend, BFormCheckbox, BRow, BCol,
} from 'bootstrap-vue'
import VuexyLogo from '@core/layouts/components/Logo.vue'
import { required, email } from '@validations'
import { togglePasswordVisibility } from '@core/mixins/ui/forms'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'

export default {
  components: {
    VuexyLogo,
    // BSV
    BCard,
    BLink,
    BCardText,
    BForm,
    BButton,
    BFormInput,
    BFormGroup,
    BInputGroup,
    BInputGroupAppend,
    BFormCheckbox,
    BRow,
    BCol,
    // validations
    ValidationProvider,
    ValidationObserver,
  },
  mixins: [togglePasswordVisibility],
  data() {
    return {
      regInfo: {
        userEmail: '',
        userName: '',
        fullName: '',
        password: '',
        confirmPassword: '',
        pin: '',
        phone: '',
        idPassport: '',
        userReferral: '',
        confirmPin: '',
        status: false,
      },

      // validation rules
      required,
      email,
    }
  },
  computed: {
    passwordToggleIcon() {
      return this.passwordFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
    confirmPasswordToggleIcon() {
      return this.confirmPasswordFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
    pinToggleIcon() {
      return this.pinFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
    confirmPinToggleIcon() {
      return this.confirmPinFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
  },
  created() {
    this.regInfo.userReferral = this.$route.query.ref
  },
  methods: {
    async validationForm() {
      const isValidForm = await this.$refs.registerForm.validate()
      if (!isValidForm) {
        return
      }

      const { ...user } = this.regInfo

      this.$http.post('/user/sign-up', user)
        .then(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success register',
              icon: 'CoffeeIcon',
              variant: 'success',
              text: 'Please confirm your mail to complete.',
            },
          })

          this.$router.push({
            name: 'register-confirm',
            query: {
              fullName: user.fullName,
              userEmail: user.userEmail,
            },
          })
        })
        .catch(error => {
          this.$refs.registerForm.setErrors(error.response.data.error)
        })
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-auth.scss';
</style>
