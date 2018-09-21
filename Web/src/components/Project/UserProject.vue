<template>
    <div>
        <b-container fluid>
             <b-row class="justify-content-md-center w-100">
                 <b-col cols="5">
                     <user-project-list id="userNotProject" v-bind:UserType="UserTypeNot"></user-project-list>
                 </b-col>
                 <b-col cols="1">
                 </b-col>
                 <b-col cols="5">
                     <user-project-list id="userInProject" v-bind:UserType="UserTypeIn"></user-project-list>
                 </b-col>
             </b-row>
         </b-container>
    </div>
</template>
<script>
import UserProjectList from '@/components/Project/UserList'
export default {
    Name:'user-project-control',
    props: ['Alluser','UserNotProject','UserInProject'],
     data: function () {
        return {
            AllUserCount:null,
            UserProjectCount:null,
            AlluserList:[],
            UserNotProjectList:[],
            UserInProjectList:[],
            UserTypeNot:"NotIn",
            UserTypeIn:"In",
        };
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

</style>