<template>
<div>
        <template>
    <div>
        <h3>
            UserList
        </h3>
    <b-container>
      
    <!-- User Interface controls -->
    <b-row>
      <b-col md="5" class="my-1">
        <b-form-group horizontal label="Filter" class="mb-0">
          <b-input-group>
            <b-form-input v-model="filter" placeholder="Type to Search" />
            <b-input-group-append>
              <b-btn :disabled="!filter" @click="filter = ''">Clear</b-btn>
            </b-input-group-append>
          </b-input-group>
        </b-form-group>
      </b-col>
      <b-col md="5" class="my-1">
        <b-form-group horizontal label="Per page" class="mb-0">
          <b-form-select :options="pageOptions" v-model="perPage" />
        </b-form-group>
      </b-col>
      <b-col md="2" class="my-1">
        <b-button size="sm" @click="CreateNewUser()" class="btn btn-info" style="float: right;" >
            Create New User
        </b-button>
      </b-col>
    </b-row>

    <!-- Main table element -->
    <b-table show-empty
             stacked="md"
             :items="users"
             :fields="fields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered"
    >
      <template slot="name" slot-scope="row"> {{row.value}} </template>
      <template slot="email" slot-scope="row"> {{row.value}} </template>
      <template slot="isActive" slot-scope="row">{{row.value ?'Yes':'No'}}</template>
      <template slot="isAdmin" slot-scope="row">{{row.value ?'Yes':'No'}}</template>
      <template slot="actions" slot-scope="row">
        <b-button size="sm" :href="`/admin/manageuser/${row.item.userId}`" class="mr-1">
          Edit Info
        </b-button>
      </template>
      <template slot="delete" slot-scope="row">
        <b-button size="sm" v-b-modal.typeModal @click="OpenTypeModal(row.item)" class="btn btn btn-danger">
          Delete Row
        </b-button>
      </template>
    </b-table>

    <b-row>
      <b-col md="6" class="my-1">
        <b-pagination :total-rows="totalRows" :per-page="perPage" v-model="currentPage" class="my-0" />
      </b-col>
    </b-row>
  </b-container>
    </div>
    </template>
    <b-modal  id="typeModal"
            title="Confirm Remove"
            @ok="ConfirmDelete(userDelete.userId)">
                Do you sure to Remove {{userDelete.name}}
        </b-modal>
    </div>
</template>

<script>
import userService from "@/api/UserService";

export default {
  data() {
    return {
      users: [],
      fields: [
        { key: "name", label: "Name", sortable: true, sortDirection: "desc" },
        { key: "email", label: "Email", sortable: true, class: "text-center" },
        { key: "isActive", label: "is Active" },
        { key: "isAdmin", label: "is Admin" },
        { key: "actions", label: "Actions" },
        { key: "delete", label: "Delete" }
      ],
      currentPage: 1,
      perPage: 5,
      //totalRows: 0,
      pageOptions: [5, 10, 15],
      sortBy: null,
      sortDesc: false,
      sortDirection: "asc",
      filter: null,
      userDelete: {name:null}
    };
  },
  computed: {
    sortOptions() {
      // Create an options list from our fields
      return this.fields.filter(f => f.sortable).map(f => {
        return { text: f.label, value: f.key };
      });
    },
    totalRows: {
      // getter
      get: function() {
        return this.users.length;
      },
      // setter
      set: function(newValue) {}
    }
    // totalRows(){
    //     return this.users.length
    // }
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    OpenTypeModal(user, index) {
      this.userDelete = user
    },
    ConfirmDelete(id) {
      
      var vm = this;
      userService.deleteUser(id).then(
        response => {
          var index = this.users.indexOf(this.userDelete);
          this.users.splice(index, 1)
        },
        error => {
          //error
        }
      );
    },
    CreateNewUser() {
      this.$router.push({ path: "/user/createNewUser" });
    }
  },
  mounted() {
    var vm = this;
    userService.getAllUser().then(
      response => {
        vm.users = response.data;
      },
      error => {
        vm.users == null;
      }
    );
  }
};
</script>
