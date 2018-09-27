<template>
    <div>
        <nav-layout>
            <h1>Manage Project</h1>
            <div class="container mt-5">
                <div class="b-form-group">
                <div class="row justify-content-md-center">
                    <div class="col col-md-2 mt-2">
                        <label class="float-right">Project Name : </label>
                    </div>
                    <div class="col col-md-2">
                        <input v-model="ProjectName" class="form-control" style="width: 300px;">
                    </div>
                    <div class="col col-md-2">
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col col-md-2 mt-2">
                        <label class="float-right">Is active : </label>
                    </div>
                    <div class="col col-md-2 ">
                         <b-form-checkbox id="chkIsActive"
                            class="float-left mt-2"
                            v-model="IsActive"
                            value=true
                            unchecked-value=false>                            
                        </b-form-checkbox>
                    </div>
                    <div class="col col-md-2">
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col col-md-2">
                        <label class="float-Right">Total task in project :</label>
                    </div>
                    <div class="col col-md-4">
                        <label class="float-left">{{this.TaskCount}}</label>
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col col-md-2">
                         <label class="float-Right">Active task in project :</label>
                    </div>
                    <div class="col col-md-4">
                        <label class="float-left">{{this.TaskActive}}</label>
                    </div>
                </div>
                </div>
            </div>
            <div>
                 <b-container fluid class="w-100 mt-5">
                    <b-row class="w-100">
                        <user-project-control class="w-100" id="userProjectControl" 
                        v-bind:ProjectId="this.ProjectId" 
                        v-bind:UserNotProject="this.UserNotProjectList"
                        v-bind:UserInProject="this.UserInProjectList"></user-project-control>
                    </b-row>
                 </b-container>
            </div>

        </nav-layout>
    </div>
</template>
<script>
import userProjectControl from '@/components/Project/UserProject'
import axios from 'axios';
export default {
    name: 'ManageProject',
 mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)

  },
   components: {
        userProjectControl,
        axios,
    },
  data: function () {
      return {
        ProjectId : null,
        ProjectName : null,
        IsActive : true,
        TaskCount:null,
        TaskActive:null,
        ApiHost: 'https://localhost:44382',
        UserNotProjectList:[],
        UserInProjectList:[],
        ProjectManageData:[],

      };
  },
  mounted() {
      if(this.$route.params.projectId != null && this.$route.params.projectId >0)
      {
          this.ProjectId = this.$route.params.projectId;
          this.InitData(this.ProjectId);
         
      }
     },
  methods:
  {
      InitData(proId)
      {
          this.LoadProject(proId);
          this.LoadUserNotInProject(proId);
          this.LoadUserInProject(proId);
      },
      LoadProject(proId)
        {
            const url = this.ApiHost +'/api/Projects/GetProjectManage/'+proId;;
            var vm = this;
             axios.get(url)
                .then(function (response) {
                    vm.ProjectManageData = response.data;
                    vm.ProjectName = vm.ProjectManageData.projectName;
                    vm.IsActive = vm.ProjectManageData.projectIsActive;
                    vm.TaskCount = vm.ProjectManageData.taskCount;
                    vm.TaskActive = vm.ProjectManageData.taskActiveCount;
                });
        },
        LoadUserNotInProject(proId)
        {
            const url = this.ApiHost +'/api/Users/GetUserNotInProjectId/'+proId;;
            var vm = this;
             axios.get(url)
                .then(function (response) {
                    vm.UserNotProjectList = response.data;
                    
                });
        },
        LoadUserInProject(proId)
        {
            const url = this.ApiHost +'/api/Users/GetUserInProjectId/'+proId;;
            var vm = this;
             axios.get(url)
                .then(function (response) {
                    vm.UserInProjectList = response.data;
                });
        },
  },
  computed:
    {
       
    },

}
</script>