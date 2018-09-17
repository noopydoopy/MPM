<template>
 <div>
    <nav-layout>
      Dashboard
        <b-table striped hover :items="items">
          <template slot="project_name" slot-scope="data">
            <a :href="`/tasklist`">
              {{data.value}}
            </a>
          </template>
        </b-table>
    </nav-layout>
  </div>
</template>

<script>
const items = [
  {  project_name: 'Harmonie', isActive: true },
  {  project_name: 'GBCP', isActive: true },
  {  project_name: 'Workflow', isActive: true },
  {  project_name: 'Diabase', isActive: true  }
]

export default {
    data () {
    return {
      items: items
    }
  }
}
</script>

