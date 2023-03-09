<template>
  <div class="input-container">
    <p class="header">{{ label }}</p>
    <input
      :type="textType"
      :class="cssClass"
      :value="input"
      @keyup="$emit('update:input', $event.target.value)"
    />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Options({
  emits: ["update:input"],
})
export default class InputField extends Vue {
  @Prop() public label!: String;
  @Prop() public input!: String;
  @Prop() public color!: String;
  @Prop() public valid: Boolean = true; // Default true

  @Prop() public textType!: String;

  public get cssClass(): string {
    if (this.valid) {
      return "input";
    }
    return "input input-error";
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.input-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-between;
  width: 440px;
  margin-bottom: 0.6rem;
  margin-top: 0.6rem;
  font-weight: normal;
  max-width: 100%;

  .header {
    min-width: 200px;
    text-align: left;
  }
}

.header {
  flex-basis: 100px;
}

.input {
  border-radius: 0.4rem;
  border-width: 0;
  background: $background-color;
  padding-left: 0.8rem;
  padding-right: 0.8rem;
  height: 2rem;
  margin: auto 0;
  max-width: 70%;
  width: 200px;
  min-width: 150px;

  @media only screen and (max-width: 600px) {
    width: 150px;
    left: 0px;
    top: 0px;
  }
}

.input-error {
  background: #ffc9cf;
}

.input:focus {
  outline: none;
  border: #999999 solid 2px;
}

.input-error:focus {
  border: #ff8ca1 solid 2px;
}
</style>
