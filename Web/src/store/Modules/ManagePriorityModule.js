import priorityService from '@/api/PriorityService'

const type = {
    requestPriorityListItem: 'PRIORITY_REQUEST_PRIORITYITEMS',
}

const state = {
    priorityListItem:[]
}
const getters = {
    priorityItem(state, getters) {
        return state.priorityListItem;
    }
}

const actions = {

    async requestPriorityListItem({state, commit}) {
        let priorities = await priorityService.loadPriorityItem()
        commit(type.requestPriorityListItem,priorities )
    }
}

const mutations = {
    [type.requestPriorityListItem](state, items) {
        state.priorityListItem = items
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}