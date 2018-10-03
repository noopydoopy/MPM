import projectService from '@/api/ProjectService'
import userService from '@/api/UserService'
import userProjectService from '@/api/UserProjectServices';
const type = {
    requestUserNotProjectList: 'USER_REQUEST_USER_NOT_IN_PROJECT',
    requestUserInProjectList: 'USER_REQUEST_USER_IN_PROJECT',
    requestProjectManageData: 'PROJECT_REQUEST_PROJECTDATA',
    addUserInProject: 'USER_ADD_TO_PROJECT',
    romoveUserInProject: 'USER_REMOVE_FROM_PROJECT',
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
    async requestAddUserToProject({state, commit},userId) {
        commit(type.addUserInProject,userId)
    },
    async requestRemoveUserFromProject({state, commit},userId) {
        commit(type.romoveUserInProject,userId)
    },
    async saveProjectDate({state, commit})
    {
        await projectService.updateProject(state.projectManageData.projectId,state.projectManageData.projectName,state.projectManageData.projectIsActive)
    },
    async saveUserProjectDate({state, commit})
    {
        let userList = []
        if(state.userInProjectList.length >0)
        {
            for (var i = 0; i < state.userInProjectList.length; i++)
            {
                userList.push(state.userInProjectList[i].userId);
            }
        }   
        await userProjectService.saveUserProject(state.projectManageData.projectId,userList);
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
    [type.addUserInProject](state, userId) {
        
        var userInProject = state.userInProjectList.find(u => u.userId === userId)    
        if(!userInProject)
        {
            var index = state.userNotProjectList.findIndex(u => u.userId === userId) 
            if(index>=0)
            {
                var userMove = state.userNotProjectList[index]          
                state.userInProjectList.push(userMove)
                state.userNotProjectList.splice(index,1) 
               // state.userInProjectList.sort(function(a, b) {
                //    return a.userId - b.userId;
                //  });            
            }
        }
    },
    [type.romoveUserInProject](state, userId) {
        
        var userNotInProject = state.userNotProjectList.find(u => u.userId === userId)    
        if(!userNotInProject)
        {
            var index = state.userInProjectList.findIndex(u => u.userId === userId) 
            if(index>=0)
            {
                var userMove = state.userInProjectList[index]          
                state.userNotProjectList.push(userMove)
                state.userInProjectList.splice(index,1) 
               // state.userNotProjectList.sort(function(a, b) {
              //      return a.userId - b.userId;
               //   });            
            }
        }
    },
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}