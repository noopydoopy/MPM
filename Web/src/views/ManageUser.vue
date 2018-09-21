<template>
  <div>
    <nav-layout>
        <h2> Manange User </h2>
      <b-container class="text-left">  
        <b-form @submit="onSubmit">

      <b-form-group id="usernameGroup"
                    label="Your Username:"
                    label-for="username">
        <b-form-input id="username"
                      type="text"
                      v-model="user.userName"
                      required
                      placeholder="Enter username" autocomplete="off" readonly>
      </b-form-input>
      </b-form-group>      
      <b-form-group id="nameGroup"
                    label="Your Name:"
                    label-for="name">
        <b-form-input id="name"
                      type="text"
                      v-model="user.name"
                      required
                      placeholder="Enter name">
      </b-form-input>
      </b-form-group>

      <b-form-group id="emailGroup"
                    label="Email address:"
                    label-for="emailAddress"
                    description="We'll never share your email with anyone else.">
        <b-form-input id="emailAddress"
                      type="email"
                      v-model="user.email"
                      required
                      placeholder="Enter email">
        </b-form-input>
      </b-form-group>
      <b-form-group id="isActiveGroup">
      <b-form-checkbox id="isActive"
                     v-model="user.isActive"
                     value="true"
                     unchecked-value="false">
      Is Active
    </b-form-checkbox>
    </b-form-group>
    <b-form-group id="isAdminGroup">
    <b-form-checkbox id="isAdmin"
                     v-model="user.isAdmin"
                     value="true"
                     unchecked-value="false">
      Is Admin
    </b-form-checkbox>
    </b-form-group>
      <b-button type="submit" variant="primary">Save</b-button>
      <b-button type="reset" variant="danger">Cancel</b-button>
    </b-form>

    </b-container>
    </nav-layout>
  </div>
</template>

<script>
import userService from "@/api/UserService";

export default {
    mounted (){
      console.log(this.$route.params.id)
      var vm = this;
      var id = this.$route.params.id;
      userService.getUserByUserId(id).then(
        response => {
          vm.user = response.data;
        },
        error => {
          if (error.response.status == 404) {
            this.$router.push({ path: "/user/list" });
          }
        }
      );
    },
    data () {
    return {
      user: {}
    }
  },
  methods: {
    onSubmit (evt) {
      evt.preventDefault();
      userService.updateUser(this.user).then(
        response => {
          this.$router.push({ path: "/user/list" });
        },
        error => {
          console.log(error);
        }
      )
    },
    onReset (evt) {
      evt.preventDefault();
      this.$router.push({ path: "/user/list" });
    }
  }
}
</script>

