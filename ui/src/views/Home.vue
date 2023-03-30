<template>
  <div class="h-container">
    <div class="container-group">
      <div class="header">Start</div>
      <hr />
      <Menu />
    </div>
     <div class="container-group">
      <div class="header">Dashboard</div>
      <hr />
       <div class="flex-container">
         <div class="email-inline">
           <div v-if="role === 0">
             Administrator:
           </div>
           <div v-else>
             Medewerker:
           </div>
           {{email}}
         </div>
         <div>
           <button type="button" class="modal-default-button" @click="logout">Logout</button>
         </div>
         <br>
       </div>
    </div>
  </div>
</template>

<script lang="ts">

import { defineComponent } from "vue";
import Menu from "@/components/Menu.vue";
import LocationSvg from "@/components/svg/LocationSvg.vue";
import RegisterSvg from "@/components/svg/RegisterSvg.vue";
import ScanSvg from "@/components/svg/ScanSvg.vue";
import SearchSvg from "@/components/svg/SearchSvg.vue";
import axios from "axios";
const Home = defineComponent({
  components: {
    Menu
  },
  data() {
    return {
      email: String,
    };
  },
  methods: {
    registerClicked() : void {
      this.$router.push("/registratie");
    },
    searchClicked() : void {
      this.$router.push("/overzicht");
    },
    scanClicked() : void {
      this.$router.push("/pakket/1");
    },
    locationClicked() : void {
      this.$router.push("/locaties");
    },
    logout(){
      localStorage.clear();
      this.$router.push("/login");
    }
  },
  mounted() {
    //axios get
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    axios.get('https://localhost:44369/api/Authentication/auth', config )
        .then(response => {
          this.email = response.data.email;
        })
    .catch(err => {
      this.$router.push("/login");
    })
  }
});
export default Home;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import "@/styling/main.scss";

.container-group {
  margin: 2em;
  margin-bottom: 4em;
}

.container-group {
  .header {
    font-family: $font-family;
    font-size: 2em;
    font-weight: 600;
    text-align: left;
  }

  hr {
    width: 75vw;
    border: 0;
    border-top: 3px solid $gray-color;
    opacity: 0.2;
    margin: 1em 0;
  }
}
.modal-default-button {
  float: left;
  background-color: $modern-purple-color;
  border: none;
  width: 100px;
  height: 30px;
  border-radius: $small-border-radius;
  box-shadow: $shadow;
  font-family: $font-family;
  color: white;
  cursor: pointer;
}
.flex-container {
  display: flex;
  flex-wrap: nowrap;
}
.flex-container > div {
  display: flex;
  align-items: center;
  justify-content: center;
}
.email-inline{
  margin-right: 10px;
}
</style>
