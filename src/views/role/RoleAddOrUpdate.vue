<template>
  <b-card
    no-body
    class="role-update-card"
  >
    <!-- form -->
    <validation-observer
      ref="updateRoleForm"
      #default="{invalid}"
    >
      <b-form @submit.prevent="formAction">
        <b-card-header class="align-items-baseline">
          <div>
            <h3>{{ isNew ? 'New' : 'Update' }} role information {{ isNew ? '' : '- ' + roleInfo.roleName }}</h3>
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
              v-ripple.400="'rgba(113, 102, 240, 0.15)'"
              variant="outline-primary"
              @click="$router.push({ name: 'role-manage'})"
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
          <b-tabs
            content-class="col-12"
            pills
          >
            <b-tab>
              <template #title>
                <feather-icon icon="HomeIcon" />
                <span>General</span>
              </template>
              <role-general-info
                :general-info="roleInfo"
                :is-new="isNew"
              />
            </b-tab>
            <b-tab>
              <template #title>
                <feather-icon icon="MoreVerticalIcon" />
                <span>Menu</span>
              </template>
              <role-menu
                :role-info="roleInfo"
                :is-new="isNew"
              />
            </b-tab>
            <b-tab>
              <template #title>
                <feather-icon icon="ShieldIcon" />
                <span>Permission</span>
              </template>
              <role-permission
                :role-info="roleInfo"
                :is-new="isNew"
              />
            </b-tab>
            <b-tab>
              <template #title>
                <feather-icon icon="UsersIcon" />
                <span>Users</span>
              </template>
              <role-user
                :role-info="roleInfo"
              />
            </b-tab>
          </b-tabs>
        </b-card-body>
      </b-form>
    </validation-observer>
  </b-card>
</template>

<script>
import {
  ValidationObserver,
} from 'vee-validate'
import {
  BCard, BCardHeader, BCardBody, BButton, BForm,
  BTabs, BTab,
} from 'bootstrap-vue'
import moment from 'moment'
import Ripple from 'vue-ripple-directive'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import useJwt from '@/auth/jwt/useJwt'
import RoleGeneralInfo from '@/views/role/RoleGeneralInfo.vue'
import RoleUser from '@/views/role/RoleUser.vue'
import RoleMenu from '@/views/role/RoleMenu.vue'
import RolePermission from '@/views/role/RolePermission.vue'

export default {
  components: {
    RoleGeneralInfo,
    RoleUser,
    RoleMenu,
    RolePermission,
    BCard,
    BCardHeader,
    BCardBody,
    BButton,
    BForm,
    ValidationObserver,
    BTabs,
    BTab,
  },
  directives: {
    Ripple,
  },
  data() {
    return {
      roleInfo: {
        isActive: false,
        users: [],
        menus: [],
        permissions: [],
      },
      isNew: false,
    }
  },
  created() {
    this.isNew = !this.$route.query.id

    if (this.isNew) {
      this.roleInfo.isActive = true
    } else {
      useJwt.axiosIns.get(`/role/getRoleById?id=${this.$route.query.id}`).then(response => {
        this.roleInfo = response.data
        this.roleInfo.createdAtDisplay = moment(response.data.createdAt).format('DD/MM/YYYY HH:mm:ss')
        this.roleInfo.updateAtDisplay = moment(response.data.updateAt).format('DD/MM/YYYY HH:mm:ss')
      })
    }
  },
  methods: {
    formAction() {
      if (this.selectedParent && this.selectedParent.id) {
        this.roleInfo.parent = this.selectedParent.id
      }

      this.roleInfo.users = this.roleInfo.users.map(x => ({
        id: x.id,
      }))
      this.roleInfo.permissions = this.roleInfo.permissions.filter(x => x.selected)

      const postURL = this.isNew ? '/role/create' : '/role/update'
      useJwt.axiosIns.post(postURL, this.roleInfo).then(() => {
        this.$toast({
          component: ToastificationContent,
          position: 'top-right',
          props: {
            title: 'Success',
            icon: 'CheckIcon',
            variant: 'success',
            text: `Success ${this.isNew ? 'create' : 'update'} role ${this.roleInfo.roleName}`,
          },
        })
        this.$router.push({ name: 'role-manage' })
      }).catch(error => {
        this.$refs.updateRoleForm.setErrors(error.response.data.error)
      })
    },
  },
}
</script>

<style lang="scss">
@import '@core/scss/vue/libs/vue-select.scss';
</style>
