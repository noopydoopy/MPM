<template>
  <div class="about">
    <nav-layout>
    <h1>This is an about page</h1>
    </nav-layout>
  </div>
</template>

<script>
export default {
  mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)
  }
}
</script>

