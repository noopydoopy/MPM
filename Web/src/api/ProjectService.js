import axios from 'axios';

export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    loadProject(proId)
    {
        const url = this.data().apiHost +'/api/Projects/GetProjectManage/'+proId;
        return axios.get(url);
    },
    updateProject(projectId,name,isActive)
    {
        const url = this.data().apiHost +'/api/Projects/'+projectId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            }
         }
        const requestBody = {
            ProjectId:projectId,
            Name: name,
            IsActive: isActive
        }
        return axios.put(url,requestBody,config);
    }
}