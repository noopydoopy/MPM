<template>
    <div>
        <b-container fluid>
            <b-row>
                <b-col>
                    {{this.UserCaption}}
                </b-col>
            </b-row>
            <b-row>
                <b-col md="12" class="my-1">
                    <b-form-group horizontal label="Filter " class="mb-0">
                        <b-input-group>
                            <b-form-input v-model="this.filter" placeholder="Type to Search" />
                            <b-input-group-append>
                                <b-btn :disabled="!this.filter" @click="filter = ''">Clear</b-btn>
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
                        :filter="this.filter"

                        @filtered="this.onFiltered">

                </b-table>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>
<script>
export default {
    Name:'user-project-list',
    props: ['UserItem','UserType'],
     data: function () {
        return {
            fields: [
                { key: 'name', label: 'User name', sortable: true, sortDirection: 'desc' },
                { key: 'email', label: 'Email', sortable: true,},
                { key: 'isActive', label: 'is Active' },
                { key: 'actions', label: 'Actions' }
                ],
            sortBy: null,
            sortDesc: false,
          
            sortDirection: 'asc',
            filter: null,
            //UserCaption : null,
        };
    },
     mounted() {

     },
    methods:
    {
        onFiltered (filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        }
    },
     computed:
    {
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
    },
}
</script>
<style scoped>

</style>


