<template>
  <div class="container p-0">
    <b-card>
      <validation-observer
        ref="updateUserForm"
        #default="{invalid}"
      >
        <b-form @submit.prevent="updateUser">
          <b-card-header class="align-items-baseline">
            <div>
              <h3>Thông tin cá nhân</h3>
            </div>
            <div>
              <b-button
                v-ripple.400="'rgba(255, 255, 255, 0.15)'"
                class="mr-1"
                type="submit"
                variant="warning"
                :disabled="invalid"
              >
                <feather-icon
                  icon="CheckIcon"
                  size="15"
                  class="mr-50"
                />
                <span class="d-sm-inline d-none align-middle">Cập nhật</span>
              </b-button>
            </div>
          </b-card-header>
          <b-card-body
            class="p-0"
          >
            <b-row class="mb-3">
              <b-col
                md="6"
                offset-md="3"
              >
                <b-media no-body>
                  <b-media-aside>
                    <b-link>
                      <b-img
                        ref="previewEl"
                        rounded
                        :src="userInfo.picture"
                        height="80"
                      />
                    </b-link>
                    <!--/ avatar -->
                  </b-media-aside>

                  <b-media-body class="mt-75 ml-75">
                    <!-- upload button -->
                    <b-button
                      v-ripple.400="'rgba(255, 255, 255, 0.15)'"
                      variant="primary"
                      size="sm"
                      class="mb-75 mr-75"
                      @click="$refs.refInputEl.$el.click()"
                    >
                      Upload
                    </b-button>
                    <b-form-file
                      ref="refInputEl"
                      v-model="profileFile"
                      accept=".jpg, .png, .gif"
                      :hidden="true"
                      plain
                      @input="inputImageRenderer"
                    />
                    <!--/ upload button -->
                    <!--/ reset -->
                    <b-card-text>Định dạng JPG, GIF or PNG. Dung lượng tối đa 5MB</b-card-text>
                  </b-media-body>
                </b-media>
              </b-col>
            </b-row>
            <b-row>
              <b-col
                md="8"
                offset-md="1"
              >
                <b-col
                  cols="12"
                  class="p-0"
                >
                  <div class="form-row form-group">
                    <label
                      for="full-name"
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Họ và tên <span class="required">*</span>
                    </label>
                    <div class="col">
                      <validation-provider
                        #default="{ errors }"
                        name="Full Name"
                        vid="fullName"
                        rules="required"
                      >
                        <b-form-input
                          id="full-name"
                          v-model="userInfo.fullName"
                          :state="errors.length > 0 ? false:null"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                  <div class="form-row form-group">
                    <label
                      for="phone"
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Số điện thoại <span class="required">*</span>
                    </label>
                    <div class="col">
                      <validation-provider
                        #default="{ errors }"
                        name="Phone"
                        vid="phone"
                        rules="required"
                      >
                        <b-form-input
                          id="phone"
                          v-model="userInfo.phone"
                          :state="errors.length > 0 ? false:null"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                  <div class="form-row form-group">
                    <label
                      for="date-of-birth"
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Ngày sinh <span class="required">*</span>
                    </label>
                    <div class="col">
                      <validation-provider
                        #default="{ errors }"
                        name="Date Of Birth"
                        vid="dateOfBirth"
                        rules="required"
                      >
                        <flat-pickr
                          id="date-of-birth"
                          v-model="userInfo.dateOfBirth"
                          class="form-control"
                          :config="{altInput: true, altFormat: 'd/m/Y'}"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                  <div class="form-row form-group">
                    <label
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Số giấy chứng thực <span class="required">*</span>
                    </label>
                    <div class="col col-md-3">
                      <validation-provider
                        #default="{ errors }"
                        name="Verify Type"
                        vid="verifyType"
                        rules="required"
                      >
                        <v-select
                          v-model="userInfo.verifyTypeInfo"
                          dir="ltr"
                          label="text"
                          :options="verifyType"
                          :clearable="false"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                    <div class="col col-md-5">
                      <validation-provider
                        #default="{ errors }"
                        name="National Id"
                        vid="nationalId"
                        rules="required"
                      >
                        <b-form-input
                          v-model="userInfo.idPassport"
                          :state="errors.length > 0 ? false:null"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                  <div class="form-row form-group">
                    <label
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Giới tính <span class="required">*</span>
                    </label>
                    <div class="col col-md-3">
                      <validation-provider
                        #default="{ errors }"
                        name="Gender"
                        vid="gender"
                        rules="required"
                      >
                        <v-select
                          v-model="userInfo.genderInfo"
                          dir="ltr"
                          label="text"
                          :options="genderOptions"
                          :clearable="false"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                  <div class="form-row form-group">
                    <label
                      class="col-md-4 col-form-label"
                      :class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-1'}`"
                    >
                      Địa chỉ <span class="required">*</span>
                    </label>
                    <div class="col col-12 col-md-4">
                      <validation-provider
                        #default="{ errors }"
                        name="Province"
                        vid="province"
                        rules="required"
                      >
                        <v-select
                          v-model="userInfo.provinceInfo"
                          dir="ltr"
                          label="name"
                          :options="provinceOptions"
                          @input="onChangeProvince"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                    <div class="mt-0 col col-12 col-md-4">
                      <validation-provider
                        #default="{ errors }"
                        name="District"
                        vid="district"
                        rules="required"
                      >
                        <v-select
                          v-model="userInfo.districtInfo"
                          dir="ltr"
                          label="name"
                          :options="districtOptions"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                    <div class="col offset-md-4 col-12 col-md-8 mt-1">
                      <validation-provider
                        #default="{ errors }"
                        name="Address"
                        vid="address"
                        rules="required"
                      >
                        <b-form-textarea
                          v-model="userInfo.address"
                          rows="4"
                        />
                        <small class="text-danger">{{ errors[0] }}</small>
                      </validation-provider>
                    </div>
                  </div>
                </b-col>
              </b-col>
            </b-row>
          </b-card-body>
        </b-form>
      </validation-observer>
    </b-card>
  </div>
