<template>
  <b-card
    no-body
    class="user-update-card"
  >
    <b-card-body>
      <b-tabs
        content-class="col-12"
        pills
      >
        <b-tab>
          <template #title>
            <feather-icon icon="HomeIcon" />
            <span>User Information</span>
          </template>
          <!-- form -->
          <validation-observer
            ref="updateUserForm"
            #default="{invalid}"
          >
            <b-form autocomplete="chrome-off" @submit.prevent="updateUser">
              <b-card-header class="align-items-baseline">
                <div>
                  <h3>{{ isUpdate ? 'Update' : 'Create' }} user information</h3>
                </div>
                <div>
                  <b-button
                    v-ripple.400="'rgba(255, 255, 255, 0.15)'"
                    class="mr-1"
                    type="submit"
                    variant="primary"
                    :disabled="invalid"
                  >
                    <feather-icon
                      icon="CheckIcon"
                      size="15"
                      class="mr-50"
                    />
                    <span class="d-sm-inline d-none align-middle">{{ isUpdate ? 'Update' : 'Create' }}</span>
                  </b-button>
                  <b-button
                    v-ripple.400="'rgba(113, 102, 240, 0.15)'"
                    variant="outline-warning"
                    @click="$router.push({ name: 'user-manage'})"
                  >
                    <feather-icon
                      icon="ArrowLeftIcon"
                      size="15"
                      class="mr-50"
                    />
                    <span class="d-sm-inline d-none align-middle">Back</span>
                  </b-button>
                </div>
              </b-card-header>
              <b-card-body>
                <b-row>
                  <b-col
                    lg="6"
                    md="8"
                    offset-md="2"
                  >
                    <b-row>
                      <b-col
                        v-if="isUpdate"
                        cols="12"
                      >
                        <b-form-group
                          label="ID"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <div class="mt-50">
                            {{ userInfo.id }}
                          </div>
                        </b-form-group>
                      </b-col>
                      <b-col
                        v-if="isUpdate"
                        cols="12"
                      >
                        <b-form-group
                          label="User name"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <div class="mt-50">
                            {{ userInfo.userName }}
                          </div>
                        </b-form-group>
                      </b-col>
                      <b-col
                        v-if="!isUpdate"
                        cols="12"
                      >
                        <b-form-group
                          label="User Name"
                          label-for="u-user-name"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <validation-provider
                            #default="{ errors }"
                            name="User Name"
                            rules="required"
                            vid="userName"
                          >
                            <b-form-input
                              id="u-user-name"
                              v-model="userInfo.userName"
                              :state="errors.length > 0 ? false:null"
                              placeholder="User Name"
                              autocomplete="chrome-off"
                            />
                            <small class="text-danger">{{ errors[0] }}</small>
                          </validation-provider>
                        </b-form-group>
                      </b-col>
                      <b-col cols="12">
                        <b-form-group
                          label="Password"
                          label-for="u-password"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <validation-provider
                            #default="{ errors }"
                            name="Password"
                            vid="password"
                            :rules="!isUpdate ? 'required' : null"
                          >
                            <div
                              class="input-group input-group-merge"
                              :class="errors.length > 0 ? 'is-invalid': ''"
                            >
                              <input
                                id="u-password"
                                v-model="userInfo.password"
                                :type="passwordFieldType"
                                name="login-password"
                                placeholder="Password"
                                class="form-control-merge form-control"
                                :class="errors.length > 0 ? 'is-invalid': ''"
                                @focus="handleType"
                                @blur="handleType"
                              >
                              <div class="input-group-append">
                                <div class="input-group-text">
                                  <feather-icon
                                    class="cursor-pointer"
                                    :icon="passwordToggleIcon"
                                    @click="togglePasswordVisibility"
                                  />
                                </div>
                              </div>
                            </div>
                            <small class="text-danger">{{ errors[0] }}</small>
                          </validation-provider>
                        </b-form-group>
                      </b-col>
                      <b-col cols="12">
                        <b-form-group
                          label="Email"
                          label-for="u-email"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <validation-provider
                            #default="{ errors }"
                            name="Email"
                            rules="email"
                          >
                            <b-form-input
                              id="u-email"
                              v-model="userInfo.userEmail"
                              :state="errors.length > 0 ? false:null"
                              placeholder="Email"
                              autocomplete="chrome-off"
                            />
                            <small class="text-danger">{{ errors[0] }}</small>
                          </validation-provider>
                        </b-form-group>
                      </b-col>
                      <b-col cols="12">
                        <b-form-group
                          label="Full name"
                          label-for="u-full-name"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                          autocomplete="chrome-off"
                        >
                          <validation-provider
                            #default="{ errors }"
                            name="Full name"
                            :rules="!isUpdate? 'required': ''"
                            vid="fullName"
                          >
                            <b-form-input
                              id="u-full-name"
                              v-model="userInfo.fullName"
                              :state="errors.length > 0 ? false : null"
                              placeholder="Full name"
                            />
                            <small class="text-danger">{{ errors[0] }}</small>
                          </validation-provider>
                        </b-form-group>
                      </b-col>
                      <!--<b-col cols="12">
                        <b-form-group
                          label="User PIN"
                          label-for="u-pin"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <b-form-input
                            id="u-pin"
                            v-model="userInfo.userPIN"
                            placeholder="User PIN"
                          />
                        </b-form-group>
                      </b-col>-->
                      <b-col cols="12">
                        <b-form-group
                          label="Roles"
                          label-for="u-role"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <v-select
                            id="u-role"
                            v-model="userInfo.roles"
                            dir="ltr"
                            multiple
                            label="roleName"
                            :options="roles"
                          />
                        </b-form-group>
                      </b-col>
                      <b-col cols="12" v-if="userData.isAdmin">
                        <b-form-group
                          label="Team"
                          label-for="u-role"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <v-select
                            id="u-team"
                            v-model="userInfo.team"
                            label="text"
                            :options="teams"
                            :reduce="item => item.value"
                          />
                        </b-form-group>
                      </b-col>
                      <b-col cols="12">
                        <b-form-group
                          label="Active"
                          label-for="u-is-active"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <b-form-checkbox
                            id="u-is-active"
                            v-model="userInfo.isActive"
                            class="custom-control-primary mt-50"
                            name="check-button"
                            switch
                          >
                            <span class="switch-icon-left">
                              <feather-icon icon="UserCheckIcon" />
                            </span>
                            <span class="switch-icon-right">
                              <feather-icon icon="UserXIcon" />
                            </span>
                          </b-form-checkbox>
                        </b-form-group>
                      </b-col>
                      <b-col
                        v-if="isUpdate"
                        cols="12"
                      >
                        <b-form-group
                          label="Created at"
                          label-cols-md="4"
                          :label-class="`font-weight-bolder ${isSmall ? '' : 'text-right pr-3'}`"
                        >
                          <div class="mt-50">
                            {{ userInfo.createdAtDisplay }}
                          </div>
                        </b-form-group>
                      </b-col>
                    </b-row>
                  </b-col>
                </b-row>
              </b-card-body>
            </b-form>
          </validation-observer>
        </b-tab>
      </b-tabs>
    </b-card-body>
  </b-card>
