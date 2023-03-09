<template>
  <div>
    <div class="wrapper">
      <LoadingIcon v-if="loading" />
      <div v-else>
        <div class="container-subheader">{{ title }}</div>
        <CBSearchSuggestion
          @selectChanged="assignCityToRoom"
          @change="buildingcbx.input = ''"
          :options="cities"
          :selectedOption="selectedCityOption"
          label="Stad:"
          :valid="cityValid"
        />
        <CBSearchSuggestion
          @selectChanged="assignBuildingToRoom"
          :options="buildings"
          :selectedOption="selectedBuildingOption"
          label="Gebouw:"
          :valid="buildingValid"
        />
        <InputField
          label="Ruimte:"
          v-model:input="room.Name"
          :valid="nameValid"
          @update:input="nameChanged"
        />
        <h4 class="error-text" v-if="error.length > 0">
          {{ error }}
        </h4>

        <div class="action-container">
          <SmallBtnFinish
            v-if="roomId"
            text="Delete"
            :red="true"
            @btn-clicked="deleteLocation()"
            :isLoading="loadDeleteRequest"
            :disabled="loadPostRequest"
          />
          <SmallBtnFinish
            text="Bevestigen"
            @btn-clicked="addRoom"
            :isLoading="loadPostRequest"
            :disabled="loadDeleteRequest"
          />
          <transition name="modal" v-if="showModal" close="showModal = false">
            <link-or-stay-modal link="locaties" @close="showModal = false" />
          </transition>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";

import InputField from "@/components/standardUi/InputField.vue";
import SmallBtnFinish from "@/components/standardUi/SmallBtnFinish.vue";
import RoomRequest from "@/classes/requests/RoomRequest";
import CBSearchSuggestion from "@/components/standardUi/CBSearchSuggestions.vue";
import { roomService } from "@/services/locatieService/roomservice";
import Building from "@/classes/Building";
import { buildingService } from "@/services/locatieService/buildingservice";
import City from "@/classes/City";
import { cityService } from "@/services/locatieService/cityservice";
import LinkOrStayModal from "@/components/standardUi/LinkOrStayModal.vue";
import { getCurrentInstance } from "@vue/runtime-core";
import SelectOption from "@/classes/helpers/SelectOption";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { AxiosError } from "axios";
import CityRequest from "@/classes/requests/CityRequest";

@Options({
  components: {
    CBSearchSuggestion,
    InputField,
    SmallBtnFinish,
    LinkOrStayModal,
    LoadingIcon,
  },
  emits: ["location-changed"],
})
export default class AddRoom extends Vue {
  @Prop()
  private roomId: string = "";

  @Prop()
  private cityId: string = "";

  @Prop()
  private title: string = "Voeg een nieuwe ruimte toe";

  private emitter =
    getCurrentInstance()?.appContext.config.globalProperties.emitter;
  private loading: boolean = true;
  private loadPostRequest: boolean = false;
  private loadDeleteRequest: boolean = false;

  private showModal: boolean = false;
  private buildings: Array<SelectOption> = new Array<SelectOption>();
  private allBuildings: Array<Building> = new Array<Building>();
  private allCities: Array<City> = new Array<City>();
  private cities: Array<SelectOption> = new Array<SelectOption>();
  private selectedBuildingOption: SelectOption = new SelectOption("", "");
  private selectedCityOption: SelectOption = new SelectOption("", "");

  private room: RoomRequest = new RoomRequest("", "");
  private city: CityRequest = new CityRequest("");
  private nameValid: boolean = true;
  private buildingValid: boolean = true;
  private cityValid: boolean = true;
  private error: string = "";
  private cityChanged: boolean = false;

  assignBuildingToRoom(option: SelectOption): void {
    this.room.BuildingId = option.id;
  }

  assignCityToRoom(option: SelectOption): void {
    if (option.id == "") {
      this.getAllByCity(this.cityId);
    }
    else {
      this.selectedBuildingOption = new SelectOption(
          '',''
        );
      this.cityId = option.id;
      this.getAllByCity(this.cityId);
    }
  }

  async addRoom() {
    this.loadPostRequest = true;
    if (this.validate()) {
      if (this.roomId) {
        roomService
          .update(this.room, this.roomId)
          .then(() => {
            this.room.Name = "";
            this.loadPostRequest = false;
            this.$emit("location-changed");
          })
          .catch((err: AxiosError) => {
            this.loadPostRequest = false;
            this.error = err.response?.data;
          });
      } else {
        roomService
          .post(this.room)
          .then(() => {
            this.room.Name = "";
            this.showModal = true;
            this.loadPostRequest = false;
          })
          .catch((err: AxiosError) => {
            this.loadPostRequest = false;
            this.emitter.emit("err", err);
          });
      }
    } else {
      this.loadPostRequest = false;
    }
  }


  deleteLocation() {
    if (confirm("Weet je zeker dat je deze locatie wilt verwijderen?")) {
      this.loadDeleteRequest = true;
      roomService
        .delete(this.roomId)
        .then(() => {
          this.$emit("location-changed");
          this.loadDeleteRequest = false;
        })
        .catch((err: AxiosError) => {
          this.emitter.emit("err", err);
          this.loadDeleteRequest = false;
        });
    }
  }

  private validate(): boolean {
    this.nameValid = this.room.Name.length > 0;
    this.buildingValid =
      this.allBuildings.find(
        (building) => building.id == this.room.BuildingId
      ) != null;

    if (!this.nameValid) {
      this.error = "Niet alle velden zijn ingevoerd";
      return false;
    }

    if (!this.cityValid) {
      this.error = "deze stad bestaat niet";
      return false;
    }

    if (!this.buildingValid) {
      this.error = "dit gebouw bestaat niet";
      return false;
    }
    this.error = "";
    return true;
  }

  nameChanged(input: string): void {
    this.nameValid = this.room.Name.length > 0;
    this.error = "";
  }

  private getAllByCity(cityId) {
    if (this.cityChanged == false) {
      this.cityChanged = true;
      buildingService
        .getAllByCity(cityId)
        .then((res) => {
          this.allBuildings = [];
          this.buildings = [];
          this.allBuildings = res;
          this.allBuildings.forEach((building) =>
            this.buildings.push(new SelectOption(building.id, building.name))
          );
          this.loading = false;
          this.cityChanged = false;
        })
        .catch((err: AxiosError) => {
          this.loading = false;
          this.cityChanged = false;
          this.emitter.emit("err", err);
        });
    }
  }

  async mounted() {
    if (this.roomId) {
      roomService.getById(this.roomId).then((res) => {
        this.room.Name = res.name;
        this.room.BuildingId = res.building.id;
        this.cityId = res.building.address.city.id;
        this.getAllByCity(this.cityId);
        /* automatically fills the fields of city and building with data*/
        this.selectedBuildingOption = new SelectOption(
          res.building.id,
          res.building.name
        );
        this.selectedCityOption = new SelectOption(
          res.building.address.city.id,
          res.building.address.city.name
        )
      });
    }

    cityService
      .getAll()
      .then((res) => {
        this.allCities = res;
        this.allCities.forEach((res) =>
          this.cities.push(new SelectOption(res.id, res.name))
        );
        this.loading = false;
      })
      .catch((err: AxiosError) => {
        this.loading = false;
        this.emitter.emit("err", err);
      });

      
  }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";

.wrapper {
  margin-top: 1em;
}
</style>