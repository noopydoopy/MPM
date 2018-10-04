import axios from 'axios'

export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    getUserByCode(code) {
        var url = this.data().apiHost + "/api/Users/code/" + code;
        return axios.get(url);
    },
    updateUser(user) {
        var url = this.data().apiHost + "/api/Users/" + user.userId;
        return axios.put(url, user);
    },
    logIn(authen) {
        console.log(authen)
        var url = this.data().apiHost + "/api/Users/authentication"
        return axios.post(url, authen);
    },
    getUserResetPasswordByCode(code) {
        var url = this.data().apiHost + "/api/Users/resetPasswordCode/" + code;
        return axios.get(url);
    },
    getUserByUserId(id) {
        var url = this.data().apiHost + "/api/Users/code/" + code;
        return axios.get(url);
    },
    updateUser(user) {
        var url = this.data().apiHost + "/api/Users/" + user.userId;
        return axios.put(url, user);
    },
    loadUserNotInProject(proId)
    {
        const url = this.data().apiHost  +'/api/Users/GetUserNotInProjectId/'+proId;;
        return axios.get(url);
    },
    loadUserInProject(proId)
    {
        const url = this.data().apiHost  +'/api/Users/GetUserInProjectId/'+proId;;
        return axios.get(url);
    },
}