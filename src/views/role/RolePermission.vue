<template>
  <div class="mt-2">
    <v-jstree
      id="role-permission-tree"
      :data="dataTree"
      allow-batch
      allow-transition
      show-checkbox
      multiple
      @item-click="nodeChecked"
    />
  </div>
</template>

<script>
import VJstree from 'vue-jstree'

import useJwt from '@/auth/jwt/useJwt'

export default {
  name: 'RolePermission',
  components: {
    VJstree,
  },
  props: {
    roleInfo: {
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
      dataTree: [],
      permissionList: [],
    }
  },
  watch: {
    roleInfo: {
      handler(value) {
        this.permissionList = value.permissions
        this.dataTree = this.buildTree(this.permissionList)
      },
    },
  },
  created() {
    if (this.isNew) {
      useJwt.axiosIns.get('/permission/getList?page=1&limit=1000').then(response => {
        this.permissionList = response.data.items
        this.dataTree = this.buildTree(this.permissionList, this.isNew)
      })
    } else {
      this.dataTree = this.buildTree()
    }
  },
  methods: {
    buildTree(permissionList, newTree) {
      const returnList = []

      if (newTree) {
        permissionList.forEach(permission => {
          // eslint-disable-next-line no-param-reassign
          permission.selected = false
        })
      }

      const resultGroup = this._.chain(this.permissionList.map(x => ({
        moduleName: x.moduleName, value: x.id, text: x.permissionName, selected: x.selected,
      }))).groupBy('moduleName').value()

      Object.keys(resultGroup).forEach(key => {
        returnList.push({
          value: this._.random(1000),
          text: key,
          opened: true,
          isLeaf: false,
          selected: false,
          children: resultGroup[key],
        })
      })

      return returnList
    },
    nodeChecked() {
      const flatten = (children, extractChildren) => Array.prototype.concat.apply(
        children, children && children.map(x => flatten(extractChildren(x) || [], extractChildren)),
      )
      const extractChildren = x => x.children
      const dataValue = []
      this.dataTree.forEach(tree => {
        const flat = flatten(extractChildren(tree), extractChildren).filter(x => x.selected).map(x => x.value)

        if (flat.length > 0) {
          dataValue.push(...flat)
        }
      })

      this.roleInfo.permissions = this.permissionList.filter(x => dataValue.includes(x.id))
        .map(x => ({ id: x.id, selected: true }))
    },
  },
}
</script>

<style lang="scss">
.tree-default .tree-selected {
  background: none !important;
}
</style>
