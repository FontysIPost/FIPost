<template>
  <div class="cd-container">
    <div class="container-subheader-small">Ontvanger</div>
    <div v-if="person">
      <div class="cd-item">{{ person.name }}</div>
      <div class="cd-item">{{ person.email }}</div>
    </div>
    <div v-else>
      <div class="cd-item">De ontvanger kan niet worden opgehaald</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Person from "@/classes/Person";

@Options({
  props: {
    person: Object,
  },
})
export default class PersonDetails extends Vue {
  private person!: Person;
}
</script>


<style scoped lang="scss">
@import "@/styling/main.scss";

.cd-container {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  row-gap: 0.2em;
}

.cd-item {
  font-size: 16px;
  @media only screen and (max-width: 600px) {
    font-size: 12px;
  }
}
</style>