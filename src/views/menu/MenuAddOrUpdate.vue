<template>
  <b-card
    no-body
    class="menu-update-card"
  >
    <!-- form -->
    <validation-observer
      ref="updateMenuForm"
      #default="{invalid}"
    >
      <b-form @submit.prevent="formAction">
        <b-card-header class="align-items-baseline">
          <div>
            <h3 class="mt-1">
              {{ isNew ? 'New' : 'Update' }} menu information {{ isNew ? '' : '- ' + menuInfo.title }}
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
              @click="deleteMenuAction()"
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
              @click="$router.push({ name: 'menu-manage'})"
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
                      {{ menuInfo.id }}
                    </div>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group
                    label="Title"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <validation-provider
                      #default="{ errors }"
                      name="Title"
                      rules="required"
                    >
                      <b-form-input
                        id="menu-title"
                        v-model="menuInfo.title"
                        :state="errors.length > 0 ? false: null"
                        placeholder="Title"
                      />
                      <small class="text-danger">{{ errors[0] }}</small>
                    </validation-provider>
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Route"
                    label-for="route"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <b-form-input
                      id="route"
                      v-model="menuInfo.route"
                      placeholder="Route"
                    />
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Icon"
                    label-for="icon"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <b-form-input
                      id="icon"
                      v-model="menuInfo.icon"
                      placeholder="Icon"
                    />
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
                      v-model="menuInfo.description"
                      placeholder="Description"
                    />
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Order"
                    label-for="order"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <validation-provider
                      #default="{ errors }"
                      rules="integer"
                      name="Order"
                    >
                      <b-form-input
                        id="order"
                        v-model="menuInfo.order"
                        :state="errors.length > 0 ? false:null"
                        placeholder="Order"
                      />
                      <small class="text-danger">{{ errors[0] }}</small>
                    </validation-provider>
                  </b-form-group>
                </b-col>
                <b-col cols="12">
                  <b-form-group
                    label="Parent"
                    label-for="parent"
                    label-cols-md="4"
                    :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
                  >
                    <v-select
                      id="parent"
                      v-model="selectedParent"
                      label="title"
                      :options="parents"
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
                      v-model="menuInfo.isActive"
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
                      {{ menuInfo.createdAtDisplay }}
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
                      {{ menuInfo.updateAtDisplay }}
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
import vSelect from 'vue-select'
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
    vSelect,
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
      menuInfo: {
        isActive: false,
      },
      parents: [],
      selectedParent: null,
      isNew: false,
    }
  },
  created() {
    this.isNew = !this.$route.query.id

    if (this.isNew) {
      this.menuInfo.isActive = true
    }

    useJwt.axiosIns.get('/menu/getAllMenu').then(res => {
      this.parents.push(...res.data)

      if (this.isNew) return
      useJwt.axiosIns.get(`/menu/getMenuById?id=${this.$route.query.id}`).then(response => {
        this.menuInfo = response.data
        this.selectedParent = this.parents.find(x => x.id === this.menuInfo.parent)
        this.menuInfo.createdAtDisplay = moment(response.data.createdAt).format('DD/MM/YYYY HH:mm:ss')
        this.menuInfo.updateAtDisplay = moment(response.data.updateAt).format('DD/MM/YYYY HH:mm:ss')
      })
    })
  },
  methods: {
    formAction() {
      if (this.selectedParent && this.selectedParent.id) {
        this.menuInfo.parent = this.selectedParent.id
      }

      const postURL = this.isNew ? '/menu/create' : '/menu/update'
      useJwt.axiosIns.post(postURL, this.menuInfo).then(() => {
        this.$toast({
          component: ToastificationContent,
          position: 'top-right',
          props: {
            title: 'Success',
            icon: 'CheckIcon',
            variant: 'success',
            text: `Success ${this.isNew ? 'create' : 'update'} menu ${this.menuInfo.title}`,
          },
        })
        this.$router.push({ name: 'menu-manage' })
      }).catch(error => {
        this.$refs.updateMenuForm.setErrors(error.response.data.error)
      })
    },
    deleteMenuAction() {
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

        useJwt.axiosIns.post('/menu/delete', this.menuInfo).then(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: `Success delete menu: ${this.menuInfo.title}`,
            },
          })
          this.$router.push({ name: 'menu-manage' })
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

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
</style>
