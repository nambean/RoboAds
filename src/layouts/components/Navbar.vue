<template>
  <div class="navbar-container d-flex content align-items-center">

    <!-- Nav Menu Toggler -->
    <ul class="nav navbar-nav d-xl-none">
      <li class="nav-item">
        <b-link
          class="nav-link"
          @click="toggleVerticalMenuActive"
        >
          <feather-icon
            icon="MenuIcon"
            size="21"
          />
        </b-link>
      </li>
    </ul>

    <!-- Left Col -->
    <div class="bookmark-wrapper align-items-center flex-grow-1 d-none d-lg-flex">
      <dark-Toggler class="d-none d-lg-block" />
    </div>

    <b-navbar-nav class="nav align-items-center ml-auto">
      <b-nav-item-dropdown
        right
        toggle-class="d-flex align-items-center dropdown-user-link"
        class="dropdown-user"
      >
        <template #button-content>
          <div class="d-sm-flex user-nav">
            <p class="user-name font-weight-bold mb-30">
              {{ userData.fullName }}
            </p>
            <span class="user-status">{{ userData.titleString ? userData.titleString: userData.roleName }}</span>
          </div>
          <b-avatar
            size="40"
            variant="light-primary"
            badge
            :src="userData && userData.picture"
            class="badge-minimal"
            badge-variant="success"
          />
        </template>
        <b-dropdown-item
          link-class="d-flex align-items-center"
          @click="$router.push({ name: 'user-information'})"
        >
          <div class="d-flex mr-50">
            <feather-icon
              size="16"
              icon="UserIcon"
              class="mr-50"
            />
          </div>
          <span>Cá nhân</span>
        </b-dropdown-item>

        <b-dropdown-item
          link-class="d-flex align-items-center"
          @click="logout"
        >
          <div class="d-flex mr-50">
            <feather-icon
              size="16"
              icon="LogOutIcon"
              class="mr-50"
            />
          </div>
          <span>Đăng xuất</span>
        </b-dropdown-item>
      </b-nav-item-dropdown>
    </b-navbar-nav>
  </div>
</template>

<script>
import {
  BLink, BNavbarNav, BNavItemDropdown, BDropdownItem, BAvatar,
} from 'bootstrap-vue'
import { baseImgURL } from '@core/utils/utils'
import { computed, watch } from '@vue/composition-api'
import DarkToggler from '@core/layouts/components/app-navbar/components/DarkToggler.vue'
import useJwt from '@/auth/jwt/useJwt'

export default {
  components: {
    BLink,
    BNavbarNav,
    BNavItemDropdown,
    BDropdownItem,
    BAvatar,

    // Navbar Components
    DarkToggler,
  },
  props: {
    toggleVerticalMenuActive: {
      type: Function,
      default: () => {
      },
    },
  },
  data() {
    return {
      baseImgURL: baseImgURL(),
      userData: JSON.parse(localStorage.getItem('userData')),
      accessToken: localStorage.getItem('accessToken'),
    }
  },
  created() {
    if (this.userData.picture) {
      this.userData.picture = this.baseImgURL + this.userData.picture
    } else {
      this.userData.picture = '/default.png'
    }

    this.$store.commit('app/ACCESS_TOKEN', this.accessToken)
    this.$socket.io.opts.query = {
      accessToken: this.accessToken,
    }

    this.$socket.connect()

    this.sockets.subscribe('user-vip', vip => {
      if (vip) {
        this.userData.vip = vip
        localStorage.setItem('userData', JSON.stringify(this.userData))
      }
    })

    this.sockets.subscribe('user-title', title => {
      if (title) {
        this.userData.titleString = title
        localStorage.setItem('userData', JSON.stringify(this.userData))
      }
    })

    const currentUser = computed(() => this.$store.getters['app/currentUser'])
    watch(currentUser, () => {
      useJwt.axiosIns.get('/user/getInfo').then(res => {
        const { user } = res.data
        this.userData.picture = user.picture
        this.userData.fullName = user.fullName
        localStorage.setItem('userData', JSON.stringify(this.userData))
        if (user.picture) {
          this.userData.picture = this.baseImgURL + this.userData.picture
        }
      })
    })
  },
  destroyed() {
    this.$socket.disconnect()
  },
  methods: {
    logout() {
      localStorage.removeItem(useJwt.jwtConfig.storageTokenKeyName)
      localStorage.removeItem('userData')
      localStorage.removeItem('menus')
      localStorage.removeItem('homeUrl')

      this.$socket.disconnect()
      // Redirect to login page
      this.$router.push({ name: 'auth-login' })
    },
  },
}
</script>
