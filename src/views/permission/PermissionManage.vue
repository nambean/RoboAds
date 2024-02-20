<template>
  <div class="container">
    <b-card
      no-body
      class="permission-manage-card"
    >
      <b-card-body>
        <!-- Table Top -->
        <b-row class="mb-2">
          <!-- Per Page -->
          <b-col
            cols="12"
            md="7"
            class="d-flex align-items-center justify-content-start mb-1 mb-md-0"
          >
            <b-button
              variant="primary"
              :to="{ name: 'permission-add'}"
            >
              Add Permission
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
              @keypress="searchPermission($event)"
            />
          </b-col>
        </b-row>
      </b-card-body>
      <div class="mx-2">
        <b-table
          class="table-border-dashed table-hover-animation"
          responsive
          :striped="true"
          :hover="true"
          :fields="fields"
          :items="permissionList"
          @row-clicked="routeToUpdate"
        >
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

          <template #cell(actions)="data">
            <div class="text-nowrap">
              <feather-icon
                :id="`update-permission-${data.item.id}`"
                icon="EditIcon"
                class="cursor-pointer"
                size="16"
                @click="routeToUpdate(data.item)"
              />
              <feather-icon
                :id="`delete-permission-${data.item.id}`"
                icon="TrashIcon"
                size="16"
                class="mx-1 cursor-pointer"
                @click="deletePermissionAction(data.item.id)"
              />
            </div>
          </template>
        </b-table>
      </div>
      <div class="mx-2 mb-2">
        <b-row>
          <b-col
            cols="12"
            sm="12"
            class="d-flex align-items-center justify-content-center justify-content-sm-end"
          >
            <b-pagination
              ref="pagination"
              v-model="pagination.currentPage"
              :total-rows="pagination.totalItems"
              :per-page="pagination.itemsPerPage"
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
    </b-card>
  </div>
</template>

<script>
import {
  BRow, BCol, BCard, BCardBody, BTable, BPagination, BButton, BFormInput, BBadge,
} from 'bootstrap-vue'
import ToastificationContent from '@core/components/toastification/ToastificationContent.vue'
import moment from 'moment'
import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'PermissionManage',
  components: {
    BRow,
    BCol,
    BCard,
    BCardBody,
    BTable,
    BPagination,
    BButton,
    BFormInput,
    BBadge,
  },
  data() {
    return {
      fields: [
        { key: 'id', label: '#' },
        { key: 'moduleName', label: 'Module Name' },
        { key: 'permissionName', label: 'Permission Name' },
        { key: 'description', label: 'Description' },
        { key: 'isActive', label: 'Status' },
        { key: 'createdAt', label: 'Date create', formatter: val => moment(val).format('DD/MM/YYYY HH:mm') },
        { key: 'actions' },
      ],
      permissionList: [],
      pagination: {
        search: '',
        currentPage: 1,
        itemsPerPage: 10,
        totalItems: 0,
      },
    }
  },
  watch: {
    'pagination.currentPage': {
      handler(value) {
        // this.$router.replace({ query: { page: value } }).catch(() => {})
        this.getPermissionList(value)
      },
    },
  },
  created() {
    this.getPermissionList(this.pagination.currentPage)
  },
  methods: {
    routeToUpdate(item) {
      return this.$router.push({ name: 'permission-update', query: { id: item.id } })
    },
    getPermissionList(page) {
      useJwt.axiosIns.get(`/permission/getList?search=${this.pagination.search}&page=${page}&limit=${this.pagination.itemsPerPage}`).then(response => {
        this.permissionList = response.data.items
        this.pagination.totalItems = response.data.meta.totalItems
      })
    },
    searchPermission(event) {
      if (event.keyCode === 13) {
        this.getPermissionList(1)
      }
    },
    deletePermissionAction(id) {
      const deletePermission = this.permissionList.find(x => x.id === id)
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

        useJwt.axiosIns.post('/permission/delete', deletePermission).then(() => {
          this.$toast({
            component: ToastificationContent,
            position: 'top-right',
            props: {
              title: 'Success',
              icon: 'CheckIcon',
              variant: 'success',
              text: `Success delete permission: ${deletePermission.permissionName}`,
            },
          })
          this.getPermissionList(this.pagination.currentPage)
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

<style lang="scss" scoped>

</style>
