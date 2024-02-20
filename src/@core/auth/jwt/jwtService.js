import jwtDefaultConfig from './jwtDefaultConfig'

export default class JwtService {
  // Will be used by this service for making API calls
  axiosIns = null

  // jwtConfig <= Will be used by this service
  jwtConfig = { ...jwtDefaultConfig }

  // For Refreshing Token
  isAlreadyFetchingAccessToken = false

  // For Refreshing Token
  subscribers = []

  $loading = null

  constructor(axiosIns, jwtOverrideConfig) {
    this.axiosIns = axiosIns
    this.jwtConfig = { ...this.jwtConfig, ...jwtOverrideConfig }

    const that = this
    // Request Interceptor
    this.axiosIns.interceptors.request.use(
      config => {
        that.$loading.show({ loader: 'dots', color: '#1b74e4' })

        // Get token from localStorage
        const accessToken = this.getToken()

        // If token is present add it to request's Authorization Header
        if (accessToken) {
          // eslint-disable-next-line no-param-reassign
          config.headers.Authorization = `${this.jwtConfig.tokenType} ${accessToken}`
        }
        return config
      },
      error => Promise.reject(error),
    )

    // Add request/response interceptor
    this.axiosIns.interceptors.response.use(
      response => {
        setTimeout(() => {
          const paras = document.getElementsByClassName('vld-overlay')
          while (paras[0]) {
            paras[0].parentNode.removeChild(paras[0])
          }
        }, 300)
        return response
      },
      error => {
        const paras = document.getElementsByClassName('vld-overlay')
        while (paras[0]) {
          paras[0].parentNode.removeChild(paras[0])
        }

        const { response } = error

        // if (status === 401) {
        if (response && response.status === 401) {
          localStorage.removeItem('accessToken')
          localStorage.removeItem('userData')
          localStorage.removeItem('menus')
          localStorage.removeItem('homeUrl')
          localStorage.setItem('sessionExpired', 'Session is expired.')
          // Redirect to login page
          return window.location.reload()
        }

        if (response && response.status === 403) {
          window.location = '/not-authorized'
        }

        return Promise.reject(error)
      },
    )
  }

  onAccessTokenFetched(accessToken) {
    this.subscribers = this.subscribers.filter(callback => callback(accessToken))
  }

  addSubscriber(callback) {
    this.subscribers.push(callback)
  }

  getToken() {
    return localStorage.getItem(this.jwtConfig.storageTokenKeyName)
  }

  getRefreshToken() {
    return localStorage.getItem(this.jwtConfig.storageRefreshTokenKeyName)
  }

  setToken(value) {
    localStorage.setItem(this.jwtConfig.storageTokenKeyName, value)
  }

  setRefreshToken(value) {
    localStorage.setItem(this.jwtConfig.storageRefreshTokenKeyName, value)
  }

  login(...args) {
    return this.axiosIns.post(this.jwtConfig.loginEndpoint, ...args)
  }

  register(...args) {
    return this.axiosIns.post(this.jwtConfig.registerEndpoint, ...args)
  }

  refreshToken() {
    return this.axiosIns.post(this.jwtConfig.refreshEndpoint, {
      refreshToken: this.getRefreshToken(),
    })
  }

  setLoading(loading) {
    this.$loading = loading
  }
}
