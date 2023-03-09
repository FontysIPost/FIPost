<template>
    <AddCity v-if="ColumnType == city" :cityId="cityId" @location-changed="ReloadTable" :title="updateCityTitle"/>
    <AddBuilding v-if="ColumnType == building" :buildingId="buildingId" @location-changed="ReloadTable" :title="updateBuildingTitle"/>
    <AddRoom  v-if="ColumnType == room" :roomId="roomId" @location-changed="ReloadTable" :title="updateRoomTitle"/>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import AddCity from "@/components/locationRegistration/AddCity.vue";
import { Emit, Prop } from "vue-property-decorator";
import AddBuilding from "@/components/locationRegistration/AddBuilding.vue";
import AddRoom from "@/components/locationRegistration/AddRoom.vue";
import { ColumnType } from "@/classes/table/ColumnType";

function EnumProp(d: string | number, e: Record<any, any>) {
  // <- Record is changed
  return {
    default: d,
    validator: (v) => e[v] !== undefined,
  };
}

@Options({
  components: {
    AddCity,
    AddBuilding,
    AddRoom
  },
  emits: [
    "reload-table"
  ]
})
export default class LocationInfo extends Vue {
  private city: ColumnType = ColumnType.CITY;
  private building: ColumnType = ColumnType.BUILDING;
  private room: ColumnType = ColumnType.ROOM;

  private updateCityTitle: string = "Wijzig een stad";
  private updateBuildingTitle: string = "Wijzig een gebouw";
  private updateRoomTitle: string = "Wijzig een kamer";

  @Prop()
  public cityId: string = "";

  @Prop()
  public buildingId: string = "";

  @Prop()
  public roomId: string = "";

  @Prop(EnumProp(ColumnType.CITY, ColumnType))
  ColumnType!: ColumnType;

  ReloadTable(): void {
    this.$emit("reload-table");
  }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";
</style>