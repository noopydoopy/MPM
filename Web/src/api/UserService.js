import axios from 'axios'

export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    getUserByCode(code) {
        var url = this.data().apiHost + "/api/Users/code/" + code;
        return axios.get(url);
    },
    activeUser(user) {
        var url = this.data().apiHost + "/api/Users/" + user.userId;
        return axios.put(url, user);
    },
    logIn(authen) {
        var url = this.data().apiHost + "/api/Users/authentication"
        return axios.post(url, authen);
    },
    getUserByUserId(id) {
        var url = this.data().apiHost + "/api/Users/" + id;
        return axios.get(url);
    },
    updateUser(user) {
        var url = this.data().apiHost + "/api/Users/" + user.userId;
        return axios.put(url, user);
    },
}