<template>
 <div>
    <nav-layout>
      Dashboard
        <b-table striped hover :items="items">
          <template slot="project_name" slot-scope="data">
            <router-link :to="{ name: 'TaskList', params: { projectId: data.item.id }}">{{data.value}}</router-link>
          </template>
        </b-table>
    </nav-layout>
  </div>
</template>

<script>
const items = [
  {  id: 1, project_name: 'Harmonie', isActive: true },
  {  id: 2, project_name: 'GBCP', isActive: true },
  {  id: 3, project_name: 'Workflow', isActive: true },
  {  id: 4, project_name: 'Diabase', isActive: true  }
]

import { mapGetters } from "vuex";
export default {
    data () {
    return {
      items: items
    }
  },
  computed: {
    ...mapGetters({
      user: "authenticationModule/user"
    })
  },
  mounted() {
    if(this.user==null){
      this.$router.push({ path: "/login" });
    }
  }
}
</script>

