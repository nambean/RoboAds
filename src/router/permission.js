export default [
  {
    path: '/permission/manage',
    name: 'permission-manage',
    component: () => import('@/views/permission/PermissionManage.vue'),
  },
  {
    path: '/permission/add',
    name: 'permission-add',
    component: () => import('@/views/permission/PermissionAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'permission-manage',
    },
  },
  {
    path: '/permission/update',
    name: 'permission-update',
    component: () => import('@/views/permission/PermissionAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'permission-manage',
    },
  },
]
