import Vue from 'vue'
import { ToastPlugin, ModalPlugin } from 'bootstrap-vue'
import VueCompositionAPI from '@vue/composition-api'

import VueLodash from 'vue-lodash'
import lodash from 'lodash'

import SocketIO from 'socket.io-client'
import VueSocketIO from 'vue-socket.io'

import i18n from '@/libs/i18n'
import router from './router'
import store from './store'
import App from './App.vue'

// Global Components
import './global-components'

// 3rd party plugins
import '@axios'
import '@/libs/portal-vue'
import '@/libs/clipboard'
import '@/libs/toastification'
import '@/libs/sweet-alerts'
import '@/libs/vue-select'
import '@/libs/tour'
import '@/libs/loading-overlay'
import 'vue2-daterange-picker/dist/vue2-daterange-picker.css'
import '@core/scss/vue/libs/chart-apex.scss'

// Lodash
Vue.use(VueLodash, { name: 'custom', lodash })

// BSV Plugin Registration
Vue.use(ToastPlugin)
Vue.use(ModalPlugin)

// Composition API
Vue.use(VueCompositionAPI)

// Feather font icon - For form-wizard
// * Shall remove it if not using font-icons of feather-icons - For form-wizard
require('@core/assets/fonts/feather/iconfont.css') // For form-wizard

// import core styles
require('@core/scss/core.scss')

// import assets styles
require('@/assets/scss/style.scss')

Vue.config.productionTip = false

Vue.use(new VueSocketIO({
  debug: true,
  connection: SocketIO(process.env.NODE_ENV === 'production' ? 'https://ads0806.com/' : 'http://localhost:3000/', {
    autoConnect: false,
    transports: ['websocket'],
    // transports: ['polling', 'websocket'],
  }),
}))

new Vue({
  router,
  store,
  i18n,
  render: h => h(App),
}).$mount('#app')
