<template>
    <div>
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
        <b-container fluid>
            <b-row >
                <b-col >
                    <div>
                        <h4 class="caption"> {{this.UserCaption}}</h4>
                    </div>
                </b-col>
            </b-row>
            <b-row>
                <b-col md="12" class="my-1 float-left">
                    <b-form-group horizontal label="Filter " class="mb-0">
                        <b-input-group>
                            <b-form-input v-model="filterUser" placeholder="Type to Search" />
                            <b-input-group-append>
                                <b-btn :disabled="!this.filterUser" @click="this.filterUser = ''">Clear</b-btn>
                            </b-input-group-append>
                        </b-input-group>
                    </b-form-group>
                </b-col>
            </b-row>
            <b-row>
                <b-col md="12">
                <b-table show-empty
                         stacked="md"
                        :items="this.UserItem"
                        :fields="this.fields"
                        :filter="this.filterUser"

                        @filtered="this.onFiltered">

                    <template slot="actions" slot-scope="row">
                        <b-button  v-if="IsInProjectNode" variant="danger" size="sm" @click.stop="CallRemoveFromProject(row.item.userId)" class="">
                            <i  class="material-icons icon-center">arrow_back</i>   
                        </b-button>
                        <b-button  v-else size="sm" variant="success" @click.stop="CallMoveToProject(row.item.userId)" class="">
                             <i class="material-icons icon-center">arrow_forward</i>
                        </b-button>  
                    </template>
                </b-table>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>
<script>
import { mapActions } from 'vuex'
export default {
    
    Name:'user-project-list',
    props: ['UserItem','UserType'],
     data: function () {
        return {
            fields: [
                { key: 'userId', label: 'UserId', sortable: true, sortDirection: 'desc' ,'class': 'text-center' },
                { key: 'name', label: 'User name', sortable: true, sortDirection: 'desc' ,'class': 'text-left' },
                { key: 'email', label: 'Email', sortable: true,'class': 'text-left' },
                { key: 'isActive', label: 'is Active' },
                { key: 'actions', label: 'Actions' }
                ],
            sortBy: null,
            sortDesc: false,
          
            sortDirection: 'asc',
            filterUser: null,
            //UserCaption : null,
        };
    },
     mounted() {
         //console.log(this.UserItem);
     },
    methods:
    {
        ...mapActions(
            {
            moveToProject : 'manageProjectModule/requestAddUserToProject' ,
            removeFromProject : 'manageProjectModule/requestRemoveUserFromProject'
            }
        ),
        CallRemoveFromProject(userId)
        {
            this.removeFromProject(userId)
        },
    
        CallMoveToProject(userId)
        {
            this.moveToProject(userId)
        },
        onFiltered (filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            //  this.totalRows = filteredItems.length
        }
    },
     computed:
    {
        IsInProjectNode()
        {
            if(this.UserType != null)
            {
                if(this.UserType == "In")
                {
                    return true;
                }
            }
            return false;
        },
       UserCaption(){
           if(this.UserType != null)
            {
                if(this.UserType == "NotIn")
                {
                    return "User not in project";
                }
                    else if(this.UserType == "In")
                {
                    return "User in project";
                }
                else
                    return "";
            }
            else{
                return "";
            }
       }
    }
}
</script>
<style scoped>

    .icon-center
    {
        text-align: center;
        vertical-align: middle;
    }
    .caption
    {
        margin-bottom: 20px;
        margin-top: 30px;
    }

</style>