</template>

<script>
import Ripple from 'vue-ripple-directive'
import { useInputImageRenderer } from '@core/comp-functions/forms/form-utils'
import { ref } from '@vue/composition-api'
import { ValidationObserver, ValidationProvider } from 'vee-validate'
import {
  BCard, BCardText, BForm, BCardHeader, BButton, BCardBody, BCol, BRow, BFormInput, BFormTextarea,
  BFormFile, BMedia, BMediaAside, BMediaBody, BLink, BImg,
} from 'bootstrap-vue'
import { required } from '@validations'
import { baseImgURL } from '@core/utils/utils'

import flatPickr from 'vue-flatpickr-component'
import vSelect from 'vue-select'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'UserInformation',
  components: {
    BCard,
    BCardText,
    BForm,
    BCardHeader,
    BButton,
    BCardBody,
    BCol,
    BRow,
    BFormInput,
    BFormTextarea,
    BFormFile,
    BMedia,
    BMediaAside,
    BMediaBody,
    BLink,
    BImg,
    flatPickr,
    vSelect,
    ValidationObserver,
    ValidationProvider,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      required,
      baseImgURL: baseImgURL(),
      userInfo: {
        picture: '/default.png',
      },
      genderOptions: [
        { text: 'Nam', value: 0 },
        { text: 'Nữ', value: 1 },
      ],
      verifyType: [
        { text: 'Số CMND', value: 1 },
        { text: 'Hộ chiếu', value: 2 },
      ],
      provinceOptions: [],
      districtOptions: [],
      profileFile: null,
      userData: JSON.parse(localStorage.getItem('userData')),
    }
  },
  async created() {
    await this.getProvince()
    await this.getUserInfo()
  },
  methods: {
    onChangeProvince(value) {
      this.userInfo.districtInfo = null
      this.districtOptions = []
      if (value) {
        this.districtOptions = value.districts
      }
    },
    getProvince() {
      return useJwt.axiosIns.get('/system-setting/getProvince')
        .then(res => {
          this.provinceOptions = res.data
        })
    },
    getUserInfo() {
      return useJwt.axiosIns.get('/user/getInfo')
        .then(res => {
          this.userInfo = res.data.user

          const provinceSelected = this.provinceOptions.find(x => x.id === this.userInfo.province)
          this.userInfo.verifyTypeInfo = this.verifyType.find(x => x.value === this.userInfo.verifyType)
          this.userInfo.genderInfo = this.genderOptions.find(x => x.value === this.userInfo.gender)
          this.userInfo.provinceInfo = provinceSelected
          this.districtOptions = provinceSelected && provinceSelected.districts
          this.userInfo.districtInfo = provinceSelected && provinceSelected.districts.find(x => x.id === this.userInfo.district)

          if (res.data.user.picture) {
            this.userInfo.picture = this.baseImgURL + res.data.user.picture
          } else {
            this.userInfo.picture = '/default.png'
          }
        })
    },
    updateUser() {
      const bodyFormData = new FormData()
      bodyFormData.append('verifyType', this.userInfo.verifyTypeInfo.value)
      bodyFormData.append('gender', this.userInfo.genderInfo.value)
      bodyFormData.append('province', this.userInfo.provinceInfo.id)
      bodyFormData.append('district', this.userInfo.districtInfo.id)
      bodyFormData.append('picture', this.profileFile)
      bodyFormData.append('fullName', this.userInfo.fullName)
      bodyFormData.append('phone', this.userInfo.phone)
      bodyFormData.append('dateOfBirth', this.userInfo.dateOfBirth)
      bodyFormData.append('idPassport', this.userInfo.idPassport)
      bodyFormData.append('address', this.userInfo.address)

      return useJwt.axiosIns.post('/user/updateInfo', bodyFormData)
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
          this.$router.push(localStorage.getItem('homeUrl'))
          this.$store.commit('app/UPDATE_USER_INFO', this.userInfo)
        })
        .catch(error => {
          this.$refs.updateUserForm.setErrors(error.response.data.error)
        })
    },
  },
  setup() {
    const refInputEl = ref(null)
    const previewEl = ref(null)

    const { inputImageRenderer } = useInputImageRenderer(refInputEl, result => {
      previewEl.value.src = result
    })

    return {
      refInputEl,
      previewEl,
      inputImageRenderer,
    }
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/libs/vue-select.scss';
@import 'src/@core/scss/vue/libs/vue-flatpicker.scss';
</style>
