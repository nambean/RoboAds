import Vue from 'vue'
import VueRouter from 'vue-router'
import { isUserLoggedIn } from '@/auth/utils'
import { canNavigate } from '@/libs/acl/routeProtection'
import authentication from './authentication'
import role from './role'
import permission from './permission'

Vue.use(VueRouter)

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  scrollBehavior() {
    return { x: 0, y: 0 }
  },
  routes: [
    ...authentication,
    ...role,
    ...permission,
    {
      path: '/login',
      name: 'auth-login',
      component: () => import('@/views/Login.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
        redirectIfLoggedIn: true,
      },
    },
    {
      path: '/register',
      name: 'auth-register',
      component: () => import('@/views/Register.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/register-confirm',
      name: 'register-confirm',
      component: () => import('@/views/RegisterConfirm.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/forgot-password',
      name: 'auth-forgot-password',
      component: () => import('@/views/ForgotPassword.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/reset-password',
      name: 'auth-reset-password',
      component: () => import('@/views/ResetPassword.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/active-account',
      name: 'auth-active-account',
      component: () => import('@/views/ActiveAccount.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/home',
      name: 'home',
      component: () => import('@/views/account/AccountHome.vue'),
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('@/views/dashboard/AdminDashboard.vue'),
    },
    {
      path: '/cookie',
      name: 'cookie',
      component: () => import('@/views/cookie/Cookie.vue'),
    },
    {
      path: '/cookie/ads-account',
      name: 'ads-account',
      component: () => import('@/views/cookie/AdsAccount.vue'),
      meta: {
        navActiveLink: 'cookie',
      },
    },
    {
      path: '/cookie/campaign',
      name: 'campaign',
      component: () => import('@/views/cookie/Campaign.vue'),
      meta: {
        navActiveLink: 'cookie',
      },
    },
    {
      path: '/cookie/campaign/history',
      name: 'campaign-history',
      component: () => import('@/views/cookie/CampaignHistory.vue'),
      meta: {
        navActiveLink: 'cookie',
      },
    },
    {
      path: '/security',
      name: 'security',
      component: () => import('@/views/security/Security.vue'),
    },
    {
      path: '/not-authorized',
      name: 'misc-not-authorized',
      component: () => import('@/views/error/NotAuthorized.vue'),
      meta: {
        layout: 'full',
        resource: 'Auth',
      },
    },
    {
      path: '/error-404',
      name: 'error-404',
      component: () => import('@/views/error/Error404.vue'),
      meta: {
        layout: 'full',
      },
    },
    {
      path: '*',
      redirect: 'home',
    },
  ],
})

router.beforeEach((to, _, next) => {
  const isLoggedIn = isUserLoggedIn()

  if (!canNavigate(to) && !isLoggedIn) {
    // Redirect to login if not logged in
    return next({ name: 'auth-login' })
  }

  // Redirect if logged in
  if (to.meta.redirectIfLoggedIn && isLoggedIn) {
    next({ name: '/' })
  }

  return next()
})

// ? For splash screen
// Remove afterEach hook if you are not using splash screen
router.afterEach(() => {
  // Remove initial loading
  const appLoading = document.getElementById('loading-bg')
  if (appLoading) {
    appLoading.style.display = 'none'
  }
})

export default router
