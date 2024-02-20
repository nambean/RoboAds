<template>
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
              {{ generalInfo.id }}
            </div>
          </b-form-group>
        </b-col>
        <b-col cols="12">
          <b-form-group
            label="Role Key (*)"
            label-cols-md="4"
            :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
          >
            <validation-provider
              #default="{ errors }"
              name="Role key"
              rules="required"
            >
              <b-form-input
                id="menu-title"
                v-model="generalInfo.roleKey"
                :state="errors.length > 0 ? false: null"
                placeholder="Title"
              />
              <small class="text-danger">{{ errors[0] }}</small>
            </validation-provider>
          </b-form-group>
        </b-col>
        <b-col cols="12">
          <b-form-group
            label="Role Name (*)"
            label-cols-md="4"
            :label-class="`font-weight-bolder ${['xs', 'sm'].includes($store.getters['app/currentBreakPoint']) ? '' : 'text-right pr-3'}`"
          >
            <validation-provider
              #default="{ errors }"
              name="Role name"
              rules="required"
            >
              <b-form-input
                id="role-name"
                v-model="generalInfo.roleName"
                :state="errors.length > 0 ? false: null"
                placeholder="Title"
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
              v-model="generalInfo.description"
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
              v-model="generalInfo.isActive"
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
              {{ generalInfo.createdAtDisplay }}
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
              {{ generalInfo.updateAtDisplay }}
            </div>
          </b-form-group>
        </b-col>
      </b-row>
    </b-col>
  </b-row>
</template>

<script>
import {
  BCol, BFormCheckbox, BFormGroup, BFormInput, BRow,
} from 'bootstrap-vue'
import { ValidationProvider } from 'vee-validate'
import { required } from '@validations'

export default {
  name: 'RoleGeneralInfo',
  components: {
    BRow,
    BCol,
    BFormGroup,
    BFormInput,
    BFormCheckbox,
    ValidationProvider,
  },
  props: {
    generalInfo: {
      type: Object,
      default: () => {},
    },
    isNew: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      required,
    }
  },
}
</script>

<style scoped>

</style>
