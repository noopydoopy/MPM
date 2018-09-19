<template>
  <div>
    <nav-layout>
        <h2> Manange User </h2>
      <b-container class="text-left">  
        <b-form @submit="onSubmit" @reset="onReset" v-if="show">

      <b-form-group id="usernameGroup"
                    label="Your Name:"
                    label-for="username">
        <b-form-input id="username"
                      type="text"
                      v-model="form.username"
                      required
                      placeholder="Enter username">
      </b-form-input>
      </b-form-group>      
      <b-form-group id="passwordGroup"
                    label="Your Password:"
                    label-for="password">
        <b-form-input id="password"
                      type="password"
                      v-model="form.password"
                      required
                      placeholder="Enter password">
        </b-form-input>
      </b-form-group>
      <b-form-group id="nameGroup"
                    label="Your Name:"
                    label-for="name">
        <b-form-input id="name"
                      type="text"
                      v-model="form.name"
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
                      v-model="form.email"
                      required
                      placeholder="Enter email">
        </b-form-input>
      </b-form-group>
      <b-form-group id="isActiveGroup">
      <b-form-checkbox id="isActive"
                     v-model="form.isActive"
                     value="active"
                     unchecked-value="in_active">
      Is Active
    </b-form-checkbox>
    </b-form-group>
    <b-form-group id="isAdminGroup">
    <b-form-checkbox id="isAdmin"
                     v-model="form.isAdmin"
                     value="admin"
                     unchecked-value="non_admin">
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
export default {
    data () {
    return {
      form: {
        email: '',
        name: '',
        checked: []
      },
      foods: [
        { text: 'Select One', value: null },
        'Carrots', 'Beans', 'Tomatoes', 'Corn'
      ],
      show: true
    }
  },
  methods: {
    onSubmit (evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
    },
    onReset (evt) {
      evt.preventDefault();
      /* Reset our form values */
      this.form.email = '';
      this.form.name = '';
      this.form.food = null;
      this.form.checked = [];
      /* Trick to reset/clear native browser form validation state */
      this.show = false;
      this.$nextTick(() => { this.show = true });
    }
  }
}
</script>

