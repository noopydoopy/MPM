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
                    <b-button v-show="!ShowEdit" variant="success sm" class="btn-edit mr-1" size="sm" @click.stop="$emit('EditPriorityItem', row.item)">Edit</b-button>
                    <b-button variant="danger sm" class="btn-delete mr-1" size="sm" @click="ShowCofirmBox" >Remove</b-button>
                    <b-modal id="confirmDeleteModal"
                        ref="modalRef"
                        title="Confirm Remove"
                        @ok="$emit('DeletePriorityItem', row.item)">
                            Do you sure to Remove Priority "{{row.item.priorityNumber}}" ?
                    </b-modal>
                </template>
            </b-table>
        </template>
    </div>
</template>
<script>
export default {

    Name:'priority-list',
    props: ['PriorityListItem','ShowEdit'],
    data: function () {
        return {
            TableHeader: [ 'priorityId','priorityNumber', 'color','action' ],
        };
    },
    methods:
    {
          ShowCofirmBox :function()
          {
                this.$refs.modalRef.show()
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

