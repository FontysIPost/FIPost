<template>
  <div class="component-container no-padding">
    <div class="search-main">
      <div class="search-fields">
        <h3 class="text">Filter</h3>
      </div>
      <div class="search-fields">
        <Search @valueChanged="valueChanged" />
        <ComboBox :options="options" placeholder="Selecteer een veld" />
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
  },
  data() {
    return {
      result: "",
      options: ["Naam", "Ontvanger", "Status"],
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

  .text  {
    text-align: center;
    margin: 0;
    padding: 0;
  }
}

.qr {
  background-image: url("../assets/QRIcoon.png");
  background-size: contain;
  width: 30px;
  height: 30px;
  margin: auto 15px;
  cursor: pointer;
}

.stream {
  width: 400px;
  height: 400px;
}
</style>
