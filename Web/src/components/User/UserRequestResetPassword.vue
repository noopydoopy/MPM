<template>
    <div>
        <b-container>
          <br>
            <b-card class="text-left">
                <b-card-header>Reset Password</b-card-header>
                <b-card-body>
                    <b-form>
                        <b-form-group id="Email"
                                        label="Email address:"
                                        label-for="email">
                            <b-form-input id="email"
                                        type="email"
                                        v-model="user.email"
                                        required
                                        placeholder="Enter email">
                            </b-form-input>
                        </b-form-group>
                        <b-alert variant="danger" dismissible :show="!isEmailValid||!isEmailFound">
                          Email is invalid.
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
  props: ["isEmailFound"],
  data: function() {
    return {
      user: {
        email: null
      },
      isEmailValid: true
    };
  },
  methods: {
    onSubmit() {
      if (this.validateForm()) {
        this.$emit("send-email-reset-password", this.user);
      }
    },
    validateForm() {
      this.isEmailValid = this.user.email != null && this.user.email != "";
      return this.isEmailValid;
    }
  }
};
</script>

