<template>
  <div class="align-left">
    <div class="component-container">
      <h1>{{ !overview ? "Login" : "Overzicht" }}</h1>
      <LoadingIcon v-if="loadPers || loadRoom" />
      <div v-else>
        <div v-if="!overview">
          <div class="group">
            <InputField
              textType="email"
              label="E-mail:"
              v-model:input="email"
            />
            <InputField
              label="Wachtwoord:"
              v-model:input="password"
              textType="password"
            />
          </div>
        </div>
        <ul v-if="errorText">
            <li v-for="e in error" :key="e" class="error-text">{{ e }}</li>
        </ul>
        <SmallBtnFinish
          :text="btnText"
          :red="btnText === 'Opnieuw' ? true : false"
          v-on:click="toggleStep"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import BtnBack from "@/components/standardUi/BtnBack.vue";
import InputField from "@/components/standardUi/InputField.vue";
import SmallBtnFinish from "@/components/standardUi/SmallBtnFinish.vue";
import CBSearchSuggestions from "@/components/standardUi/CBSearchSuggestions.vue";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import axios from "axios";



@Options({
  components: {
    BtnBack,
    InputField,
    SmallBtnFinish,
    CBSearchSuggestions,
    LoadingIcon,
  },
})
export default class Login extends Vue {

    errorText;
    private error: string[] = ["Niet alle velden zijn ingevuld"];
    private email: string = "";
    private password: string = "";
    private overview: boolean = false;
    private btnText: string = "Inloggen";


    toggleStep() {
      // Clear errors
      this.errorText = false;
      this.error = [];

      // Validate if fields have values.
      if (this.email === "" || this.password === "") {
        this.errorText = true;
        this.error.push("Niet alle velden zijn ingevuld.");
      }
      else{
        //execute login code
        axios.post('https://localhost:44369/api/Authentication/login', {
          email: this.email,
          password: this.password
        }) .then(function (response) {
          localStorage.setItem('token', response.data);
          window.location.replace("/")
          console.log('Hoi')
        })
        .catch(err => {
          if (err.response.status == 401){
            this.errorText = true;
            this.error = [];
            this.error.push("Er is iets mis gegaan");
          }
        })
      }

      if (this.errorText) {
        this.btnText = "Opnieuw";
      }
    }
}

</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.SmallBtnFinish{
    width: 180px;
}

.align-left {
  text-align: left;
}
.margin-button {
  margin-right: 5%;
}

hr {
  opacity: 0.4;
  margin: 0px 0;
}

.group {
  margin-bottom: 2em;
}

p {
  margin-bottom: 1em;
}
</style>
