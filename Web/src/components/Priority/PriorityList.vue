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
                    <b-button variant="danger sm" class="btn-delete mr-1" size="sm" @click="ShowCofirmBoxDialog(row.item)" v-b-modal.modalRef >Remove</b-button>
                    
                </template>
            </b-table>

          
        </template>
          <b-modal 
          id="modalRef"
                    ref="modalRef"
                    title="Confirm Remove"                
                    @ok="EmitDelet">
                    {{this.MsgDelete}}             
            </b-modal>
    </div>
</template>
<script>
export default {

    Name:'priority-list',
    props: ['PriorityListItem','ShowEdit'],
    data: function () {
        return {
            TableHeader: [ 'priorityId','priorityNumber', 'color','action' ],
            RemoveItem : null,
        };
    },
    methods:
    {
        ShowCofirmBoxDialog:function(priority)
        {           
            this.RemoveItem =priority;
        },
        EmitDelet:function()
        {
            this.$emit('DeletePriorityItem', this.RemoveItem);
        }
    },
    computed:
    {
        MsgDelete()
        {
            if(this.RemoveItem!=null)
                return "Do you sure to Remove Priority "+this.RemoveItem.priorityNumber+" ?";
            else
                return "";
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

