import axios from 'axios';

export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    loadProject(proId)
    {
        const url = this.data().apiHost +'/api/Projects/GetProjectManage/'+proId;
        return axios.get(url);
    }
}