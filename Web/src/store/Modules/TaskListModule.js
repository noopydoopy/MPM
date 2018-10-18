import taskServices from '@/api/TaskServices'
import userServices from '@/api/UserService'
import typeServices from '@/api/TypeService'
import priorityService from '@/api/PriorityService'

const type = {
    requestTasksData: 'TASKLIST_REQUEST_TASKS',
    requestUsersData: 'TASKLIST_REQUEST_USERS',
    requestTypesData: 'TASKLIST_REQUEST_TYPES',
    requestPrioritiesData: 'TASKLIST_REQUEST_PRORITIES',
}
const state = {
    taskLists: [],
    users: [],
    types: [],
    priorities: []
}
const getters = {
    taskLists(state) {
        return state.taskLists
    }
}
const actions = {
    async requestTaskLists({ commit }, projectId) {
        let tasks = await taskServices.loadTasks(projectId)
        commit(type.requestTasksData, tasks.data)
    },
    async requestUsers({ commit }) {
        let users = await userServices.getAllUser()
        commit(type.requestUsersData, users.data)
    },
    async requestTypes({ commit }) {
        let types = await typeServices.getAll()
        commit(type.requestTypesData, types.data)
    },
    async requestPriorities({ commit }) {
        let priorities = await priorityService.loadPriorityItem()
        commit(type.requestPrioritiesData, priorities.data)
    }
}
const mutations = {
    [type.requestTasksData](state, items) {
        state.taskLists = items
    },
    [type.requestUsersData](state, items) {
        state.users = items
    },
    [type.requestTypesData](state, items) {
        state.types = items
    },
    [type.requestPrioritiesData](state, items) {
        state.priorities = items
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}