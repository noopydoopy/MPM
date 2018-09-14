<template>
    <div>
        <wide-layout>
        <b-container>
            <b-card class="text-left">
                <b-card-header>User Detail</b-card-header>
                <b-card-body>
                    <b-form @submit="onSubmit" @reset="onReset" >
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
                        <b-form-group id="Name"
                                        label="Name:"
                                        label-for="name">
                            <b-form-input id="name"
                                        v-model="form.name"
                                        required
                                        placeholder="Enter name">
                            </b-form-input>
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

<script>
export default {
  data: () => ({
    form: {
      email: null,
      password: null,
      name: null
    }
  }),
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
      this.$router.push({ path: "/" });
    },
    onReset(evt) {
      evt.preventDefault();
      this.form.email = null;
      this.form.password = null;
      this.form.name = null;
    }
  }
};
</script>