</template>

<script>
import { ValidationProvider, ValidationObserver } from 'vee-validate'
import {
  BCard, BCardHeader, BCardBody, BButton, BRow, BCol, BForm, BFormGroup, BFormInput,
  BFormCheckbox, BTabs, BTab,
} from 'bootstrap-vue'
import { required, email } from '@validations'
import moment from 'moment'
import Ripple from 'vue-ripple-directive'
import vSelect from 'vue-select'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import { computed, watch } from '@vue/composition-api'
import { togglePasswordVisibility } from '@core/mixins/ui/forms'
import useJwt from '@/auth/jwt/useJwt'
import store from '@/store'

export default {
  components: {
    BCard,
    BCardHeader,
    BCardBody,
    BButton,
    BRow,
    BCol,
    BForm,
    BFormGroup,
    BFormInput,
    BTabs,
    BTab,
    vSelect,
    BFormCheckbox,
    ValidationProvider,
    ValidationObserver,
  },
  directives: {
    Ripple,
  },
  mixins: [togglePasswordVisibility],
  data() {
    return {
      email,
      required,
      userInfo: {},
      genderOptions: [
        { text: 'Male', value: 0 },
        { text: 'Female', value: 1 },
      ],
      roles: [],
      teams: [
        { text: 'Team 1', value: 1 },
        { text: 'Team 2', value: 2 },
        { text: 'Team 3', value: 3 },
        { text: 'Team 4', value: 4 },
      ],
      isUpdate: true,
      isSmall: false,
      userData: JSON.parse(localStorage.getItem('userData')),
    }
  },
  computed: {
    passwordToggleIcon() {
      return this.passwordFieldType === 'password' ? 'EyeIcon' : 'EyeOffIcon'
    },
  },
  async created() {
    this.passwordFieldType = 'text'

    const currentBreakPoint = computed(() => store.getters['app/currentBreakPoint'])
    watch(currentBreakPoint, val => {
      this.isSmall = ['xs', 'sm'].includes(val)
    })

    const dataRole = await useJwt.axiosIns.get('/role/getAllRole')
    if (dataRole.status !== 200) return

    const { id } = this.$route.query

    this.roles = dataRole.data

    if (!id) {
      this.isUpdate = false
      return
    }

    useJwt.axiosIns.get(`/user/getUserById?id=${this.$route.query.id}`).then(response => {
      this.userInfo = response.data
      delete this.userInfo.password
      this.userInfo.createdAtDisplay = moment(response.data.createdAt).format('DD/MM/YYYY HH:mm:ss')
    })
  },
  methods: {
    handleType(event) {
      const { srcElement, type } = event
      const { value } = srcElement

      if (type === 'blur' && !value) {
        this.passwordFieldType = 'text'
      } else {
        this.passwordFieldType = 'password'
      }
    },
    updateUser() {
      const pathRequest = this.isUpdate ? '/user/adminUpdateUser' : '/user/adminCreateUser'
      useJwt.axiosIns.post(pathRequest, this.userInfo).then(() => {
        this.$toast({
          component: ToastificationContent,
          position: 'top-right',
          props: {
            title: 'Success',
            icon: 'CheckIcon',
            variant: 'success',
            text: `Success ${this.isUpdate ? 'update' : 'create'} user ${this.userInfo.userName}`,
          },
        })
        this.$router.push({ name: 'user-manage' })
      }).catch(error => {
        this.$refs.updateUserForm.setErrors(error.response.data.error)
      })
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/libs/vue-select.scss';
</style>
