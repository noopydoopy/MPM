<template>
    <div>
        <wide-layout>
        <b-container>
            <b-card class="text-left">
                <b-card-header>Login</b-card-header>
                <b-card-body>
                    <b-form @submit="onSubmit" @reset="onReset" >
                        <b-form-group id="Email"
                                        label="Email address:"
                                        label-for="email"
                                        description="We'll never share your email with anyone else.">
                            <b-form-input id="email"
                                        type="email"
                                        v-model="user.email"
                                        required
                                        placeholder="Enter email">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="Password"
                                        label="Password:"
                                        label-for="password">
                            <b-form-input id="password"
                                        type="password"
                                        v-model="user.password"
                                        required
                                        placeholder="Enter name">
                            </b-form-input>
                        </b-form-group>
                        <div class="invalid-feedback" v-if="!isLogin">
                            The email or password you entered is incorrect.
                        </div>
                        <b-form-group id="RememberMe">          
                            <b-form-checkbox v-model="remember">Remeber me</b-form-checkbox>              
                        </b-form-group>
                        <b-button type="submit" size="sm" variant="primary">Submit</b-button> 
                        <span style="margin-left:10px;"></span>
                        <b-button type="reset" size="sm" variant="danger">Reset</b-button>
                        </b-form>
                </b-card-body>
            </b-card>
        </b-container>
        </wide-layout>
    </div>
</template>

<style scoped>
.card-body {
  /* padding: 0px !important; */
}
</style>

<script>
import userService from "@/api/UserService";

export default {
  data: () => ({
    user: {
      email: null,
      password: null
    },
    remember: false,
    isLogin: false
  }),
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      userService.logIn(this.user).then(
        response => {
          this.isLogin = true
          if (response.data.token) {
            localStorage.setItem("token", response.data.token);
          }
          this.$router.push({ path: "/" });
        },
        error => {
          this.isLogin = false
          }
      );
    },
    onReset(evt) {
      evt.preventDefault();
      this.user.email = null;
      this.user.password = null;
      this.remember = false;
    }
  }
};
</script>

