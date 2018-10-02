import priorityService from '@/api/PriorityService'

const type = {
    requestPriorityListItem: 'PRIORITY_REQUEST_PRIORITYITEMS',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}

const state = {
    priorityListItem:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,

}
const getters = {
    priorityItem(state, getters) {
        return state.priorityListItem;
    },
    showEdit(state, getters) {
        return state.showEditAddForm;
    },
    saveResultMsg(state, getters) {
        return state.saveResultMsg;
    },
    showSaveAlert(state, getters) {
        return state.showResultMsg;
    },
}

const actions = {

    async requestPriorityListItem({state, commit}) {
        let priorities = await priorityService.loadPriorityItem()
        commit(type.requestPriorityListItem,priorities.data )
    },
    async requestAddPriority({state,commit,dispatch},newPriority )
    {
        let result = await priorityService.AddNewPriority(newPriority.priorityNumber,newPriority.color);
        await dispatch('setApiResult',result)
    },
    async requestUpdatePriority({state,commit,dispatch},updatePriority)
    {
        let result = await priorityService.UpdatePriority(updatePriority.priorityId,updatePriority.priorityNumber,updatePriority.color);
        await dispatch('setApiResult',result)
    },
    async requestDeletePriority({state,commit,dispatch},priorityId)
    {
        let result = await priorityService.DeletePriorityData(priorityId)
        await dispatch('setApiResult',result)
    },
    async showEditForm({state, commit}, show)
    {
         commit(type.showEdit,show )
    },
    async showErrorMessage({state, commit}, error)
    {
         commit(type.showResultMsg,error.show )
         commit(type.saveResultMsg,error.message )
    },
    async setApiResult({state,commit,dispatch},result)
    {
        if(result.status == 201 || result.status == 200)
        {
            await dispatch('requestPriorityListItem')
            await dispatch('showEditForm',false )
            await dispatch('showErrorMessage',{show:false,message:""})
        }
        else
        {
            let errorMsg={show:true,
            message:"api error : "+result.status+" : "+result.statusText}

            await dispatch('showErrorMessage',errorMsg)
        }
    }
    
}

const mutations = {
    [type.requestPriorityListItem](state, items) {
        state.priorityListItem = items
    },
    [type.showEdit](state, show) {
        state.showEditAddForm = show
    },
    [type.saveResultMsg](state, msg) {
        state.saveResultMsg = msg
    },
    [type.showResultMsg](state, show) {
        state.showResultMsg = show
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}