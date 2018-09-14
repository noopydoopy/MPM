<template>
    <div>
          <nav-layout>
            <h1>Manage Priority</h1>
        </nav-layout>
    </div>
    
</template>
<script>
export default {
        name: 'ManagePriority',

        mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)
  }
}
</script>
