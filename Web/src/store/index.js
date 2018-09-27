import Vue from 'vue'
import Vuex from 'vuex'

import managePriorityModule from '@/store/Modules/ManagePriorityModule'

Vue.use(Vuex)

export default new Vuex.Store({

  modules: {
    managePriorityModule
  }
})
