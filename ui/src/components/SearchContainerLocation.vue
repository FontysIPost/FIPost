<template>
  <div class="component-container no-padding">
    <div class="search-main">
      <div class="search-fields">
        <h2 class="text">Zoeken</h2>
      </div>

      <div class="search-fields">
        <Search @valueChanged="valueChanged" />
        <ComboBox :options="options" placeholder="Selecteer een veld" />
        <font-awesome-icon
          @click="goToAddLocationPage()"
          class="plus-location"
          icon="plus-square"
        />

        <button
                :class="[ 'small-btn-finish purple']"
                @click="goToLocationOverview()">
          Locaties
        </button>
        <button 
        :class="[ 'small-btn-finish purple']"
         @click="goToCityOverview()">
         Steden
        </button>
        <button 
        :class="[ 'small-btn-finish purple']"
         @click="goToBuildingOverview()">
         Gebouwen
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Search from "./Search.vue";
import ComboBox from "@/components/standardUi/ComboBox.vue";

const SearchContainer = defineComponent({
  components: {
    Search,
    ComboBox,
  },
  methods: {
    valueChanged(val) {
      // Needs implementation.
      console.log(val);
    },
    goToLocationOverview() {
      this.$router.push("/locaties");
    },
    goToAddLocationPage() {
      this.$router.push("/locaties/nieuw");
    },
    goToCityOverview(){
      this.$router.push("/locaties/steden");
    },
    goToBuildingOverview(){
      this.$router.push("/locaties/gebouwen");
    }
  },
  data() {
    return {
      result: "",
      options: ["Stad", "Gebouw", "Ruimte"],
    };
  },
});

export default SearchContainer;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import "@/styling/main.scss";

.search-main {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  row-gap: 0.2rem;
  justify-content: flex-start;
  padding: 0.2rem;
}

.search-main div {
  margin: 0 0.2rem;
}

.search-fields {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  row-gap: 10px;
  justify-content: flex-start;
  align-items: center;
  padding: 0.75rem;

  .text {
    text-align: center;
    margin: 0;
    padding: 0;
  }
}

.search-title {
  text-align: center;
  font-family: $font-family;
  font-weight: 550;
  margin: auto 0 !important;
}

.qr {
  background-image: url("../assets/QRIcoon.png");
  background-size: contain;
  width: 30px;
  height: 30px;
  margin: auto 15px;
  cursor: pointer;
}

.plus-location {
  font-size: 30px;
  align-self: center;
  margin: 0 20px;
  cursor: pointer;
  color: $green-color;
}
.small-btn-finish {
  width: 140px;
  font-size: 14px;
  height: 35px;
  margin-right: 5px;

  color: #ffffff;

  border-radius: 5px;
  border: 0px;
  box-shadow: $shadow;

  font-weight: bold;
  cursor: pointer;
}
.purple {
  background: $modern-purple-color;
}
</style>
