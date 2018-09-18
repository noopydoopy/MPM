<template>
    <div>
        <template>
             <b-table striped hover :items="PriorityListItem" :fields="TableHeader">
                 <template slot="color" slot-scope="data" >                   
                     <div> 
                        <span>                    
                            {{data.value}}     
                        </span>
                        <span class="color-box" :style="'background-color: ' + data.value +' !important'">
                        </span>
                    </div>
                </template>
                 
                <template slot="action" slot-scope="row">
                    <b-button variant="success sm" class="btn-edit mr-1" size="sm" @click.stop="deletePriority(row.index)">Edit</b-button>
                    <b-button variant="danger sm" class="btn-delete mr-1" size="sm" @click.stop="deletePriority(row.index)">Remove</b-button>
                </template>
            </b-table>
        </template>
    </div>
</template>
<script>
export default {

    Name:'priority-list',
    props: ['PriorityListItem'],
    data: function () {
        return {
            TableHeader: [ 'priorityId','priorityNumber', 'color','action' ],
        };
    },
    methods:
    {
            deletePriority(index)
            {

            }
    }
}
</script>
<style scoped>
    .color-box{
        display: inline-block;
	    width: 16px;
	    height: 16px;
    }
</style>

