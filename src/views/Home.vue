<template>
  <div class="">
    <nav-layout>
     
      <img alt="Vue logo" src="../assets/logo.png">
      <HelloWorld msg="Welcome to Your Vue.js App"/>
    </nav-layout>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'home',
  components: {
    HelloWorld
  },
  mounted() {
    console.log(this.$route.path)
    this.$emit('page-change', this.$route.name)
  }
}
</script>
