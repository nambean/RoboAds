<template>
  <div class="misc-wrapper">

    <div class="misc-inner p-2 p-sm-3">
      <div class="w-100 text-center">
        <h2 class="mb-1">
          Chưa xác thực! 🔐
        </h2>
        <p class="mb-2">
          Bạn không có quyền thực hiện thao tác này. Vui lòng về trang chủ !!
        </p>
        <b-button
          variant="primary"
          class="mb-1 btn-sm-block"
          @click="loginRoute()"
        >Về trang chủ</b-button>
        <b-img
          fluid
          :src="imgUrl"
          alt="Not authorized page"
        />
      </div>
    </div>
  </div>
</template>

<script>
/* eslint-disable global-require */
import { BImg, BButton } from 'bootstrap-vue'
import store from '@/store/index'

export default {
  components: {
    BImg, BButton,
  },
  data() {
    return {
      downImg: require('@/assets/images/pages/not-authorized.svg'),
    }
  },
  computed: {
    imgUrl() {
      if (store.state.appConfig.layout.skin === 'dark') {
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.downImg = require('@/assets/images/pages/not-authorized-dark.svg')
        return this.downImg
      }
      return this.downImg
    },
  },
  methods: {
    loginRoute() {
      this.$router.push(localStorage.getItem('homeUrl'))
    },
  },
}
</script>

<style lang="scss">
@import 'src/@core/scss/vue/pages/page-misc.scss';
</style>
