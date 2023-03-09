<template>
  <div class="cbsearch-container">
    <p v-if="!custom" class="header hw">{{ label }}</p>
    <slot class="hw" v-else></slot>

    <div :class="valid ? 'custom-select' : 'custom-select error'">
      <input
        type="text"
        :class="valid ? 'input' : 'input error'"
        v-on:input="updateSuggestions()"
        v-model="selectedRef.name"
        v-on:blur="loseFocus()"
        v-on:focus="startFocus()"
      />


      <div class="items" :class="{ selectHide: !open }">
        <div
          v-for="suggestion in suggestions"
          :key="suggestion"
          @mousedown="onChange(suggestion)"
        >
          {{ suggestion.name }}
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
    custom: Boolean,
  },
  emits: ["select-changed"],
})
export default class CBSearchSuggestions extends Vue {
  @Prop()
  private selectedOption?: SelectOption;

  @Watch("selectedOption", { immediate: true, deep: true })
  onPropertyChanged(value: SelectOption, oldValue: SelectOption) {
    if (value) {
      this.selectedRef.name = value.name;
    }
  }

  private selectedRef: SelectOption = new SelectOption("", "");

  private placeholder: string = "";
  private suggestions: Array<SelectOption> = [];
  private options!: Array<SelectOption>;
  private open: Boolean = false;
  private valid: Boolean = true;

  private onChange(option: SelectOption): void {
    this.selectedRef = new SelectOption(option.id, option.name);
    this.open = false;
    this.$emit("select-changed", this.selectedRef);
  }

  private updateSuggestions() {
    this.selectedRef.id = "";
    this.$emit("select-changed", this.selectedRef);
    this.suggestions = this.options.filter((el: SelectOption) =>
      el.name
        .toLocaleLowerCase()
        .includes(this.selectedRef.name.toLocaleLowerCase())
    );
    this.open = true;
  }

  private loseFocus() {
    this.$emit("select-changed", this.selectedRef);
    this.open = false;
  }

  private startFocus() {
    this.suggestions = this.options;
    this.open = true;
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.cbsearch-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}

.header {
  min-width: 200px;
  text-align: left;
}

.custom-select {
  border: none;
  font-family: $font-family;
  font-size: 12px;
  background-color: $background-color;
  color: $black-color;

  border-radius: 0.4rem;
  border-width: 0;
  width: 200px;
  max-width: 70%;
  min-width: 150px;
  height: 2rem;
  margin: auto 0;
  padding: 1px 0.8rem;

  display: flex;
  flex-direction: row;

  @media only screen and (max-width: 600px) {
    width: 150px;
  }
}

.input {
  user-select: none;
  border: 0px;
  width: 100%;
  height: 100%;
  background-color: $background-color;
  padding: 0px 0px;
}

.error {
  background: #ffc9cf;
}

.input:focus {
  outline: none;
  border: 0px;
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

.cbsearch-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-between;
  width: 440px;
  margin-bottom: 0.6rem;
  margin-top: 0.6rem;
  max-width: 100%;
}
</style>