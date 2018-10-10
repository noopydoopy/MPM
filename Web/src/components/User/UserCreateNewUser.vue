<template>
  <div><br>
    <h4> Create New User </h4>
    <b-container class="text-left">  
      <b-form>
        <b-form-group id="nameGroup"
          label="Name:"
          label-for="name">
          <b-form-input id="name"
            type="text"
            v-model="user.name"
            required
            placeholder="Enter name">
          </b-form-input>
        </b-form-group>
        <b-alert variant="danger" dismissible :show="!isNameValid">
          Please fill name
        </b-alert>
        <b-form-group id="emailGroup"
          label="Email address:"
          label-for="emailAddress">
        <b-form-input id="emailAddress"
          type="email"
          v-model="user.email"
          required
          placeholder="Enter email">
        </b-form-input>
        </b-form-group>
        <b-alert variant="danger" dismissible :show="!isEmailValid">
          Please provide a valid email.
        </b-alert>
        <b-form-group id="isAdminGroup">
        <b-form-checkbox id="isAdmin"
          v-model="user.isAdmin"
          value="true"
          unchecked-value="false">
          Is Admin
        </b-form-checkbox>
        <b-alert variant="danger" dismissible :show="!isCreate">
          There are promblem when create new user
        </b-alert> 
        </b-form-group>
        <b-button type="button" @click="onSubmit" variant="primary">Submit</b-button> 
        <b-button type="cancel" @click="onCancel" variant="danger">Cancel</b-button>
        </b-form>
    </b-container>
  </div>
</template>

<script>
import userService from "@/api/UserService";

export default {
    data () {
      return {
        user: {
          isAdmin: false
        },
        isEmailValid: true,
        isNameValid: true,
        isCreate: true
      }
    },
    methods: {
      onSubmit (evt) {
        if (this.validateForm()) {
          userService.createNewUser(this.user).then(
              response => {
                this.isCreate = true
                this.$router.push({ path: "/user/list" });
              },
              error => {
                this.isCreate = false
                console.log(error);
              }
          )
        }
      },
      onCancel (evt) {
        evt.preventDefault();
        this.$router.push({ path: "/user/list" });
      },
      validateForm() {
        this.isEmailValid = this.validateEmail(this.user.email);
        this.isNameValid = (this.user.name != null && this.user.name!="");
        return this.isEmailValid && this.isNameValid;
      },
      validateEmail(email) {
        var regx = /^([\w.-]+)@(\[(\d{1,3}\.){3}|(([a-zA-Z\d-]+\.)+))([a-zA-Z]{2,4}|\d{1,3})(\]?)$/;
        return regx.test(email);
      }
    }
  }
</script>
