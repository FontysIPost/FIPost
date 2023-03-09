<template>
  <div class="search-container">
    <input v-model="inputValue" placeholder="Zoeken..." />
    <div class="icon-container">
      <font-awesome-icon class="s-btn" icon="search" size="1x" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

const Search = defineComponent({
  data() {
    return {
      inputValue: "",
    };
  },
  watch: {
    inputValue(newValue, oldValue) {
      this.$emit("valueChanged", newValue);
    },
  },
});
export default Search;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import "@/styling/main.scss";

.search-container {
  border: none;
  font-family: $font-family;
  font-size: 12px;
  background-color: $background-color;
  color: $black-color;

  border-radius: 0.4rem;
  width: 200px;
  max-width: 70%;
  min-width: 150px;
  height: 2rem;
  padding: 1px 0.8rem;

  display: flex;
  flex-direction: row;
  cursor: pointer;
  @media only screen and (max-width: 600px) {
    width: 100%;
  }

  input {
    border: none;
    width: 100%;
    height: 100%;
    padding: 0;
    font-family: $font-family;
    font-size: 12px;
    background-color: $background-color;
    color: $black-color;

    &:focus {
      outline: none;
    }
  }

  .icon-container {
    display: grid;
    align-items: center;
    .s-btn {
      cursor: pointer;
      padding: 6px;
    }
  }
}
</style>
