<template>
  <!-- The `label` is attached to the hidden file input -->
    <label class="qr"  for="cameraFileInput">
      <!-- The hidden file `input` for opening the native camera -->
      <input
        id="cameraFileInput"
        ref="input"
        type="file"
        accept="image/*"
        capture="environment"
      />
    </label>
</template>

<script lang="ts">
import { defineComponent } from "vue";

const Search = defineComponent({
  mounted() {
    (this.$refs.input as HTMLInputElement).click()
  }
});

export default Search;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import "@/styling/main.scss";
.qr {
  background-image: url("../assets/QRIcoon.png");
  background-size: contain;
  width: 30px;
  height: 30px;
  margin: auto 15px;
  cursor: pointer;
}

#cameraFileInput {
  display: none;
}

</style>
