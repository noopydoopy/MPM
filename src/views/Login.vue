<template>
    <div>
        <wide-layout>
        <b-container>
            <b-card class="text-left">
                <b-card-header>Login</b-card-header>
                <b-card-body>
                    <b-form @submit="onSubmit" @reset="onReset" >
                        <b-form-group id="exampleInputGroup1"
                                        label="Email address:"
                                        label-for="email"
                                        description="We'll never share your email with anyone else.">
                            <b-form-input id="email"
                                        type="email"
                                        v-model="form.email"
                                        required
                                        placeholder="Enter email">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="exampleInputGroup2"
                                        label="Password:"
                                        label-for="password">
                            <b-form-input id="password"
                                        type="password"
                                        v-model="form.password"
                                        required
                                        placeholder="Enter name">
                            </b-form-input>
                        </b-form-group>
                        
                        <b-form-group id="exampleGroup4">
              
                            <b-form-checkbox v-model="form.remember">Remeber me</b-form-checkbox>
              
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
export default {
  data: () => ({
    form: {
      email: null,
      password: null,
      remember: false
    }
  }),
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
      this.$router.push({ path: '/' })
    },
    onReset(evt) {
      evt.preventDefault();
    this.form.email = null
    this.form.password = null
    this.form.remember = false
    }
  }
};
</script>

