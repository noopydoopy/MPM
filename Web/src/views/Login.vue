<template>
    <div>
        <nav-layout>
          <br>
        <b-container>
            <b-card class="text-left">
                <b-card-header>Login</b-card-header>
                <b-card-body>
                    <b-form @submit="onSubmit" @reset="onReset" >
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
                        <b-alert variant="danger" dismissible :show="!isLogin">
                          The email or password you entered is incorrect.
                        </b-alert>        
                        <b-form-checkbox id="RememberMe" v-model="remember">Remeber me</b-form-checkbox>              
                        <b-link to="#">Forget password</b-link><br><br>
                        <b-button type="submit" size="sm" variant="primary">Submit</b-button> 
                        <span style="margin-left:10px;"></span>
                        <b-button type="reset" size="sm" variant="danger">Reset</b-button>
                        </b-form>
                </b-card-body>
            </b-card>
        </b-container>
        </nav-layout>
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
      grantType: "password",
      email: null,
      password: null
    },
    remember: false,
    isLogin: true
  }),
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      userService.logIn(this.user).then(
        response => {
          this.isLogin = true;
          if (response.data) {
            localStorage.setItem("token", JSON.stringify(response.data));
            if (this.remember) {
              var rememberMe = {
                refreshToken: response.data.refreshToken,
                refreshTokenExpire: response.data.refreshTokenExpire
              };
              localStorage.setItem("rememberMe", JSON.stringify(rememberMe));
            }
          }
          this.$router.push({ path: "/" });
        },
        error => {
          this.isLogin = false;
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
