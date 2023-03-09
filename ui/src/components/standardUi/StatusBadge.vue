<template>
  <div
    :class="complete ? 'badge complete' : 'badge incomplete'"
  >
    {{ complete ? completeText : inCompleteText }}
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    completeText: String,
    inCompleteText: String,
    complete: Boolean,
  },
})
export default class StatusBadge extends Vue {}

</script>


<style scoped lang="scss">
@import "@/styling/main.scss";

.badge {
  width: 120px;
  height: 25px;

  border-radius: 5px;

  font-weight: bold;
  font-size: 12px;

  text-align: center;
  line-height: 25px;
  overflow: hidden;

    @media only screen and (max-width: 600px) {
    font-size: 12px;
    height: 20px;
    line-height: 20px;
  }
  margin-bottom: 5px;
}

.complete {
  background: $green-color;
  color: $text-green;
}

.incomplete {
  background: $background-yellow;
  color: $text-yellow;
}
</style>