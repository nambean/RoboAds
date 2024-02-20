<template>
  <div class="container">
    <b-row>
      <b-col cols="12">
        <b-card>
          <b-row>
            <!-- Per Page -->
            <b-col
              cols="12"
              md="7"
              class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
            >
              <b-button
                variant="primary"
                :to="{ name: 'user-update' }"
              >
                Add User
              </b-button>
            </b-col>
            <!-- Search -->
            <b-col
              cols="12"
              md="5"
            >
              <b-form-input
                v-model="pagination.search"
                class="content-right"
                placeholder="Search..."
                right
                @keypress="searchUser($event)"
              />
            </b-col>
          </b-row>
        </b-card>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="12">
        <b-card>
          <b-table
            class="table-border-dashed table-hover-animation"
            responsive
            :striped="true"
            :hover="true"
            :fields="fields"
            :items="userList"
          >
            <template #cell(stt)="data">
              {{ (pagination.page - 1) * pagination.limit + data.index + 1 }}
            </template>
            <template #cell(userInfo)="data">
              <b-media vertical-align="center">
                <template #aside>
                  <b-avatar
                    size="45"
                    :src="data.item.avatar"
                    :text="avatarText(data.item.fullName)"
                    variant="light-warning"
                  />
                </template>
                <span class="font-weight-bold d-block text-nowrap">
                  {{ data.item.fullName }}
                </span>
                <small class="text-muted">Account: <span class="font-weight-bold text-nowrap">{{ data.item.userName }}</span></small>
                <br>
                <small v-if="data.item.userEmail" class="text-muted">Email: <span class="font-weight-bold text-nowrap">{{ data.item.userEmail }}</span></small>
              </b-media>
            </template>

            <template #cell(uuidActive)="data">
              <template>
                <b-badge
                  pill
                  :variant="!!data.value ? 'light-danger' : 'light-success'"
                >
                  {{ !!data.value ? 'Not activated' : 'Activated' }}
                </b-badge>
              </template>
            </template>

            <template #cell(actions)="data">
              <div class="text-nowrap">
                <feather-icon
                  icon="EditIcon"
                  class="cursor-pointer mr-1"
                  size="16"
                  @click="routeToUpdateUser(data.item)"
                />
                <feather-icon
                  icon="TrashIcon"
                  size="16"
                  class="cursor-pointer"
                  @click="deleteUser(data.item)"
                />
              </div>
            </template>
          </b-table>
        </b-card>
      </b-col>
    </b-row>
    <div class="mx-2 mb-2">
      <b-row>
        <b-col
          cols="12"
          sm="12"
          class="d-flex align-items-center justify-content-center justify-content-sm-end"
        >
          <b-pagination
            v-model="pagination.page"
            :total-rows="pagination.totalItems"
            :per-page="pagination.limit"
            first-number
            last-number
            class="mb-0 mt-1 mt-sm-0"
            prev-class="prev-item"
            next-class="next-item"
          >
            <template #prev-text>
              <feather-icon
                icon="ChevronLeftIcon"
                size="18"
              />
            </template>
            <template #next-text>
              <feather-icon
                icon="ChevronRightIcon"
                size="18"
              />
            </template>
          </b-pagination>
        </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BTable, BBadge, BMedia, BAvatar, BPagination, BFormInput, BButton,
  // BDropdown, BDropdownItem,BFormRating,
} from 'bootstrap-vue'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import { avatarText } from '@core/utils/filter'
import moment from 'moment'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'UserManage',
  components: {
    BRow,
    BCol,
    BCard,
    BTable,
    BBadge,
    BMedia,
    BAvatar,
    BPagination,
    BFormInput,
    BButton,
    // BFormRating,
    // BDropdown,
    // BDropdownItem,
  },
  data() {
    return {
      fields: [
        { key: 'stt', label: 'STT' },
        { key: 'userInfo', label: 'Information' },
        { key: 'uuidActive', label: 'Active' },
        { key: 'createdAt', label: 'Created At', formatter: val => moment(val).format('DD/MM/YYYY HH:mm') },
        { key: 'actions', label: 'Actions', class: 'text-center' },
      ],
      userList: [],
      pagination: {
        search: '',
        page: 1,
        limit: 10,
        totalItems: 0,
      },
    }
  },
  watch: {
    'pagination.page': {
      handler(value) {
        this.getUserList(value)
      },
    },
  },
  created() {
    this.getUserList(this.pagination.page)
  },
  setup() {
    return {
      avatarText,
    }
  },
  methods: {
    routeToUpdateUser(item) {
      this.$router.push({ name: 'user-update', query: { id: item.id } })
    },
    getUserList(page) {
      useJwt.axiosIns.get(`/user/getListUser?search=${this.pagination.search}&page=${page}&limit=${this.pagination.limit}`).then(response => {
        this.userList = response.data.items
        this.pagination.totalItems = response.data.meta.totalItems
      })
    },
    searchUser(event) {
      if (event.keyCode === 13) {
        this.getUserList(1)
      }
    },
    deleteUser(user) {
      this.$swal({
        title: `Are you sure to delete ${user.fullName}?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        customClass: {
          confirmButton: 'btn btn-primary cursor-pointer',
          cancelButton: 'btn btn-outline-danger ml-1 cursor-pointer',
          cancelButtonText: 'Cancel',
        },
        buttonsStyling: false,
      }).then(result => {
        if (!result.value) {
          return
        }
        useJwt.axiosIns.post(`/user/delete?id=${user.id}`, this.userInfo).then(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: `Success delete user ${user.fullName}`,
            },
          })
        }).catch(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Fail',
              icon: 'XIcon',
              variant: 'danger',
              text: `Fail to delete user ${user.fullName}`,
            },
          })
        })
      })
    },
  },
}
</script>

<style lang="scss" scoped>

</style>
