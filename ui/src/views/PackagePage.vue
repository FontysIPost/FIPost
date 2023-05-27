<template>
    <div class="component">
        <btn-back class="back-button"/>
        <LoadingIcon v-if="isLoading" />
        <div class="page" v-else>
            <div class="pi-item-container">
                <CreateTicket @new-ticket="reloadPage" :fPackage="packageM" />
                <RoutePackageInfo :tickets="packageM.tickets" />
            </div>
            <div class="pi-item-container">
                <PrintQR :packageId="packageId" :receiversAddress1="buildReceiversAddress1()" :receiversAddress2="buildReceiversAddress2()"
                         :receiversName="buildReceiversName()" :sendersName="buildSendersName()"/>
                <PackageDetails :packageM="packageM" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import PackageDetails from "@/components/packageInfo/PackageDetails.vue";
import PrintQR from "@/components/PrintQR.vue";
import RoutePackageInfo from "@/components/route/RoutePackageInfo.vue";
import CreateTicket from "@/components/route/CreateTicket.vue";
import BtnBack from "@/components/standardUi/BtnBack.vue";
import Package from "@/classes/Package";
import { AxiosError } from "axios";
import { pakketService } from "@/services/pakketService/pakketservice";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";

@Options({
  components: {
    BtnBack,
    PackageDetails,
    PrintQR,
    RoutePackageInfo,
    CreateTicket,
    LoadingIcon,
  },
})
export default class PackagePage extends Vue {
  private packageId: String = "";
  private packageM: Package = new Package();

  private isLoading: Boolean = true;
  private error: Boolean = false;

  private ticketKey: number = 0;
  private receiversAddressData1: String = "";
  private receiversAddressData2: String = "";
  private receiversNameData: String = "";
  private sendersNameData; String = "";

  private async reloadPage() {
    this.isLoading = true;
    await this.getPackage();
  }

  async mounted() {
    this.packageId = this.$router.currentRoute.value.params.id.toString();
    await this.getPackage();
  }

  private async getPackage() {
    pakketService
      .get(this.packageId)
      .then((res) => {
        this.packageM = res;
        this.isLoading = false;
      })
      .catch((err: AxiosError) => {
        this.error = true;
        this.isLoading = false;
      });
  }

  buildReceiversAddress1() {
    if (this.packageM.collectionPoint) {
      this.receiversAddressData1 =
        this.packageM.collectionPoint.building.address.street.toString() +
        " " +
        this.packageM.collectionPoint.building.name +
        ", " +
        this.packageM.collectionPoint.name; 
      return this.receiversAddressData1;
    }   
    return "Er ging iets mis bij het ophalen van de locatie";
  }
  
 buildReceiversAddress2() {
    if (this.packageM.collectionPoint) {
      this.receiversAddressData2 =
        this.packageM.collectionPoint.building.address.postalCode +
        ", " +
        this.packageM.collectionPoint.building.address.city.name;
      return this.receiversAddressData2;
    }   
    return "Er ging iets mis bij het ophalen van de locatie";
  }

  buildReceiversName() {
    if (this.packageM.collectionPoint) {
      this.receiversNameData =
        this.packageM.receiver.name.toString()
      return this.receiversNameData;
    }
    return "Er ging iets mis bij het ophalen van de naam";
  }

   buildSendersName() {
    if (this.packageM.collectionPoint) {
      this.sendersNameData =
        this.packageM.sender.toString()
      return this.sendersNameData;
    }
    return "Er ging iets mis bij het ophalen van de naam";
  }
}


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
@import "@/styling/main.scss";
.component {
  width: 70%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .back-button {
        margin-right: 80%;
    }
}
@media only screen and (max-width: 1300px) {
    .component {
        width: 100%;

    }
}

@media only screen and (max-width: 700px) {
    justify-content: center;
    row-gap: 15px;
    width: 100%;
    .component {
        width: 100%;

    }
}
.page {
  justify-content: center;
  width: 100%;
  display: flex;
  flex-wrap: wrap;
  column-gap: 1em;

  
  .pi-item-container {
    width: 40%;
    display: flex;
    flex-direction: column;
    row-gap: 15px;
  }

  @media only screen and (max-width: 1300px) {
    justify-content: center;
    row-gap: 15px;
    .pi-item-container {
      width: 70%;
    
    }
  }

  @media only screen and (max-width: 700px) {
    justify-content: center;
    row-gap: 15px;
    width: 100%;
      .pi-item-container {
          width: 100%;

      }
  }
}

</style>