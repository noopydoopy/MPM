<template>
    <div>
        <nav-layout>
            <h1>Manage Priority</h1>
            <div class="container">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-4 col-md-offset-4">	
					        <label class="col-sm-4 control-label">Priority Number: </label>
                        </div>
					    <div class="col-sm-8">
						    <input v-model="PriorityNumber" type="number" style="width: 300px;">
                        </div>
					</div>
                    <div class="row">  
                            <div class="col-md-4 col-md-offset-4">
                                <label class="col-sm-4 control-label">Color: </label>
                            </div>
                            <div class="col-sm-8">
                                <color-picker-chrome :color="Color" v-model="Color" />
					        </div>				            
                    </div>
                </div>  
            </div>
            <div class="table-priority"> 
                <priority-list v-bind:PriorityListItem="PriorityListItem"></priority-list>
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
        console.log(this.$route.path)
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
        };
    },
    beforeMount(){
        
    },
    
    methods:
    {
        loadPriority()
        {
            const url = this.ApiHost +'/api/Priorities';
            var vm = this;
             axios.get(url)
                .then(function (response) {
                    console.log(response.data);
                    vm.PriorityListItem = response.data;
                });
        }
    }

}
</script>
