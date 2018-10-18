<template>
<div>
    <nav-layout>
        <b-container fluid>
          <h2>Tasklist</h2>
            <!-- User Interface controls -->
            <b-row>
              <b-col md="6" class="my-1">
                  <b-form-group horizontal label="Filter" class="mb-0">
                  <b-input-group>
                      <b-form-input v-model="filter" placeholder="Type to Search" />
                      <b-input-group-append>
                      <b-btn :disabled="!filter" @click="filter = ''">Clear</b-btn>
                      </b-input-group-append>
                  </b-input-group>
                  </b-form-group>
              </b-col>
            </b-row>

            <!-- Main table element -->
            <b-table show-empty
                    stacked="md"
                    :items="tasks"
                    :fields="fields"
                    :current-page="currentPage"
                    :per-page="perPage"
                    :filter="filter"
                    :sort-by.sync="sortBy"
                    :sort-desc.sync="sortDesc"
                    :sort-direction="sortDirection"
                    @filtered="onFiltered"
            >
            <template slot="title" slot-scope="row">
              <span class="color-box" :style="'background-color: ' + row.item.priorityColor +' !important'"></span>
              {{row.value}}
            </template>
            <template slot="description" slot-scope="row">{{descriptionDisplay(row.item.description)}}</template>
            <template slot="startDate" slot-scope="row">{{row.value | moment("Do MMM YYYY")}}</template>
            <template slot="endDate" slot-scope="row">{{row.value | moment("Do MMM YYYY")}}</template>
            <template slot="isActive" slot-scope="row">
               <b-form-checkbox v-model="row.value" disabled></b-form-checkbox>
            </template>            
            <template slot="actions" slot-scope="row">
                <b-button size="sm" @click.stop="openModal(row.item, row.index, $event.target)" class="mr-1">Info modal</b-button>
                <b-btn size="sm" v-b-modal.modalPrevent>modal</b-btn>
            </template>           
            </b-table>

            <b-row>
              <b-col md="6" class="my-1">
                  <b-pagination :total-rows="totalRows" :per-page="perPage" v-model="currentPage" class="my-0" />
              </b-col>
            </b-row>

            <b-modal id="modalInfo" @hide="resetModal" :title="task.title" size="lg"  class="text-left">            
              <b-form-group horizontal :label-cols="2" label="Title" label-for="title">
                <b-form-input id="title"></b-form-input>
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Description" label-for="description">
                <b-form-input id="description"></b-form-input>
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Start date" label-for="startdate">
                <b-form-input id="startdate"></b-form-input>
              </b-form-group> 
              <b-form-group horizontal :label-cols="2" label="End date" label-for="enddate">
                <b-form-input id="enddate"></b-form-input>
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Assign To" label-for="assignto">
                <b-form-select id="assignto" :options="users" value-field="userId" text-field="name" class="mb-3" />
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Type" label-for="type">
                <b-form-select id="type" :options="types" value-field="typeId" text-field="name" class="mb-3" />
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Priority" label-for="priority">
                <b-form-select id="priority" :options="priorities" value-field="priorityId" text-field="priorityNumber" class="mb-3" />
              </b-form-group>
              <b-form-group horizontal :label-cols="2" label="Active" label-for="input_default">
                 <b-form-checkbox></b-form-checkbox>
              </b-form-group>
            </b-modal>
        </b-container>
    </nav-layout>
</div>
</template>

<script>
import axios from "axios";
import { mapState } from "vuex";

export default {
  props: ["projectid"],
  data() {
    return {
      ProjectId: null,
      fields: [
        { key: "title", sortable: true, sortDirection: "asc" },
        { key: "description", sortable: true, sortDirection: "asc" },
        { key: "startDate", sortable: true, sortDirection: "asc" },
        { key: "endDate", sortable: true, sortDirection: "asc" },
        { key: "priority", sortable: true, sortDirection: "asc" },
        { key: "type", sortable: true, sortDirection: "asc" },
        { key: "assignTo.name", sortable: true, sortDirection: "asc" },
        { key: "isActive", label: "Active" },
        { key: "actions", label: "Actions" }
      ],
      currentPage: 1,
      perPage: 5,
      totalRows: 0,
      sortBy: null,
      sortDesc: false,
      sortDirection: "asc",
      filter: null,
      task: { title: "", content: "" },
    };
  },
  mounted() {
    if (this.$route.params.projectId != null && this.$route.params.projectId > 0) {
      this.ProjectId = this.$route.params.projectId;
      this.loadtasks(this.ProjectId);
    }
  },
  computed: {
    sortOptions() {
      // Create an options list from our fields
      return this.fields.filter(f => f.sortable).map(f => {
        return { text: f.label, value: f.key };
      });
    },
    ...mapState('taskListModule', {
      tasks: state => state.taskLists,
      users: state => state.users,
      types: state => state.types,
      priorities: state => state.priorities
    })
  },
  methods: {
    descriptionDisplay(value) {
      return value.substring(0, 2) + "...";
    },
    openModal(item, index, button) {
      this.task.title = item.title;
      this.task.content = item.description;

      this.loadUsers();
      this.loadAllTypes();
      this.loadAllPriorities();

      this.$root.$emit("bv::show::modal", "modalInfo", button);
    },
    resetModal() {
      this.task.title = "";
      this.task.content = "";
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    loadtasks(projectid) {
      this.$store.dispatch("taskListModule/requestTaskLists", projectid);
    },
    loadUsers() {
      this.$store.dispatch("taskListModule/requestUsers");
    },
    loadAllTypes() {
      this.$store.dispatch("taskListModule/requestTypes");
    },
    loadAllPriorities() {
      this.$store.dispatch("taskListModule/requestPriorities");
    },

    clearName() {
      this.name = "";
    },
    handleOk(evt) {
      // Prevent modal from closing
      evt.preventDefault();
      if (!this.name) {
        alert("Please enter your name");
      } else {
        this.handleSubmit();
      }
    },
    handleSubmit() {
      this.names.push(this.name);
      this.clearName();
      this.$refs.modal.hide();
    }
  }
};
</script>

<style scoped>
.color-box {
  display: inline-block;
  width: 16px;
  height: 16px;
}
</style>