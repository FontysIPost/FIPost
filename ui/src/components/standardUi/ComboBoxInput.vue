<template>
  <div class="combobox-container">
    <p class="header">{{ label }}</p>
    <div @click="toggle()" :class="valid ? 'custom-select' : 'custom-select error'">
      <div class="selected" :class="{ open: open }" >
        {{ selectedRef.name }}
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
        <div
          v-for="option in options"
          :key="option.id"
          @click="onChange(option)"
        >
          {{ option.name }}
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import { Prop, Watch } from "vue-property-decorator";
import SelectOption from "@/classes/helpers/SelectOption";

@Options({
  props: {
    placeholder: String,
    options: Array as () => Array<SelectOption>,
    label: String,
    valid: Boolean,
  },
  emits: ["select-change"],
})
export default class ComboBoxInput extends Vue {
  @Prop()
  private selectedOption?: SelectOption;

  @Watch("selectedOption", { immediate: true, deep: true })
  onPropertyChanged(value: SelectOption, oldValue: SelectOption) {
    if (value) {
      this.selectedRef.name = value.name;
    }
  }

  private selectedRef: SelectOption = new SelectOption("", "");

  private placeholder!: string;
  private options!: Array<SelectOption>;
  private open: Boolean = false;
  private valid: Boolean = true;

  private onChange(option: SelectOption): void {
    this.selectedRef = option;
    this.$emit("select-change", this.selectedRef);
  }

  // mounted() {
  //   if(this.selectedRef.name == "") {
  //     this.selectedRef.name = this.placeholder;
  //   }
  // }

  private toggle() {
    this.open = !this.open;
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.combobox-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-between;
  width: 440px;
  margin-bottom: 0.6rem;
  margin-top: 0.6rem;
  max-width: 100%;

  .header {
    min-width: 200px;
    text-align: left;
  }
}

.header {
  flex-basis: 100px;
}

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
  margin: auto 0;
  padding: 1px 0.8rem;

  display: flex;
  flex-direction: row;
  cursor: pointer;
  text-align: left;

  @media only screen and (max-width: 600px) {
    width: 150px;
  }
}

.custom-select .selected {
  cursor: pointer;
  user-select: none;
  width: 90%;
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

.error {
  background: #ffc9cf;
}
</style>