<template>
  <div class="nav-container">
    <div class="nav-main">
      <div class="logo-container" @click="logoClicked()">
        <div class="logo"></div>
      </div>
      <div class="user" v-if="email.toString().endsWith('@student.fontys.nl')">
          <div class="role">
              <div v-if="role === 0">
                  Administrator: {{email}}
              </div>
              <div v-else>
                  Medewerker: {{email}}
              </div>
          </div>
          <div>
              <button type="button" class="modal-default-button" @click="logout">Logout</button>
          </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "axios";
const Navigationbar = defineComponent({
    data() {
        return {
            email: String,
            role: Number
        };
    },
    methods: {
    logoClicked() : void {
      this.$router.push('/');
    },
      logout(){
        localStorage.clear();
        location.reload();
          setTimeout(() => {
              location.reload();
          }, 10);
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
            this.role = response.data.role;
        })
        .catch(() => {
            this.$router.push("/login");
        })
    }
});
export default Navigationbar;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import "@/styling/main.scss";

.nav-container {
  position: -webkit-sticky; /* Safari */
  position: sticky;
  top: 0;
  background-color: #fff;
  width: 100%;
}

.nav-main {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  width: 100%;
  height: 50px;
  box-shadow: 0 2px 10px rgba(88, 88, 88, 0.16);
  overflow: hidden;
  align-content: center;
  font-family: $font-family;

  .logo-container {
    max-width: 100px;
    margin: 10px;
    margin-left: 15px;
    cursor: pointer;
    display: flex;
    
    .logo {
      background: url("../assets/logo.png");
      background-repeat: no-repeat;
      background-size: contain;
      height: 30px;
      width: 100px;
      margin: 3px;
      align-self: center;
    }
  }

  .user {
    display: flex;
    margin: 20px;
  }
}
@media only screen and (max-width: 586px) {
  .role {
    visibility: hidden;
    width: 1px;
  }
  .modal-default-button {
    margin-top: 8px;
  }
} 
.nav-extra {
  display: none;
}

@media only screen and (max-width: 1000px) {
  .nav-extra {
    overflow: hidden;
    display: flex;
    padding: 10px 0;

    .btn-container-extra {
      display: flex;
      justify-content: space-evenly;
      margin: auto;
    }
  }
}.modal-default-button {
   margin-left: 20px;
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
 .role {
   margin-top: 5px;
 }
</style>
