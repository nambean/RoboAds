export default [
  {
    path: '/user-information',
    name: 'user-information',
    component: () => import('@/views/user/UserInformation.vue'),
  },
  {
    path: '/user/manage',
    name: 'user-manage',
    component: () => import('@/views/user/UserManage.vue'),
  },
  {
    path: '/user/create-or-update',
    name: 'user-update',
    component: () => import('@/views/user/UserUpdate.vue'),
    meta: {
      navActiveLink: 'user-manage',
    },
  },
  {
    path: '/menu/manage',
    name: 'menu-manage',
    component: () => import('@/views/menu/MenuManage.vue'),
  },
  {
    path: '/menu/update',
    name: 'menu-update',
    component: () => import('@/views/menu/MenuAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'menu-manage',
    },
  },
  {
    path: '/menu/add',
    name: 'menu-add',
    component: () => import('@/views/menu/MenuAddOrUpdate.vue'),
    meta: {
      navActiveLink: 'menu-manage',
    },
  },
]
