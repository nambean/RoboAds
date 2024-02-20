import {
  reactive, getCurrentInstance, watch, toRefs,
} from '@vue/composition-api'
import moment from 'moment'
import router from '@/router'

export const isObject = obj => typeof obj === 'object' && obj !== null

export const isToday = date => {
  const today = new Date()
  return (
    date.getDate() === today.getDate() && date.getMonth() === today.getMonth() && date.getFullYear() === today.getFullYear()
  )
}

export const convertObjectToParam = object => {
  let str = ''
  // eslint-disable-next-line guard-for-in,no-restricted-syntax
  for (const key in object) {
    if (object[key]) {
      if (str !== '') {
        str += '&'
      }
      str += `${key}=${object[key]}`
    }
  }
  return str
}

export const dateRangeObject = () => {
  const today = new Date()
  today.setHours(0, 0, 0, 0)

  const todayStart = today
  const todayEnd = new Date(today.getFullYear(), today.getMonth(), today.getDate(), 23, 59, 59, 0)
  const yesterdayStart = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 1, 0, 0, 0, 0)
  const yesterdayEnd = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 1, 23, 59, 59, 0)
  const thisMonthStart = new Date(today.getFullYear(), today.getMonth(), 1, 0, 0, 0, 0)
  const thisMonthEnd = new Date(today.getFullYear(), today.getMonth() + 1, 0, 23, 59, 59, 0)
  const lastMonthStart = new Date(today.getFullYear(), today.getMonth() - 1, 1, 0, 0, 0, 0)
  const lastMonthEnd = new Date(today.getFullYear(), today.getMonth(), 0, 23, 59, 59, 0)
  const thisYearStart = new Date(today.getFullYear(), 0, 1, 0, 0, 0, 0)
  const thisYearEnd = new Date(today.getFullYear(), 11, 31, 23, 59, 59, 0)
  const lastYearStart = new Date(today.getFullYear() - 1, 0, 1, 0, 0, 0, 0)
  const lastYearEnd = new Date(today.getFullYear() - 1, 11, 31, 0, 0, 0, 0)

  return {
    All: [null, null],
    Today: [todayStart, todayEnd],
    Yesterday: [yesterdayStart, yesterdayEnd],
    'This month': [thisMonthStart, thisMonthEnd],
    'Last month': [lastMonthStart, lastMonthEnd],
    'This year': [thisYearStart, thisYearEnd],
    'Last year': [lastYearStart, lastYearEnd],
  }
}

export const convertToInternationalCurrencySystem = labelValue => {
  const number = Math.abs(Number(labelValue.toFixed(2)))
  if (number >= 1.0e+9) {
    return `${number / 1.0e+9}B`
  }

  if (number >= 1.0e+6) {
    return `${number / 1.0e+6}M`
  }

  if (number >= 1.0e+3) {
    return `${number / 1.0e+3}K`
  }

  return number
}

export const formatDateTime = date => {
  const checkDate = moment(date)
  if (checkDate.isValid()) {
    return moment(date).format('DD/MM/YYYY')
  }
  return ''
}

export const calculatorFixedHeaderTableHeight = positiveHeight => {
  const appHeight = document.getElementById('app').clientHeight
  let navbarHeight = 0

  const navbarContainer = document.getElementsByClassName('navbar-container')
  if (navbarContainer && navbarContainer.length > 0) {
    navbarHeight = navbarContainer[0].clientHeight
  }

  let footerHeight = 0
  const footer = document.getElementsByClassName('footer')
  if (footer && footer.length > 0) {
    footerHeight = footer[0].clientHeight
  }

  const tableSticky = document.getElementsByClassName('b-table-sticky-header')
  if (tableSticky && tableSticky.length > 0) {
    document.getElementsByClassName('b-table-sticky-header')[0].style.maxHeight = `${appHeight - navbarHeight - footerHeight - positiveHeight}px`
  }
}

const getRandomFromArray = array => array[Math.floor(Math.random() * array.length)]

// ? Light and Dark variant is not included
// prettier-ignore
export const getRandomBsVariant = () => getRandomFromArray(['primary', 'secondary', 'success', 'warning', 'danger', 'info'])

export const isDynamicRouteActive = route => {
  const { route: resolvedRoute } = router.resolve(route)
  return resolvedRoute.path === router.currentRoute.path
}

// Thanks: https://medium.com/better-programming/reactive-vue-routes-with-the-composition-api-18c1abd878d1
export const useRouter = () => {
  const vm = getCurrentInstance().proxy
  const state = reactive({
    route: vm.$route,
  })

  watch(
    () => vm.$route,
    r => {
      state.route = r
    },
  )

  return { ...toRefs(state), router: vm.$router }
}

export const baseImgURL = () => (process.env.NODE_ENV === 'production' ? 'https://ads0806.com/' : 'http://localhost:3000/')

export const formatNumber = num => Intl.NumberFormat().format(num)

/**
 * This is just enhancement over Object.extend [Gives deep extend]
 * @param {target} a Object which contains values to be overridden
 * @param {source} b Object which contains values to override
 */
// export const objectExtend = (a, b) => {
//   // Don't touch 'null' or 'undefined' objects.
//   if (a == null || b == null) {
//     return a
//   }

//   Object.keys(b).forEach(key => {
//     if (Object.prototype.toString.call(b[key]) === '[object Object]') {
//       if (Object.prototype.toString.call(a[key]) !== '[object Object]') {
//         // eslint-disable-next-line no-param-reassign
//         a[key] = b[key]
//       } else {
//         // eslint-disable-next-line no-param-reassign
//         a[key] = objectExtend(a[key], b[key])
//       }
//     } else {
//       // eslint-disable-next-line no-param-reassign
//       a[key] = b[key]
//     }
//   })

//   return a
// }
