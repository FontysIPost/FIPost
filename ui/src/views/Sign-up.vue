<template>
  <div class="align-left">
    <BtnBack class="button-back" />
    <div class="component-container">
      <h1>{{ !overview ? "Sign-up" : "Overzicht" }}</h1>
      <LoadingIcon v-if="loadPers || loadRoom" />
      <div v-else>
        <div v-if="!overview">
          <div class="group">
            <InputField
              textType="email"
              label="E-mail:"
              v-model:input="email"
              :valid = emailError  
            />
            <InputField
              label="Wachtwoord:"
              v-model:input="password1"
              textType="password"   
              :valid = passwordError          
            />
            <InputField
              label="Herhaal wachtwoord:"
              v-model:input="password2"
              textType="password"
              :valid = passwordError                
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



@Options({
  components: {
    BtnBack,
    InputField,
    SmallBtnFinish,
    CBSearchSuggestions,
    LoadingIcon,
  },
})
export default class RegisterPackage extends Vue {

    errorText; 
    private error: string[] = ["Niet alle velden zijn ingevuld"];
    private email: string = "";
    private emailError: boolean = true;
    private password1: string = "";
    private passwordError: boolean = true;
    private password2: string = ""; //Herhaal wachtwoord (moet gelijk zijn aan 1)
    private overview: boolean = false;
    private btnText: string = "Account Creëren";


    toggleStep() {
    // Clear errors
    this.errorText = false;
    this.error = [];

   

    // Validate if fields have values.
    if (this.email === "" || this.password1 === "" || this.password2 === "") {
      this.errorText = true;
      this.error.push("Niet alle velden zijn ingevuld.");

    }
    else if (this.password1 !== this.password2){
        this.errorText = true;
        this.error.push("De ingevoerde wachtwoorden komen niet overeen");
        this.passwordError = false;
        this.password1 = "";
        this.password2 = "";
    }
    else{
        this.errorText = false;      
        this.passwordError = true;
    }
     if(!this.email.includes('@') || !this.email.includes('.')){
        this.emailError = false;
        this.errorText = true;
        this.error.push("Er mist een '@' en/of '.' bij 'E-mail'");
    }
    else{
         this.emailError = true;
    }
      if (this.errorText) {     
        this.btnText = "Opnieuw";
      }
      else{
          this.btnText = "Account Creëren";
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