<template>
  <div class="mt-2">
    <v-jstree
      id="role-menu-tree"
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
  name: 'RoleMenu',
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
      menuList: [],
    }
  },
  watch: {
    roleInfo: {
      handler(value) {
        useJwt.axiosIns.get('/menu/getList?page=1&limit=1000').then(response => {
          this.menuList = response.data.items
          this.dataTree = this.buildTreeFromFlatList(response.data.items, value.menus)
        })
      },
    },
  },
  created() {
    if (this.isNew) {
      useJwt.axiosIns.get('/menu/getList?page=1&limit=1000').then(response => {
        this.menuList = response.data.items
        this.dataTree = this.buildTreeFromFlatList(response.data.items, [])
      })
    }
  },
  methods: {
    nodeChecked() {
      const flatten = (children, extractChildren) => Array.prototype.concat.apply(
        children, children && children.map(x => flatten(extractChildren(x) || [], extractChildren)),
      )
      const extractChildren = x => x.children
      const dataValue = []
      this.dataTree.forEach(tree => {
        const flat = flatten(extractChildren(tree), extractChildren).filter(x => x.selected).map(x => x.value)

        if (tree.selected) {
          dataValue.push(tree.value)
        }

        if (flat.length > 0) {
          dataValue.push(...flat)
        }
      })

      this.roleInfo.menus = this.menuList.filter(x => dataValue.includes(x.id))
    },
    buildTreeFromFlatList(menuList, userMenu) {
      menuList.forEach(menu => {
        const selectedMenu = userMenu && userMenu.find(x => x.id === menu.id)
        // eslint-disable-next-line no-param-reassign
        menu.selected = !!selectedMenu
      })

      const listParent = menuList.filter(x => !x.parent)
      const returnList = []
      listParent.forEach(parent => {
        returnList.push({
          value: parent.id,
          text: parent.title,
          opened: true,
          isLeaf: false,
          selected: parent.selected,
          children: menuList
            .filter(x => x.parent === parent.id)
            .map(x => ({
              value: x.id, text: x.title, isLeaf: false, selected: x.selected,
            })),
        })
      })
      return returnList
    },
  },
}
</script>

<style lang="scss">
.tree-default .tree-selected {
  background: none !important;
}
</style>
