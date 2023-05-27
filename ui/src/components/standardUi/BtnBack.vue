<template>
  <div class="btn-back" @click="onClick()">
    {{ text }}
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    text: String,
  },
})
class BtnBack extends Vue {
  text: string = "Terug";

  onClick(): void {
    this.$router.go(-1);
  }
}

export default BtnBack;
</script>


<style scoped lang="scss">
@import "@/styling/main.scss";

.btn-back {
  width: 0;
  height: 40px;

  background: transparent;
  color: #000000;

  border-radius: 10px;
  border: 0;

  font-weight: bold;
  font-size: 16px;
  cursor: pointer;
}
</style>