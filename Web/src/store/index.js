import Vue from 'vue'
import Vuex from 'vuex'

import managePriorityModule from '@/store/Modules/ManagePriorityModule'
import authenticationModule from '@/store/Modules/AuthenticationModule'

Vue.use(Vuex)

export default new Vuex.Store({

  modules: {
    managePriorityModule,
    authenticationModule
  }
})
