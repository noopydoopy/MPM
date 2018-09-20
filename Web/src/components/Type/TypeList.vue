<template>
    <div>
        <template>
             <b-table ref="myTable" :items="typeListItems" :fields="fileds">
                 <template slot="name" slot-scope="data" >                   
                     <div> 
                        <span>                    
                            {{data.value}}     
                        </span>
                    </div>
                </template>
                 
                <template slot="action" slot-scope="row">
                    <b-button v-show="!showEdit" variant="success sm" class="btn-edit mr-1" size="sm" @click.stop="$emit('editType', row.item)">Edit</b-button>
                    <b-button variant="danger sm" class="btn-delete mr-1" size="sm" v-b-modal.typeModal @click="OpenTypeModal(row.item)">Remove</b-button>
                </template>
            </b-table>
        </template>
        <b-modal id="typeModal"
            title="Confirm Remove"
            @ok="confirmDelete">
                {{ deleteMessage }}
        </b-modal>
    </div>
</template>

<script>
export default {
     props: ['typeListItems', 'showEdit'],
     data() {
        return {
            fileds:[
                {  key: 'name',   label: 'Type Name' },
                {  key: 'action', lable: ' Action' }
            ],
            deleteType: null,
            deleteMessage: ''
        }
     },
     methods:{
         OpenTypeModal(type){
            this.deleteType = type;
            this.deleteMessage = "Do you sure to Remove Type " + this.deleteType.name
         },
         confirmDelete(){
             if(this.deleteType != null)
                this.$emit('deleteType', this.deleteType)
         }
     }
}
</script>


