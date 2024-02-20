import Vue from 'vue'

// axios
import axios from 'axios'

const axiosIns = axios.create({
  // ================================
  baseURL: process.env.NODE_ENV === 'production' ? 'https://ads0806.com/api/' : 'http://localhost:3000/api/',
  timeout: 100000,
  headers: { 'Access-Control-Allow-Origin': true },
})

Vue.prototype.$http = axiosIns

export default axiosIns
