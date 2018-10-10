import subtractMinutes from "date-fns/sub_minutes";
import isAfter from "date-fns/is_after";
import userService from "@/api/UserService";
import userProjectService from "@/api/UserProjectServices";

const type = {
    updateAuthenticationStore: 'UPDATE_AUTHENTICATION_STORE',
    getUserProjectofCurrentUser:'GET_USERPROJECT_CURRENTUSER'
}

const state = {
    header: null,
    user: null,
    projectOfUser: null
}
const getters = {
    header(state, getters) {
        return state.header;
    },
    user(state, getters) {
        return state.user;
    },
    userIsAdmin() {
        if (state.user) {
            if (state.user.isAdmin) {
                return true;
            }
        }
        else return false;
    },
    CanAccessProject:(state) => (projectId) => 
    {
        let result = false;
        if(state.user)
        {
            if(state.user.isAdmin)
            {
                result = true;
            }
            else
            {
                if(state.projectOfUser)
                {
                    if(state.projectOfUser.userId == state.user.userId )
                    {
                        var isProject = state.projectOfUser.projectIdList.find(p => p === projectId)  
                        result = isProject;
                    }
                }
            }
        }
      return result;
    }
}

const actions = {
    async getUserProject({ state, commit }) {
        if(state.user)
        {
            let userId = state.user.userId;
            let userLinkProjectModel = await userProjectService.getUserProjectByUserId(userId);
            commit(type.getUserProjectofCurrentUser, userLinkProjectModel.data)
        }
    },
    async setAuthenticationStore({ state, commit,dispatch }) {
        let token = localStorage.getItem("token");
        let rememberMe = localStorage.getItem("rememberMe");
        let refreshToken = null;
        let refreshTokenExpire = null;
        let now = new Date();

        if (token) {
            token = decodeURIComponent(atob(token).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            token = JSON.parse(token);
        }
        if (rememberMe) {
            rememberMe = decodeURIComponent(atob(rememberMe).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            rememberMe = JSON.parse(rememberMe);
        }

        if (token) {
            let tokenExpiryDate = new Date(token.accessTokenExpire)
            let tenMinutesBeforeExpiry = subtractMinutes(tokenExpiryDate, 10);

            if (isAfter(now, tokenExpiryDate)) {
                console.log("Token expired");
                if (rememberMe) {
                    refreshToken = rememberMe.refreshToken
                    refreshTokenExpire = rememberMe.refreshTokenExpire
                } else {
                    localStorage.removeItem("token");
                    commit(type.updateAuthenticationStore, null)
                }
            } else if (isAfter(now, tenMinutesBeforeExpiry)) {
                console.log("Token expired/will expire in the next 10 minutes");
                refreshToken = token.refreshToken
                refreshTokenExpire = token.refreshTokenExpire
            } else {
                commit(type.updateAuthenticationStore, token)
            }
        } else if (rememberMe) {
            refreshToken = rememberMe.refreshToken
            refreshTokenExpire = rememberMe.refreshTokenExpire
        } else {
            commit(type.updateAuthenticationStore, null)
        }

        if (refreshToken) {
            if (!isAfter(now, new Date(refreshTokenExpire))) {
                var user = {
                    grantType: "refresh_token",
                    RefreshToken: refreshToken
                }
                await userService.logIn(user).then(
                    response => {
                        if (response.data) {
                            localStorage.setItem("token", btoa(encodeURIComponent(response.data).replace(
                                /%([0-9A-F]{2})/g,
                                function toSolidBytes(match, p1) {
                                    return String.fromCharCode("0x" + p1);
                                }
                            )))
                            if (rememberMe) {
                                var rememberMe = {
                                    refreshToken: response.data.refreshToken,
                                    refreshTokenExpire: response.data.refreshTokenExpire
                                }
                                localStorage.setItem("rememberMe", btoa(encodeURIComponent(rememberMe).replace(
                                    /%([0-9A-F]{2})/g,
                                    function toSolidBytes(match, p1) {
                                        return String.fromCharCode("0x" + p1);
                                    }
                                )))
                            }
                        }
                        commit(type.updateAuthenticationStore, response.data)
                    },
                    error => {
                        localStorage.removeItem("token");
                        localStorage.removeItem("rememberMe");
                        commit(type.updateAuthenticationStore, null)
                    }
                );
            } else {
                localStorage.removeItem("token");
                localStorage.removeItem("rememberMe");
                commit(type.updateAuthenticationStore, null)
            }
        }
    }
    
}

const mutations = {
    [type.updateAuthenticationStore](state, token) {
        if (token != null) {
            state.header = 'Bearer ' + token.accessToken,
                state.user = token
        } else {
            state.header = null,
                state.user = null
        }
    },
    [type.getUserProjectofCurrentUser](state,userLinkProject)
    {
        state.projectOfUser = userLinkProject;
    }

}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}