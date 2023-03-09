<template>
  <Navigationbar />
  <modal v-if="modalVisible" @close="closeModal" :body="body">
  </modal>

  <div id="app">
    <router-view />
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Navigationbar from "@/components/Navigationbar.vue";
import Modal from "@/views/Modal.vue";
import { getCurrentInstance } from "vue";
import { AxiosError } from "axios";
import axios from "axios";

@Options({
  components: {
    Navigationbar,
    Modal,
  },
})
export default class App extends Vue {
  public modalVisible: boolean = false;
  public body: string = "";
  public stayOnExit = true;
  public is401Error = false;

  public showModal(): void {
    this.modalVisible = true;
  }

  public closeModal(): void {
    this.modalVisible = false;
    if(!this.stayOnExit){
      this.$router.back();
    }
    else{
      if (this.is401Error){
        this.$router.push("/login");
      }
    }
  }

  private emitter = getCurrentInstance()?.appContext.config.globalProperties.emitter;

  async mounted() {
    this.emitter.on("err", (err: AxiosError) => {
      if(err.response != null){
        if(err.response.status == 500 || err.response.status == 404 || err.response.status == 400){
            this.stayOnExit = false;
        }
        else{
          if (err.response.status == 401){
            this.is401Error = true;
          }
          this.stayOnExit = true;
        }
        this.body = err.response.data;
      }
      else{
        this.stayOnExit = false;
        this.body = "Probeer het later opnieuw."
      }
      this.modalVisible = true;
    });
  }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";
@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Black.otf") format("opentype");
  font-weight: 900;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-ExtraBold.otf") format("opentype");
  font-weight: 800;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Bold.otf") format("opentype");
  font-weight: 700;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-SemiBold.otf") format("opentype");
  font-weight: 600;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Medium.otf") format("opentype");
  font-weight: 500;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Regular.otf") format("opentype");
  font-weight: 400;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Light.otf") format("opentype");
  font-weight: 300;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-ExtraLight.otf") format("opentype");
  font-weight: 200;
  font-style: normal;
}

@font-face {
  font-family: "Metropolis";
  src: url("./assets/fonts/Metropolis-Thin.otf") format("opentype");
  font-weight: 100;
  font-style: normal;
}

#app {
  font-family: $font-family;
  padding-top: 1em;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: $black-color;
  padding: 1em;
}
</style>
