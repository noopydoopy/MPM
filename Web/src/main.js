import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import BootstrapVue from 'bootstrap-vue'

import WideLayout from '@/layout/WideLayout'
import NavLayout from '@/layout/NavLayout'

Vue.use(BootstrapVue)
Vue.use(require('vue-moment'));

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.component('wide-layout', WideLayout)
Vue.component('nav-layout', NavLayout)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
