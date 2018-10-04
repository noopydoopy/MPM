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
                            @dismissed="ShowEditForm(false)">
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
                    <b-button variant="success" class="ml-2 mb-1 mt-5 btn-sm" v-show="!ShowEdit" v-on:click="AddPriority">Add new</b-button>
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

import { mapGetters } from 'vuex'

export default {
    name: 'ManagePriority',
    components: {
        PriorityList,
        ColorPickerChrome,
    },

    mounted() {
        this.$emit('page-change', this.$route.name)
        if(!this.hasPermission)
        {
            this.$router.push({ path: "/forbidden" });
        }
        this.loadPriority();
        
    },
    updated() {
        
    },
    data: function () {
        return {
            PriorityId: null,
            PriorityNumber: null,
            Color:null,
            IsNew : false,
        };
    },  
    computed: 
    {
        ...mapGetters({
            PriorityListItem: 'managePriorityModule/priorityItem',
            ShowEdit: 'managePriorityModule/showEdit',
            ShowSaveAlert: 'managePriorityModule/showSaveAlert',
            SaveResultMsg: 'managePriorityModule/saveResultMsg',
            tokenHeader:'authenticationModule/header',
            userLogin:'authenticationModule/user',
            }),

        hasPermission()
        {
            if(this.userLogin)
            {
                if(this.userLogin.isAdmin)
                {
                    return true;
                }
            }
            else return false;
        }
    },
    
    methods:
    {
        
        loadPriority()
        {
            this.$store.dispatch('managePriorityModule/requestPriorityListItem')
        },
        ShowEditForm(show)
        {
            this.$store.dispatch('managePriorityModule/showEditForm',show)
        },
        AddNewPriority: function(pNumber,pColor)
        {
            this.$store.dispatch('managePriorityModule/requestAddPriority',{
                    priorityNumber:pNumber
                    ,color :pColor           
            })
            
        },
        UpdatePriority: function(pId,pNumber,pColor)
        {
               this.$store.dispatch('managePriorityModule/requestUpdatePriority',{
                    priorityId: pId
                    ,priorityNumber: pNumber
                    ,color :pColor           
            })     
        },
        DeletePriorityData(pId)
        {
                this.$store.dispatch('managePriorityModule/requestDeletePriority',pId);
                
        },
        AddPriority()
        {
            this.PriorityId = null;
            this.PriorityNumber = null;
            this.Color = null;           
            this.IsNew = true;    
            
            this.ShowEditForm(true);
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
                this.ShowEditForm(true); 
                this.IsNew = false;  
            }     
        },
        ClosedPriority()
        {
            this.ShowEditForm(false);
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
            this.$store.dispatch('managePriorityModule/showErrorMessage',{show:show,message:msg})
        },
    
    }

}
</script>
<style scoped>

</style>

