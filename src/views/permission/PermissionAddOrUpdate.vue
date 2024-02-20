<template>
  <b-card
    no-body
    class="permission-update-card"
  >
    <!-- form -->
    <validation-observer
      ref="updatePermissionForm"
      #default="{invalid}"
    >
      <b-form @submit.prevent="formAction">
        <b-card-header class="align-items-baseline">
          <div>
            <h3 class="mt-1">
              {{ isNew ? 'New' : 'Update' }} permission information {{ isNew ? '' : '- ' + permissionInfo.permissionName }}
            </h3>
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
              <span class="d-sm-inline d-none align-middle">{{ isNew ? 'Create': 'Update' }}</span>
            </b-button>
            <b-button
              v-if="!isNew"
              v-ripple.400="'rgba(255, 255, 255, 0.15)'"
              class="mr-1"
              variant="danger"
              @click="deletePermissionAction()"
            >
              <feather-icon
                icon="TrashIcon"
                size="15"
                class="mr-50"
              />
              <span class="d-sm-inline d-none align-middle">Delete</span>
            </b-button>
            <b-button
              v-ripple.400="'rgba(113, 102, 240, 0.15)'"
              variant="outline-primary"
              @click="$router.push({ name: 'permission-manage'})"
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
                  v-if="!isNew"
                  cols="12"
                >
                  <b-form-group
                    label="ID"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <div class="mt-50">
                      {{ permissionInfo.id }}
                    </div>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group
                    label="Module Name (*)"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <validation-provider
                      #default="{ errors }"
                      name="Module Name"
                      rules="required"
                      vid="moduleName"
                    >
                      <b-form-input
                        id="permission-title"
                        v-model="permissionInfo.moduleName"
                        :state="errors.length > 0 ? false: null"
                        placeholder="Module Name"
                      />
                      <small class="text-danger">{{ errors[0] }}</small>
                    </validation-provider>
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Permission Name (*)"
                    label-for="route"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <validation-provider
                      #default="{ errors }"
                      name="Permission Name"
                      rules="required"
                      vid="permissionName"
                    >
                      <b-form-input
                        id="route"
                        v-model="permissionInfo.permissionName"
                        :state="errors.length > 0 ? false: null"
                        placeholder="Permission Name"
                      />
                      <small class="text-danger">{{ errors[0] }}</small>
                    </validation-provider>
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Permission URL (*)"
                    label-for="icon"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <validation-provider
                      #default="{ errors }"
                      name="Permission URL"
                      rules="required"
                      vid="permissionURL"
                    >
                      <b-form-input
                        id="route"
                        v-model="permissionInfo.permissionURL"
                        :state="errors.length > 0 ? false: null"
                        placeholder="Permission URL"
                      />
                      <small class="text-danger">{{ errors[0] }}</small>
                    </validation-provider>
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Description"
                    label-for="description"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <b-form-input
                      id="description"
                      v-model="permissionInfo.description"
                      placeholder="Description"
                    />
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Active"
                    label-for="u-is-active"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <b-form-checkbox
                      id="u-is-active"
                      v-model="permissionInfo.isActive"
                      class="custom-control-primary mt-50"
                      name="check-button"
                      switch
                    >
                      <span class="switch-icon-left">
                        <feather-icon icon="CheckIcon" />
                      </span>
                      <span class="switch-icon-right">
                        <feather-icon icon="XIcon" />
                      </span>
                    </b-form-checkbox>
                  </b-form-group>
                </b-col>
                <b-col
                  v-if="!isNew"
                  cols="12"
                >
                  <b-form-group
                    label="Created at"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <div class="mt-50">
                      {{ permissionInfo.createdAtDisplay }}
                    </div>
                  </b-form-group>
                </b-col>
                <b-col
                  v-if="!isNew"
                  cols="12"
                >
                  <b-form-group
                    label="Updated at"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <div class="mt-50">
                      {{ permissionInfo.updateAtDisplay }}
                    </div>
                  </b-form-group>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </b-card-body>
      </b-form>
    </validation-observer>
  </b-card>
</template>

<script>
import { ValidationProvider, ValidationObserver } from 'vee-validate'
import {
  BCard, BCardHeader, BCardBody, BButton, BRow, BCol, BForm, BFormGroup, BFormInput, BFormCheckbox,
} from 'bootstrap-vue'
import { required, integer } from '@validations'
import moment from 'moment'
import Ripple from 'vue-ripple-directive'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import useJwt from '@/auth/jwt/useJwt'

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
    BFormCheckbox,
    ValidationProvider,
    ValidationObserver,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      required,
      integer,
      permissionInfo: {
        isActive: false,
      },
      isNew: false,
    }
  },
  created() {
    this.isNew = !this.$route.query.id

    if (this.isNew) {
      this.permissionInfo.isActive = true
      return
    }

    useJwt.axiosIns
      .get(`/permission/getPermissionById?id=${this.$route.query.id}`)
      .then(response => {
        this.permissionInfo = response.data
        this.permissionInfo.createdAtDisplay = moment(response.data.createdAt).format('DD/MM/YYYY HH:mm:ss')
        this.permissionInfo.updateAtDisplay = moment(response.data.updateAt).format('DD/MM/YYYY HH:mm:ss')
      })
  },
  methods: {
    formAction() {
      if (this.selectedParent && this.selectedParent.id) {
        this.permissionInfo.parent = this.selectedParent.id
      }

      const postURL = this.isNew ? '/permission/create' : '/permission/update'
      useJwt.axiosIns.post(postURL, this.permissionInfo).then(() => {
        this.$toast({
          component: ToastificationContent,
          position: 'top-right',
          props: {
            title: 'Success',
            icon: 'CheckIcon',
            variant: 'success',
            text: `Success ${this.isNew ? 'create' : 'update'} permission ${this.permissionInfo.permissionName}`,
          },
        })
        this.$router.push({ name: 'permission-manage' })
      }).catch(error => {
        this.$refs.updatePermissionForm.setErrors(error.response.data.error)
      })
    },
    deletePermissionAction() {
      this.$swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        customClass: {
          confirmButton: 'btn btn-primary',
          cancelButton: 'btn btn-outline-danger ml-1',
        },
        buttonsStyling: false,
      }).then(result => {
        if (!result.value) {
          return
        }

        useJwt.axiosIns.post('/permission/delete', this.permissionInfo).then(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: `Success delete permission: ${this.permissionInfo.permissionName}`,
            },
          })
          this.$router.push({ name: 'permission-manage' })
        }).catch(error => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Fail',
              icon: 'XIcon',
              variant: 'danger',
              text: `${error.response.data.error}`,
            },
          })
        })
      })
    },
  },
}
</script>
