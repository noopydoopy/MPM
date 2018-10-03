<template>
    <div>
        <b-container fluid>
             <b-row class="justify-content-md-center w-100">
                 <b-col cols="5" class="userlist">

                     <user-project-list id="userNotProject" 
                     v-bind:UserType="UserTypeNot"
                     v-bind:UserItem="this.UserNotProject"></user-project-list>

                 </b-col>
                 <b-col cols="1">
                 </b-col>
                 <b-col cols="5" class="userlist">
                     <user-project-list id="userInProject" 
                     v-bind:UserType="UserTypeIn"
                     v-bind:UserItem="this.UserInProject"></user-project-list>
                 </b-col>
             </b-row>
         </b-container>
    </div>
</template>
<script>
import UserProjectList from '@/components/Project/UserList'
export default {
    Name:'user-project-control',
    props: ['ProjectId','UserNotProject','UserInProject'],
     data: function () {
        return {
            AllUserCount:null,
            UserProjectCount:null,
            UserTypeNot:"NotIn",
            UserTypeIn:"In",
            
        };
    },
     mounted() {

     },
    components: {
        UserProjectList,
    },
    methods:
    {
    },
    computed:
    {
    },
    
}
</script>
<style scoped>
    .userlist
    {
        border-color: #f3f3f3;
        border-style: solid;
        border-radius: 15px;
    }
</style>