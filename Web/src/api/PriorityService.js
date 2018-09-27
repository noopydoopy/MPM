import axios from 'axios';
import userService from './UserService';


export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    loadPriorityItem()
    {
        const url = this.data().apiHost+'/api/Priorities';
        return axios.get(url)
    },
}