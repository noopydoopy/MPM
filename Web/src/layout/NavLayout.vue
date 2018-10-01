<template>
    <div>
        <b-navbar toggleable="md"  type="dark" variant="info">

            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

            <b-navbar-brand href="#">MPM</b-navbar-brand>

            <b-collapse is-nav id="nav_collapse" class="text-left">

                <b-navbar-nav >
                <b-nav-item href="#"><router-link to="/">Home</router-link> </b-nav-item>
                <b-nav-item href="#"><router-link to="/dashboard">Dashboard</router-link> </b-nav-item>
                <b-nav-item href="#"><router-link to="/about">About</router-link></b-nav-item>
                <b-nav-item href="#"><router-link to="/user/list">Users</router-link></b-nav-item>
                <b-nav-item-dropdown>
                    <template slot="button-content">
                        Administrator
                    </template>
                    <b-dropdown-item href="#"><router-link to="/admin/manageuser">Manage User</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managetype">Manage Type</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managepriority">Manage Priority</router-link></b-dropdown-item>
                </b-nav-item-dropdown>
                </b-navbar-nav>

                <!-- Right aligned nav items -->
                <b-navbar-nav class="ml-auto">

                <b-nav-form>
                    <b-form-input size="sm" class="mr-sm-2" type="text" placeholder="Search"/>
                    <b-button size="sm" class="my-2 my-sm-0" type="button">Search</b-button>
                </b-nav-form>


                <b-nav-item-dropdown right>
                    <!-- Using button-content slot -->
                    <template slot="button-content">
                    <em>Account</em>
                    </template>
                    <b-dropdown-item href="#"><router-link to="/user/profile">Profile</router-link></b-dropdown-item>
                    <b-dropdown-item @click="logOut">Sign out</b-dropdown-item>
                </b-nav-item-dropdown>
                </b-navbar-nav>

            </b-collapse>
        </b-navbar>
        <slot></slot>
    </div>
</template>

<style scoped>
nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>

<script>
export default {
  data: () => ({
    title: "Nav template"
  }),
  methods: {
    logOut: function() {
      localStorage.removeItem("token");
      localStorage.removeItem("rememberMe");
      this.$router.push({ path: "/login" });
    }
  }
};
</script>
