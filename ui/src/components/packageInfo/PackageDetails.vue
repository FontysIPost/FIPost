<template>
  <div>
    <div class="package-details">
      <div class="container container-header">Pakketgegevens</div>

      <div class="details-wrapper">
        <div class="pd-content">
          <div class="container-subheader-small">Pakketnaam</div>
          <div class="pd-item">{{ packageM.name }}</div>
        </div>

        <PersonDetails :person="packageM.receiver" />

        <div class="pd-container">
          <div class="pd-content">
            <div class="container-subheader-small">Afzender</div>
            <div class="pd-item">
              {{
                packageM.sender.length > 1
                  ? packageM.sender
                  : "De afzender kan niet worden opgehaald"
              }}
            </div>
          </div>
          <div class="sd-img">
            <img alt="BoxQR" src="@/assets/BoxQR.png" />
          </div>
        </div>

        <div>
          <StatusBadge
            :completeText="getDateString()"
            inCompleteText="Onbekend"
            :complete="packageM.tickets.length >= 0"
          />
          <RoomDetails
            :room="
              packageM.tickets[lastTicketIndex]
                ? packageM.tickets[lastTicketIndex].location
                : null
            "
            title="Binnen gekomen bij"
          />
        </div>

        <div>
          <StatusBadge
            completeText="Afgeleverd"
            inCompleteText="In behandeling"
            :complete="packageM.routeFinished"
          />
          <RoomDetails
            :room="packageM.collectionPoint"
            title="Af te halen op"
          />
        </div>
        
      </div>
        
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import PersonDetails from "@/components/packageInfo/PersonDetails.vue";
import RoomDetails from "@/components/packageInfo/RoomDetails.vue";
import StatusBadge from "@/components/standardUi/StatusBadge.vue";
import Room, { roomHelper } from "@/classes/Room";
import Package from "@/classes/Package";
import { dateConverter } from "@/classes/helpers/DateConverter";


@Options({
  props: {
    packageM: Object,
  },
  components: {
    PersonDetails,
    RoomDetails,
    StatusBadge,
  },
})
export default class PackageDetails extends Vue {
  private packageM: Package = new Package();
  private lastTicketIndex: number = 0;
  private createdAtRoom: Room = roomHelper.getEmptyRoom();
  private deliveryRoom: Room = roomHelper.getEmptyRoom();

  async mounted() {
    if (this.packageM.tickets != null) {
      this.lastTicketIndex = this.packageM.tickets.length - 1;
    }
    else{
      this.lastTicketIndex = -1;
    }
  }

  private getDateString() {
    if (this.lastTicketIndex != -1) {
      return dateConverter.getDateString(
        this.packageM.tickets[this.lastTicketIndex].finishedAt
      );
    } else {
      return "Onbekend";
    }
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.package-details {
  background-color: white;
  border-radius: $border-radius;
  box-shadow: $shadow;
  text-align: left;

  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  row-gap: 1em;

  padding: 3em;
  @media only screen and (max-width: 600px) {
    padding: 2em;
  }
}

.pd-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  column-gap: 0.5em;
}

.pd-content {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  row-gap: 0.2em;
}

.pd-item {
  font-size: 16px;
  @media only screen and (max-width: 600px) {
    font-size: 12px;
  }
}

.sd-img {
  width: 3em;
  position: relative;
}

img {
  width: 100px;
  position: absolute;
  left: -50px;
  top: -50px;
  @media only screen and (max-width: 600px) {
    width: 3em;
    left: 0px;
    top: 0px;
  }
}

.details-wrapper {
  display: flex;
  flex-direction: column;
  row-gap: 1em;
}
</style>