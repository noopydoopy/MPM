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
                        <b-form-group id="Name"
                                        label="Name:"
                                        label-for="name">
                            <b-form-input id="name"
                                        v-model="form.name"
                                        required
                                        placeholder="Enter name">
                            </b-form-input>
                        </b-form-group>
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
      }
    };
  },
  methods: {
    onSubmit() {
      if (this.validateForm()) {
        this.user.email = this.form.email;
        this.user.password = this.form.password;
        this.user.name = this.form.name;
        this.user.isActive = true;
        this.$emit('active-user', this.user);
      } else {
        alert("data fail");
      }
    },
    validateForm() {
      if (this.form.password == this.form.cpassword) {
        return true;
      } else {
        return false;
      }
    }
  }
};
</script>

