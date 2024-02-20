<template>
  <b-row class="mt-5">
    <b-col
      lg="4"
      md="6"
      offset-md="4"
      class="mb-5"
    >
      <v-select
        :filterable="false"
        :options="userList"
        @search="onSearchUser"
        @input="onChangeSelectedUser"
      >
        <template
          slot="no-options"
        >
          Type to search user...
        </template>
        <template
          slot="option"
          slot-scope="option"
        >
          <div class="d-center">
            <span class="font-weight-bold d-block text-nowrap">
              {{ option.fullName }}
            </span>
            <small class="text-muted">{{ option.userEmail }}</small>
          </div>
        </template>
        <template
          slot="selected-option"
          slot-scope="option"
        >
          <div class="selected d-center">
            <span class="font-weight-bold d-block text-nowrap">
              {{ option.fullName }}
            </span>
          </div>
        </template>
      </v-select>
    </b-col>
    <b-col cols="12">
      <b-table
        striped
        hover
        responsive
        :current-page="currentPage"
        :per-page="itemsPerPage"
        :items="roleInfo.users"
        :fields="fields"
        :sticky-header="true"
      >
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
            <small class="text-muted">{{ data.item.userEmail }}</small>
          </b-media>
        </template>

        <template #cell(isActive)="data">
          <template>
            <b-badge
              pill
              :variant="data.value ? 'light-success' : 'light-danger'"
            >
              {{ data.value ? 'Active' : 'Inactive' }}
            </b-badge>
          </template>
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
              :id="`delete-user-${data.item.id}`"
              icon="TrashIcon"
              size="16"
              class="mx-1 cursor-pointer"
              @click="deleteUser(data.item)"
            />
          </div>
        </template>
      </b-table>
    </b-col>
    <b-col
      cols="12"
      sm="12"
      class="d-flex align-items-center justify-content-center justify-content-sm-end"
    >
      <b-pagination
        v-model="currentPage"
        :total-rows="totalItems"
        :per-page="itemsPerPage"
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
</template>

<script>
import {
  BAvatar,
  BBadge,
  BCol, BMedia, BRow, BTable, BPagination,
} from 'bootstrap-vue'
import moment from 'moment'
import { avatarText } from '@core/utils/filter'
import vSelect from 'vue-select'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'RoleUser',
  components: {
    BRow,
    BCol,
    BTable,
    BBadge,
    BMedia,
    BAvatar,
    BPagination,
    vSelect,
  },
  props: {
    roleInfo: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      currentPage: 1,
      itemsPerPage: 10,
      fields: [
        { key: 'id', label: '#' },
        { key: 'userName', label: 'User Name' },
        { key: 'userInfo' },
        { key: 'isActive', label: 'Status' },
        { key: 'uuidActive', label: 'Activation Status' },
        { key: 'createdAt', label: 'Date create', formatter: val => moment(val).format('DD/MM/YYYY HH:mm') },
        {
          key: 'actions', tdClass: 'text-center', thClass: 'text-center',
        },
      ],
      userList: [],
    }
  },
  computed: {
    totalItems() {
      return this.roleInfo.users.length
    },
  },
  created() {
  },
  setup() {
    return {
      avatarText,
    }
  },
  methods: {
    deleteUser(item) {
      this.roleInfo.users = this.roleInfo.users.filter(x => x.id !== item.id)
    },
    onSearchUser(search, loading) {
      if (search.length) {
        loading(true)
        useJwt.axiosIns
          .get(`/user/getListUser?search=${search}&page=1&limit=20`)
          .then(response => {
            this.userList = response.data.items
            loading(false)
          })
          .catch(() => {
            loading(false)
          })
      }
    },
    onChangeSelectedUser(selectedUser) {
      const existUser = this.roleInfo.users.find(x => x.id === selectedUser.id)
      if (existUser) return
      this.roleInfo.users.push(selectedUser)
    },
  },
}
</script>

<style scoped>

</style>
