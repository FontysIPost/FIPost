<template>
  <div class="custom-select" @click="toggle()">
    <div class="selected" :class="{ open: open }" >
      {{ selectedOption }}
    </div>

    <div>
      <font-awesome-icon v-if="open" icon="sort-up" size="2x" class="arrow" />
      <font-awesome-icon
        v-else
        icon="sort-down"
        size="2x"
        class="arrow"
        style="margin-top: -10px"
      />
    </div>

    <div class="items" :class="{ selectHide: !open }">
      <div v-for="option in options" :key="option" @click="onChange(option)">
        {{ option }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";

@Options({
  props: {
    placeholder: String,
    options: Array as () => Array<String>,
  },
  emits: ["select-change"],
})
export default class ComboBox extends Vue {
  private selectedOption: String = "";
  private placeholder!: String;
  private open: Boolean = false;

  private onChange(option: String): void {
    this.selectedOption = option;
    this.$emit("select-change", this.selectedOption);
  }

  mounted() {
    this.selectedOption = this.placeholder;
  }
  private toggle() {
    this.open = !this.open;
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.custom-select {
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
}

.custom-select .selected {
  padding-left: 1em;
  cursor: pointer;
  user-select: none;
  width: 80%;
  height: 100%;
  padding-top: 6px;
}

.items {
  border-radius: $small-border-radius;
  box-shadow: $shadow;
  overflow: hidden;
  position: absolute;
  background-color: $background-color;
  color: $black-color;

  width: 200px;
  max-width: 70%;
  min-width: 150px;
  z-index: 1;

  margin: auto 0;
  padding: 1px 0.8rem;
  margin-top: 33px;
  margin-left: -11px;

  max-height: 150px;
  overflow-y: auto;

  @media only screen and (max-width: 600px) {
    width: 150px;
  }
}

.custom-select .items div {
  color: $black-color;
  padding: 0.5em 2em 0.5em 25px;
  cursor: pointer;
  user-select: none;
}

.custom-select .items div:hover {
  background-color: #d8d8da;
}

.selectHide {
  display: none;
}

.arrow {
  color: $fontys-purple;
  position: absolute;
  padding-top: 8px;
}
</style>