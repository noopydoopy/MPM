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
    AddNewPriority(pNumber,pColor)
    {   
        const url = this.data().apiHost +'/api/Priorities';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            }
         }
        const requestBody = {
            PriorityNumber: pNumber,
            Color: pColor
        }
        return axios.post(url,requestBody,config); 
    },
    UpdatePriority(pId,pNumber,pColor)
    {
        const url = this.data().apiHost +'/api/Priorities/'+pId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            }
         }
        const requestBody = {
            PriorityId:pId,
            PriorityNumber: pNumber,
            Color: pColor
        }
        return axios.put(url,requestBody,config);
    },
    DeletePriorityData(pId)
    {
        const url = this.data().apiHost+'/api/Priorities/'+pId;
        return axios.delete(url);

    },
    
}