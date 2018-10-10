<template>
    <div>
        <nav-layout>
        
        <component :is="component" v-bind:user="user" v-bind:isEmailFound="isEmailFound" @update-user="updateUser" @send-email-reset-password="sendEmailResetPassword"></component>
        </nav-layout>
    </div>
</template>

<script>
import Profile from "@/components/User/Profile";
import UserList from "@/components/User/UserList";
import UserDetailUpdate from "@/components/User/UserDetailUpdate";
import UserDetailUpdateError from "@/components/User/UserDetailUpdateError";
import UserResetPassword from "@/components/User/UserResetPassword";
import UserRequestResetPassword from "@/components/User/UserRequestResetPassword";
import UserRequestResetPasswordSend from "@/components/User/UserRequestResetPasswordSend";
import UserCreateNewUser from "@/components/User/UserCreateNewUser";
import userService from "@/api/UserService";

export default {
  components: {
    Profile,
    UserList,
    UserDetailUpdate,
    UserResetPassword,
    UserRequestResetPassword,
    UserRequestResetPasswordSend,
    UserCreateNewUser,
    UserDetailUpdateError
  },
  data: () => ({
    component: "profile",
    user: {},
    isEmailFound: true 
  }),
  mounted() {
    console.log(this.$route.params.mode);
    if (this.$route.params.mode === "profile") {
      this.component = "profile";
    } else if (this.$route.params.mode === "list") {
      this.component = "user-list";
    } else if (this.$route.params.mode === "update") {
      var code = this.$route.query.code;
      userService.getUserByCode(code).then(
        response => {
          this.user = response.data;
          if (this.user.isActive == true) {
            this.$router.push({ path: "/login" });
          } else {
            this.component = "user-detail-update";
          }
        },
        error => {
          if (error.response.status == 404) {
            this.component = "user-detail-update-error";
          }
        }
      );
    }else if (this.$route.params.mode === "resetPassword"){
      var code = this.$route.query.code;
      userService.getUserResetPasswordByCode(code).then(
        response => {
          this.user = response.data;
          this.component = "user-reset-password";
        },
        error => {
          if (error.response.status == 404) {
            this.component = "user-detail-update-error";
          }
        }
      );
    }else if (this.$route.params.mode === "requestResetPassword"){
      this.component = "user-request-reset-password";
    }else if (this.$route.params.mode === "requestResetPasswordSend"){
      this.component = "user-request-reset-password-send";
    }else if (this.$route.params.mode === "createNewUser"){
      this.component = "user-create-new-user";
    }
  },
  methods: {
    updateUser: function(user) {
      userService.updateUser(user).then(
        response => {
          this.$router.push({ path: "/login" });
        },
        error => {
          console.log(error);
        }
      )
    },
    sendEmailResetPassword: function(user) {
      userService.sendEmailResetPassword(user).then(
        response => {
          this.isEmailFound = true
          this.$router.push({ path: "/user/requestResetPasswordSend" });
        },
        error => {
          this.isEmailFound = false
          console.log(error);
        }
      )
    }
  }
};
</script>
