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
                        <label class="float-left">0</label>
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col col-md-2">
                         <label class="float-Right">Active task in project :</label>
                    </div>
                    <div class="col col-md-4">
                        <label class="float-left">0</label>
                    </div>
                </div>
                </div>
            </div>
            <div>
                 <b-container  class="w-100 mt-5">
                    <b-row class="w-100">
                        <user-project-control class="w-100" id="userProjectControl"></user-project-control>
                    </b-row>
                 </b-container>
            </div>

        </nav-layout>
    </div>
</template>
<script>
import userProjectControl from '@/components/Project/UserProject'
export default {
    name: 'ManageProject',
 mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)

  },
   components: {
        userProjectControl,
    },
  data: function () {
      return {
        ProjectName : null,
        IsActive : true,
      };
  },
  methods:
  {
     
  },

}
</script>