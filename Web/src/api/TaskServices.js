import axios from 'axios';

export default {
    data() {
        return { apiHost: "https://localhost:44382" }
    },
    loadTasks(projectid) {        
        const url = this.data().apiHost + "/api/Tasks/GetByProjectID/" + projectid;
        return axios.get(url);
    },

    updateProject(projectId, name, isActive) {
        const url = this.data().apiHost + '/api/Tasks/' + projectId;
        const config = {
            headers: {
                'Content-Type': 'application/json',
            }
        }
        const requestBody = {
            ProjectId: projectId,
            Name: name,
            IsActive: isActive
        }
        return axios.put(url, requestBody, config);
    }
}