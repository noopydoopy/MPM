<template>
    <div>
        <nav-layout>
        
        <component :is="component" v-bind:user="user" @active-user="activeUser"></component>
        </nav-layout>
    </div>
</template>

<script>
import Profile from "@/components/User/Profile";
import UserList from "@/components/User/UserList";
import UserDetailUpdate from "@/components/User/UserDetailUpdate";
import UserDetailUpdateError from "@/components/User/UserDetailUpdateError";
import axios from "axios";

export default {
  components: {
    Profile,
    UserList,
    UserDetailUpdate,
    UserDetailUpdateError
  },
  data: () => ({
    component: "profile",
    user: {},
    apiHost: "https://localhost:44382"
  }),
  mounted() {
    console.log(this.$route.params.mode);
    if (this.$route.params.mode === "profile") {
      this.component = "profile";
    } else if (this.$route.params.mode === "list") {
      this.component = "user-list";
    } else if (this.$route.params.mode === "update") {
      var vm = this;
      var param = this.$route.query.code;
      const url = this.apiHost + "/api/Users/code/" + param;
      axios.get(url).then(
        function(response) {
          vm.user = response.data;
          if (vm.user.isActive == true) {
            vm.$router.push({ path: "/login" });
          } else {
            vm.component = "user-detail-update";
          }
        },
        error => {
          if (error.response.status == 404) {
            vm.component = "user-detail-update-error";
          }
        }
      );
    }
  },
  methods: {
    activeUser: function(user) {
      console.log("test");
      const url = this.apiHost + "/api/Users/" + user.userId;
      axios.put(url, user).then(
        function(response) {
          vm.$router.push({ path: "/login" });
        },
        error => {
          console.log(error);
        }
      );
    }
  }
};
</script>
