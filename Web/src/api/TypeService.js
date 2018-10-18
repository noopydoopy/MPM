import axios from 'axios';

export default {
    data() {
        return { apiHost: "https://localhost:44382" }
    },
    getAll() {        
        const url = this.data().apiHost + "/api/Types/";
        return axios.get(url);
    },
}