import projectService from '@/api/ProjectService'
import userService from '@/api/UserService'
const type = {
    requestUserNotProjectList: 'USER_REQUEST_USER_NOT_IN_PROJECT',
    requestUserInProjectList: 'USER_REQUEST_USER_IN_PROJECT',
    requestProjectManageData: 'PROJECT_REQUEST_PROJECTDATA',
}
const state = {
    userNotProjectList:[],
    userInProjectList:[],
    projectManageData:[],
}
const getters = {
    userNotProjectList(state, getters) {
        return state.userNotProjectList;
    },
    userInProjectList(state, getters) {
        return state.userInProjectList;
    },
    projectManageData(state, getters) {
        return state.projectManageData;
    },
}
const actions = {
    async requestProjectList({state, commit},projectId) {
        let project = await projectService.loadProject(projectId)
        commit(type.requestProjectManageData,project.data )
    },
    async requestUserNotProjectList({state, commit},projectId) {
        let user = await userService.loadUserNotInProject(projectId)
        commit(type.requestUserNotProjectList,user.data )
    },
    async requestUserInProjectList({state, commit},projectId) {
        let user = await userService.loadUserInProject(projectId)
        commit(type.requestUserInProjectList,user.data )
    },
}
const mutations = {
    [type.requestUserNotProjectList](state, items) {
        state.userNotProjectList = items
    },
    [type.requestUserInProjectList](state, items) {
        state.userInProjectList = items
    },
    [type.requestProjectManageData](state, items) {
        state.projectManageData = items
    },
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}