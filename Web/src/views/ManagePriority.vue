<template >
    <div class="bg-light" >
        <nav-layout>
            <h1>Manage Priority</h1>

            <div class="container mt-4 border-top border-bottom border-secondary" id="Edit Zone" v-if="ShowEdit">
                <div class="row justify-content-md-center">
                        <div class="col col-md-6">
                        <b-alert variant="danger"
                            dismissible
                            :show="ShowSaveAlert"
                            @dismissed="ShowSaveAlert=false">
                            {{SaveResultMsg}}
                        </b-alert>
                        </div>
                    </div>
                    
                <div class="form-group">
                    <div class="row justify-content-md-center mt-2">
                        <div class="col col-lg-2">    
                            <label class="float-left font-weight-bold" v-if="IsNew">Add New Priority</label>  
                            <label class="float-left" v-else>Edit Priority</label>                 
                        </div>
                    </div>
                    
                    <div class="row justify-content-md-center mt-2">
                        <div class="col col-md-2">	
					        <label class="float-right">Priority Number: </label>
                        </div>
					    <div class="col col-md-2 ">
						    <input v-model="PriorityNumber" type="number" class="form-control" style="width: 300px;">
                        </div>
                        <div class="col col-md-2 ">
                        </div>
					</div>
                    <div class="row justify-content-md-center mt-2">  
                            <div class="col col-md-2">
                                <label class="float-right">Color: </label>
                            </div>
                            <div class="col col-md-2">
                                <color-picker-chrome :color="Color" v-model="Color" />
					        </div>	
                            <div class="col col-md-2 ">
                            </div>			            
                    </div>
                   
                    <div class="row mt-2 float-center">
                        <div class="col">
                            <b-button variant="success" class="m-1" v-on:click="SavePriority">Save</b-button>
                            <b-button variant="danger" class="m-1" v-on:click="ClosedPriority">Closed</b-button>
                        </div>
                    </div>
                </div>  
            </div>
             <div class="container">
                <div class="row col col-md-2 col-md-push-10 float-right">
                    <b-button variant="success" class="ml-2 mb-1 mt-5" v-show="!ShowEdit" v-on:click="AddPriority">Add new</b-button>
                </div>
                <div class="row col justify-content-md-center ">
                    <priority-list v-bind:PriorityListItem="PriorityListItem" v-bind:ShowEdit="ShowEdit" @EditPriorityItem="EditPriority" @DeletePriorityItem="DeletePriority" style="width:95%"></priority-list>
                </div>               
            </div>
        </nav-layout>
    </div>
    
</template>
<script>
import PriorityList from '@/components/Priority/PriorityList'
import ColorPickerChrome from '@/components/Priority/ColorPickerChrome'
import axios from 'axios';

export default {
    name: 'ManagePriority',
    components: {
        PriorityList,
        ColorPickerChrome,
    },

    mounted() {
        this.$emit('page-change', this.$route.name)
        this.loadPriority();
    },
    data: function () {
        return {
            PriorityId: null,
            PriorityNumber: null,
            Color:null,
            PriorityListItem: [],
            ApiHost: 'https://localhost:44382',
            ShowEdit:false,
            IsNew : false,
            ShowSaveAlert : false,
            SaveResultMsg : null,
        };
    },    
    methods:
    {
        loadPriority()
        {
            const url = this.ApiHost +'/api/Priorities';
            var vm = this;
             axios.get(url)
                .then(function (response) {
                    vm.PriorityListItem = response.data;
                });
        },
        AddNewPriority: function(pNumber,pColor)
        {
            const url = this.ApiHost +'/api/Priorities';
            const config = {
            headers: {              
                'Content-Type': 'application/json',
                }
             }
             var querystring = require('querystring');
            const requestBody = {
                PriorityNumber: pNumber,
                Color: pColor
            }
            var vm = this;
            axios.post(url,querystring.stringify(requestBody),config).then(result => 
                    {
                        vm.ClosedPriority();
                        vm.loadPriority();
                    }
            ).catch(e => {
                    vm.SetResultMsg(true,'Error: ' + e);
                });        
        },
        UpdatePriority: function(pId,pNumber,pColor)
        {
            const url = this.ApiHost +'/api/Priorities/'+pId;
            const config = {
            headers: {              
                'Content-Type': 'application/json',
                }
             }
             var querystring = require('querystring');
            const requestBody = {
                PriorityId:pId,
                PriorityNumber: pNumber,
                Color: pColor
            }
            var vm = this;
            axios.put(url,querystring.stringify(requestBody),config).then(result => 
                    {
                        vm.ClosedPriority();
                        vm.loadPriority();
                    }
            ).catch(e => {
                    vm.SetResultMsg(true,'Error: ' + e);
                });        
        },
        DeletePriorityData(pId)
        {
             const url = this.ApiHost +'/api/Priorities/'+pId;
            
            var vm = this;
            axios.delete(url).then(result => 
                    {
                        vm.loadPriority();
                    }
            ).catch(e => {
                    vm.SetResultMsg(true,'Error: ' + e);
                });  
        },
        AddPriority()
        {
            this.PriorityId = null;
            this.PriorityNumber = null;
            this.Color = null;
            this.ShowEdit = true;   
            this.IsNew = true;         
        },
        DeletePriority(priority)
        {
             if(priority!=null)
            {
                this.DeletePriorityData(priority.priorityId);
            }
        },
        EditPriority(priority)
        {
            if(priority!=null)
            {
                this.PriorityId = priority.priorityId;
                this.PriorityNumber = priority.priorityNumber;
                this.Color = priority.color;
                this.ShowEdit = true;   
                this.IsNew = false;  
            }     
        },
        ClosedPriority()
        {
            this.ShowEdit = false;
            this.PriorityId = null;
            this.PriorityNumber = null;
            this.Color = null;
            this.SetResultMsg(false,'');
        },
        SavePriority()
        {
            if(this.PriorityNumber != null && this.PriorityNumber !=='')
            {
                if(this.PriorityNumber <= 0)
                {
                    this.SetResultMsg(true,'Priority Number must greater than 0')
                    
                }
                else
                {
                    if(this.IsNew)
                        this.AddNewPriority(this.PriorityNumber,this.Color);
                    else
                        this.UpdatePriority(this.PriorityId,this.PriorityNumber,this.Color);
                }
            }
            else
            {
                this.SetResultMsg(true,'Priority Number is empty!')
            }
        },
        SetResultMsg : function(show,msg)
        {
                this.ShowSaveAlert = show;
                this.SaveResultMsg = msg;
        },
       

    }

}
</script>
<style scoped>

</style>

