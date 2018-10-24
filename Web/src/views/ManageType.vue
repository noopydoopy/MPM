<template>
    <div class="bg-light" >
    <nav-layout>
      <h1> Manange Type </h1>
      <div class="container mt-4 border-top border-bottom border-secondary" id="Edit Zone" v-if="showEdit">
          <div class="row justify-content-md-center">
                  <div class="col col-md-6">
                  <b-alert variant="danger"
                      dismissible
                      :show="showSaveAlert"
                      @dismissed="showSaveAlert=false">
                      {{ saveResultMsg }}
                  </b-alert>
                  </div>
            </div>
                <div class="form-group">
                    <div class="row justify-content-md-center mt-2">
                        <div class="col col-lg-2">    
                            <label class="float-left font-weight-bold" v-if="isNew">Add New Type</label>  
                            <label class="float-left" v-else>Edit Type</label>                 
                        </div>
                    </div>
                   <div class="row justify-content-md-center mt-2">
                        <div class="col col-md-2">	
					        <label class="float-right">Type name: </label>
                        </div>
					        <div class="col col-md-4 ">
						      <input v-model="form.typename" type="text" class="form-control">
                        </div>
					        </div>
                  <div class="row mt-2 float-center">
                      <div class="col">
                          <b-button variant="success" class="m-1" v-on:click="saveType">Save</b-button>
                          <b-button variant="danger" class="m-1" v-on:click="closedType">Closed</b-button>
                      </div>
                  </div>
                </div>  
      </div>
      <div class="container">
          <div class="row col col-md-2 col-md-push-10 float-right">
              <b-button variant="success" class="ml-2 mb-1 mt-5" v-show="!showEdit" v-on:click="addType">Add new</b-button>
          </div>
          <div class="row col justify-content-md-center ">
              <TypeList v-bind:typeListItems="typeListItems" v-bind:showEdit="showEdit" @editType="editType" @deleteType="deleteType" style="width:95%"></TypeList>
          </div>               
      </div>
    </nav-layout>
  </div>
</template>

<script>
import TypeList from '@/components/Type/TypeList'
import axios from 'axios';
import { mapGetters } from 'vuex'
export default {
    components:{
      TypeList
    },
    mounted() {
      this.$emit('page-change', this.$route.name)
      if(!this.userIsAdmin)
        {
            this.$router.push({ path: "/forbidden" });
        }
      this.loadAllTypeList();
    },
    data()
    {
      return {
            showEdit: false,
            isNew: true,
            showSaveAlert : false,
            saveResultMsg: '',
            form:{ typeId: null, typename: '' },
            typeListItems : [],
            apiHost: 'https://localhost:44382'
      }
    },
     computed: 
    {
        ...mapGetters({
            userIsAdmin:'authenticationModule/userIsAdmin',
        })
    },
    methods:{
      loadAllTypeList(){
            let url = this.apiHost +'/api/types';
            var vm = this;
             axios.get(url)
              .then(function (response) {
                  vm.typeListItems = response.data;
              });
      },
      addNewType: function(type)
      {
          const url = this.apiHost +'/api/types';
          const config = {
          headers: {              
              'Content-Type': 'application/json',
              }
            }
          const requestBody = {
              Name: type.typename
          }
          var vm = this;
          axios.post(url, requestBody, config).then(result => 
          {
              vm.closedType();
              vm.loadAllTypeList();
          }
          ).catch(e => {
              console.log(e);
              vm.setResultMsg(true, e);
          });        
      },
      updateType: function(type)
      {
          const url = this.apiHost +'/api/types/'+ type.typeId;
          const config = {
            headers: {              
              'Content-Type': 'application/json',
            }
          }
          const requestBody = {
              TypeId: type.typeId,
              Name: type.typename
          }
          var vm = this;
          axios.put(url, requestBody, config).then(result => 
          {
              vm.closedType();
              vm.loadAllTypeList();
          }
          ).catch(e => {
                  vm.setResultMsg(true,'Error: ' + e);
          });        
      },
      deleteTypeData(typeId)
      {
          const url = this.apiHost +'/api/Types/' + typeId;

          var vm = this;
          axios.delete(url).then(result => 
          {
            vm.loadAllTypeList();
          }
          ).catch(e => {
              vm.setResultMsg(true,'Error: ' + e);
          });  
      },
      addType()
      {
          this.form.typeId = null;
          this.form.typename = null;
          this.showEdit = true;   
          this.isNew = true;         
      },
      deleteType(type)
      {
        if(type != null)
        {
          this.deleteTypeData(type.typeId);
        }
      },
      editType(type)
      {
          if(type != null)
          {
              this.form.typeId = type.typeId;
              this.form.typename = type.name;
              this.showEdit = true;   
              this.isNew = false;  
          }     
      },
      closedType()
      {
          this.showEdit = false;
          this.form.typeId = null;
          this.form.typename = null;
          this.setResultMsg(false,'');
      },
      saveType()
      {
          if(this.form.typename != null && this.form.typename !=='')
          {
              if(this.isNew)
                  this.addNewType(this.form);
              else
                  this.updateType(this.form);
          }
          else
          {
              this.setResultMsg(true,'Type name is empty!')
          }
      },
      setResultMsg : function(show,msg)
      {
        this.showSaveAlert = show;
        this.saveResultMsg = msg;
      }
    }
}
</script>
