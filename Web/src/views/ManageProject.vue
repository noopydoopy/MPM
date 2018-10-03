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
                        <input v-model="ProjectManageData.projectName" class="form-control" style="width: 300px;">
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
                            v-model="ProjectManageData.projectIsActive"
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
                        <label class="float-left">{{ProjectManageData.taskCount}}</label>
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col col-md-2">
                         <label class="float-Right">Active task in project :</label>
                    </div>
                    <div class="col col-md-4">
                        <label class="float-left">{{ProjectManageData.taskActiveCount}}</label>
                    </div>
                </div>
                <div class="row justify-content-md-center mt-2 float-center">
                    <div class="col col-md-2">                      
                    </div>
                    <div class="col col-md-4">
                        <b-button class="float-left mr-2" variant="success" @click="saveProject">Save</b-button>
                        <b-button class="float-left" variant="danger" @click="showModal" v-b-modal.modalRef>Cancel</b-button>
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
        <b-modal 
            id="modalRef"
            ref="modalRef"
            title= ""          
            @ok="rollback">
            {{this.modalDetail}}             
        </b-modal>
    </div>
    
</template>
<script>
import userProjectControl from '@/components/Project/UserProject'
import axios from 'axios';
import { mapGetters } from 'vuex'
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
        modalDetail:"",
        modalTitle:""
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
      saveProject()
      {
          this.$store.dispatch('manageProjectModule/saveProjectDate');          
      },
      showModal()
      {
            this.modalDetail ="Do you want to reset your changed data?";
            this.modalTitle = "Confirm to reset changed";
      },
      rollback()
      {
          this.InitData(this.ProjectId);
      },
      LoadProject(proId)
        {
            this.$store.dispatch('manageProjectModule/requestProjectList',proId);
        },
        LoadUserNotInProject(proId)
        {
            this.$store.dispatch('manageProjectModule/requestUserNotProjectList',proId);
        },
        LoadUserInProject(proId)
        {
             this.$store.dispatch('manageProjectModule/requestUserInProjectList',proId);
        },
  },
  computed:
    {
       ...mapGetters({
            UserNotProjectList: 'manageProjectModule/userNotProjectList',
            UserInProjectList: 'manageProjectModule/userInProjectList',
            ProjectManageData: 'manageProjectModule/projectManageData'
            })
    },

}
</script>