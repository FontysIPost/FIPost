<template>
  <div :class="isSmall ? 'loader small' : 'loader'"></div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    isSmall: Boolean,
  },
})
export default class LoadingIcon extends Vue {
  isSmall: Boolean = false;
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.loader {
  border: 16px solid #e3e3e3; /* Light grey */
  border-top: 16px solid $modern-purple-color; /* Blue */
  border-radius: 50%;
  width: 120px;
  height: 120px;
  animation: spin 2s linear infinite;
  position: relative;
  margin: auto;
  margin-top: 50px;
}

.small {
  width: 12px;
  height: 12px;
  border: 5px solid #e3e3e3; /* Light grey */
  border-top: 5px solid $modern-purple-color; /* Blue */
  margin-top: 0px;
  position: inherit;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>