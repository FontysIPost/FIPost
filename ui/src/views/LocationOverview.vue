<template>
  <div class="component">
    <btn-back />
    <h1>Locatie Overzicht</h1>
    <SearchContainerLocation />
    <location-overview-table />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import BtnBack from "@/components/standardUi/BtnBack.vue";
import LocationOverviewTable from "@/components/location/LocationOverviewTable.vue";
import SearchContainerLocation from "@/components/SearchContainerLocation.vue";

@Options({
  components: {
    BtnBack,
    LocationOverviewTable,
    SearchContainerLocation
  },
})
export default class LocatieOverzicht extends Vue {}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";
.component {
  width: 60%;
}
@media only screen and (max-width: 930px) {
  .component {
    width: 95%;
  }
}
</style>