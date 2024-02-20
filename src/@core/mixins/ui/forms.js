// We haven't added icon's computed property because it makes this mixin coupled with UI
export const togglePasswordVisibility = {
  data() {
    return {
      passwordFieldType: 'password',
      confirmPasswordFieldType: 'password',
      pinFieldType: 'password',
      confirmPinFieldType: 'password',
    }
  },
  methods: {
    togglePasswordVisibility() {
      this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password'
    },
    toggleConfirmPasswordVisibility() {
      this.confirmPasswordFieldType = this.confirmPasswordFieldType === 'password' ? 'text' : 'password'
    },
    togglePinVisibility() {
      this.pinFieldType = this.pinFieldType === 'password' ? 'text' : 'password'
    },
    toggleConfirmPinVisibility() {
      this.confirmPinFieldType = this.confirmPinFieldType === 'password' ? 'text' : 'password'
    },
  },
}

export const _ = null
