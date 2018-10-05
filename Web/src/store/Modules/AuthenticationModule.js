import subtractMinutes from "date-fns/sub_minutes";
import isAfter from "date-fns/is_after";
import userService from "@/api/UserService";

const type = {
    updateAuthenticationStore: 'UPDATE_AUTHENTICATION_STORE'
}

const state = {
    header: null,
    user: null
}
const getters = {
    header(state, getters) {
        return state.header;
    },
    user(state, getters) {
        return state.user;
    }
}

const actions = {
    async setAuthenticationStore({ state, commit }) {
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
                            localStorage.setItem("token", JSON.stringify(response.data))
                            if (rememberMe) {
                                var rememberMe = {
                                    refreshToken: response.data.refreshToken,
                                    refreshTokenExpire: response.data.refreshTokenExpire
                                };
                                localStorage.setItem("rememberMe", JSON.stringify(rememberMe));
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

}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}