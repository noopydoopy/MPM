<template>
    <div>
        <template>
             <b-table striped hover :items="PriorityListItem" :fields="TableHeader">
                <template slot="action" slot-scope="row">
                    <b-button class="btn-edit mr-1" size="sm" @click.stop="deletePriority(row.index)">Edit</b-button>
                    <b-button class="btn-delete mr-1" size="sm" @click.stop="deletePriority(row.index)">Remove</b-button>
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
