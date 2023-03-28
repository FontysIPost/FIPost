<template>
  <div class="wrapper">
    <LoadingIcon v-if="isLoading" />
    <div v-else>
      <div class="container-subheader">{{ title }}</div>
      <CBSearchSuggestion
          @selectChanged="assignCityToRoom"
          :options="cities"
          :selectedOption="selectedCityOption"
          label="Stad:"
          :valid="cityValid"
        />
      <InputField
        label="Stad:"
        v-model:input="city.Name"
        :valid="nameValid"
        @update:input="nameChanged"
      />

      <h4 class="error-text" v-if="error.length > 0">
        {{ error }}
      </h4>

      <div class="action-container">
        <SmallBtnFinish
          v-if="cityId"
          text="Delete"
          :red="true"
          @btn-clicked="deleteLocation()"
          :isLoading="loadDeleteRequest"
          :disabled="loadPostRequest"
        />
        <SmallBtnFinish
          text="Bevestigen"
          @btn-clicked="addCity"
          :isLoading="loadPostRequest"
          :disabled="loadDeleteRequest"
        />
        <transition name="modal" v-if="showModal" close="showModal = false">
          <link-or-stay-modal link="locaties" @close="showModal = false" />
        </transition>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { Prop } from "vue-property-decorator";
import InputField from "@/components/standardUi/InputField.vue";
import SmallBtnFinish from "@/components/standardUi/SmallBtnFinish.vue";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import CityRequest from "@/classes/requests/CityRequest";
import { cityService } from "@/services/locatieService/cityservice";
import LinkOrStayModal from "@/components/standardUi/LinkOrStayModal.vue";
import { getCurrentInstance } from "@vue/runtime-core";
import { AxiosError } from "axios";

@Options({
  components: {
    InputField,
    SmallBtnFinish,
    LinkOrStayModal,
    LoadingIcon,
  },
  emits: ["location-changed"],
})
export default class AddCity extends Vue {
  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  private city: CityRequest = new CityRequest("");
  private showModal: boolean = false;
  private nameValid: boolean = true;
  private error: string = "";
  private isLoading: boolean = false;
  private loadPostRequest: boolean = false;
  private loadDeleteRequest: boolean = false;

  @Prop()
  public cityId: string = "";

  @Prop()
  public title: string = "Voeg een stad toe";

  async addCity() {
    this.loadPostRequest = true;
    if (this.validate()) {
      if (this.cityId) {
        // Update.
        cityService
          .updateCity(this.cityId, this.city)
          .then(() => {
            this.city.Name = "";
            this.loadPostRequest = false;
            this.$emit("location-changed");
          })
          .catch((err: AxiosError) => {
            this.loadPostRequest = false;
            this.error = err.response?.data;
          });
      } else {
        cityService
          .post(this.city)
          .then(() => {
            this.city.Name = "";
            this.showModal = true;
            this.loadPostRequest = false;
          })
          .catch((err: AxiosError) => {
            this.loadPostRequest = false;
            this.emitter.emit("err", err);
          });
      }
    }
    else{
      this.loadPostRequest = false;
    }
  }

  capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

  private validate(): boolean {
    this.nameValid = this.city.Name.length > 0;
    if (this.nameValid) {
      this.error = "";
      return true;
    } else {
      this.error = "Niet alle velden zijn ingevoerd";
      return false;
    }
  }

  deleteLocation() {
    if (confirm("Weet je zeker dat je deze locatie wilt verwijderen?")) {
      this.loadDeleteRequest = true;
      cityService
        .deleteCity(this.cityId)
        .then(() => {
          this.loadDeleteRequest = false;
          this.$emit("location-changed");
        })
        .catch((err: AxiosError) => {
          this.loadDeleteRequest = false;
          this.emitter.emit("err", err);
        });
    }
  }
  nameChanged(input: string): void {
    this.city.Name = this.capitalizeFirstLetter(this.city.Name);
    this.nameValid = this.city.Name.length > 0;
    this.error = "";
  }

  async mounted() {
    if (this.cityId) {
      this.isLoading = true;
      console.log("getcity" + this.cityId);
      cityService.getById(this.cityId).then((res) => {

        this.city = new CityRequest(res.name);
        this.isLoading = false;
      });
    }
  }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";

.wrapper {
  margin-top: 1em;
}
</style>
