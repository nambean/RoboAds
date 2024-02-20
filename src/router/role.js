export default [
  {
    path: '/role/manage',
    name: 'role-manage',
    component: () => import('@/views/role/RoleManage.vue'),
  },
  {
    path: '/role/add',
    name: 'role-add',
    component: () => import('@/views/role/RoleAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'role-manage',
    },
  },
  {
    path: '/role/update',
    name: 'role-update',
    component: () => import('@/views/role/RoleAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'role-manage',
    },
  },
]
