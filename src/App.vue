<template>
  <div id="app">
    <router-view :key="$route.path"/>
  </div>
</template>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>

<script>
export default {
  data: () => ({
    currentTemplate: "nav-layout"
  }),
  methods: {
    switchTemplate() {
      if(this.currentTemplate === 'nav-layout'){
        this.currentTemplate = 'wide-layout'
      }
      else {
        this.currentTemplate = 'nav-layout'
      }
    },
    pageChange() {
      console.log('Page changed')
    }
  },
  mounted() {
    // this.$emit('page-change', this.$route.path)
  }
};
</script>
