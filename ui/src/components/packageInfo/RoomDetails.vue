<template>
  <div>
    <div v-if="room" class="cpd-container">
      <div class="container-subheader-small">
        {{ title }}
      </div>
      <div class="cpd-item">
        {{ room.building.address.city.name + " " + room.building.name }}
      </div>
      <div class="cpd-item">{{ room.building.name + " " + room.name }}</div>
      <div class="cpd-item">
        {{
          room.building.address.street +
          " " +
          room.building.address.number + room.building.address.addition +
          ", " +
          room.building.address.postalCode
        }}
      </div>
    </div>
    <div v-else class="cpd-container">
      <div class="container-subheader">
        {{ title }}
      </div>
      <div class="cpd-item">Deze locatie kon niet worden opgehaald. </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Room from "@/classes/Room";

@Options({
  props: {
    room: Object,
    title: String,
  },
})
export default class RoomDetails extends Vue {
  public room!: Room;
}
</script>



<style scoped lang="scss">
@import "@/styling/main.scss";

.cpd-container {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  row-gap: 0.2em;
}

.cpd-item {
  width: 50%;
  font-size: 16px;
  @media only screen and (max-width: 600px) {
    font-size: 12px;
  }
}
</style>