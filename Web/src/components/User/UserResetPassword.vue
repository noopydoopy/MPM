<template>
    <div>
      <br>
        <b-container>
            <b-card class="text-left">
                <b-card-header>Reset Password</b-card-header>
                <b-card-body>
                    <b-form>
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
                        <b-button type="button" @click="onSubmit" size="sm" variant="primary">Submit</b-button> 
                        <span style="margin-left:10px;"></span>
                        </b-form>
                </b-card-body>
            </b-card>
        </b-container>
    </div>
</template>

<script>
export default {
  props: ["user"],
  data: function() {
    return {
      form: {
        password: null,
        cpassword: null,
      },
      isPasswordValid: true
    };
  },
  methods: {
    onSubmit() {
      if (this.validateForm()) {
        this.user.password = this.form.password;
        this.$emit('update-user', this.user);
      }
    },
    validateForm() {
      this.isPasswordValid = (this.form.password == this.form.cpassword) && this.form.password!=null && this.form.password!=""
      return this.isPasswordValid
    }
  }
};
</script>

