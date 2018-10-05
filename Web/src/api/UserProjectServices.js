import axios from 'axios';
export default {
    data() {
        return {apiHost: "https://localhost:44382"}
    },
    saveUserProject(projectId,userList)
    {
        const url = this.data().apiHost +'/api/UserProjects/SaveUserProject';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            }
         }
        const requestBody = {
            ProjectId: projectId,
            UserIdList: userList
        }
        return axios.post(url,requestBody,config); 
    }
}