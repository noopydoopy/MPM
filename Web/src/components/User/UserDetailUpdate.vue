<template>
    <div>
        <wide-layout>
        <b-container>
            <b-card class="text-left">
                <b-card-header>User Detail</b-card-header>
                <b-card-body>
                    <b-form>
                        <b-form-group id="Email"
                                        label="Email address:"
                                        label-for="email">
                            <b-form-input id="email"
                                        type="email"
                                        v-model="form.email"
                                        required
                                        placeholder="Enter email">
                            </b-form-input>
                        </b-form-group>
                        <b-alert variant="danger" dismissible :show="!isEmailValid">
                          Please provide a valid email.
                        </b-alert>
                        <b-form-group id="Password"
                                        label="Password:"
                                        label-for="password">
                            <b-form-input id="password"
                                        type="password"
                                        v-model="form.password"
                                        required
                                        placeholder="Enter password">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="Confirm Password"
                                        label="Confirm Password:"
                                        label-for="cpassword">
                            <b-form-input id="cpassword"
                                        type="password"
                                        v-model="form.cpassword"
                                        required
                                        placeholder="Confirm password">
                            </b-form-input>
                        </b-form-group>
                        <b-alert variant="danger" dismissible :show="!isPasswordValid">
                          Password is not matched.
                        </b-alert>
                        <b-form-group id="Name"
                                        label="Name:"
                                        label-for="name">
                            <b-form-input id="name"
                                        v-model="form.name"
                                        required
                                        placeholder="Enter name">
                            </b-form-input>
                        </b-form-group>
                        <b-alert variant="danger" dismissible :show="!isNameValid">
                          Please fill name
                        </b-alert>
                        <b-button type="button" @click="onSubmit" size="sm" variant="primary">Submit</b-button> 
                        <span style="margin-left:10px;"></span>
                        </b-form>
                </b-card-body>
            </b-card>
        </b-container>
        </wide-layout>
    </div>
</template>

<script>
export default {
  props: ["user"],
  data: function() {
    return {
      form: {
        email: this.user.email,
        password: null,
        cpassword: null,
        name: this.user.name
      },
      isEmailValid: true,
      isPasswordValid: true,
      isNameValid: true
    };
  },
  methods: {
    onSubmit() {
      if (this.validateForm()) {
        this.user.email = this.form.email;
        this.user.password = this.form.password;
        this.user.name = this.form.name;
        this.user.isActive = true;
        this.$emit("update-user", this.user);
      }
    },
    validateForm() {
      this.isEmailValid = this.validateEmail(this.form.email);
      this.isPasswordValid = this.validatePassword();
      this.isNameValid = (this.form.name != null && this.form.name!="");
      return this.isEmailValid && this.isPasswordValid && this.isNameValid;
    },
    validateEmail(email) {
      var regx = /^([\w.-]+)@(\[(\d{1,3}\.){3}|(([a-zA-Z\d-]+\.)+))([a-zA-Z]{2,4}|\d{1,3})(\]?)$/;
      return regx.test(email);
    },
    validatePassword() {
      return (
        this.form.password == this.form.cpassword && this.form.password != null && this.form.password != ""
      );
    }
  }
};
</script>

