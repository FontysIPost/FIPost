<template>
  <div class="checkbox-container" @click="onClick()">
    <div :class="checked ? 'checkbox checked' : 'checkbox'">
      <font-awesome-icon
        v-if="checked"
        class="checkmark"
        icon="check"
      />
    </div>
    <label class="checkbox-text">{{ text }}</label>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    text: String,
  },
  emits: ['checked']
})
export default class CheckBox extends Vue {
  private checked: Boolean = false;

  private onClick(): void {
    this.checked = !this.checked;
    this.$emit("checked", this.checked);
  }
}
</script>


<style scoped lang="scss">
@import "@/styling/main.scss";

.checkbox-container {
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  max-width: 200px;
  column-gap: 2em;
  @media only screen and (max-width: 600px) {
    font-size: 12px;
  }
}

.checkbox {
  width: 16px;
  height: 16px;
  border: 1px solid $gray-color;
  border-radius: 0.3em;
  transition: all linear 0.3s;
  display: inline-block;
}

.checked {
  background-color: $fontys-purple;
}

.checkmark {
  width: 12px;
  height: 12px;
  text-align: center;
  margin: 2px;
  color: $background-color;
}
</style>