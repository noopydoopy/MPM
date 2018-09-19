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
                    <b-button variant="danger sm" class="btn-delete mr-1" size="sm" v-b-modal="`modal${row.item.typeId}`">Remove</b-button>
                    <b-modal :id="`modal${row.item.typeId}`"
                        title="Confirm Remove"
                        @ok="$emit('deleteType', row.item)">
                            Do you sure to Remove Type "{{row.item.name}}" ?
                    </b-modal>
                </template>
            </b-table>
        </template>
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
            ]
        }
     }
}
</script>


