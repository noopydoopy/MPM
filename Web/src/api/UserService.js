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
    logIn(user) {
        var url = this.data().apiHost + "/api/Users/authentication"
        return axios.post(url, user);
    },
    logOut() {
        localStorage.removeItem('token');
    },
    authHeader() {
        let token = localStorage.getItem('token');
    
        if (token) {
            return { 'Authorization': 'Bearer ' + token };
        } else {
            return {};
        }
    }
}