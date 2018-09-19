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
                    
                </div>
                </div>
            </div>

        </nav-layout>
    </div>
</template>
<script>
export default {
    name: 'ManageProject',
     mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)
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