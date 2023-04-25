<template>
  <div class="align-left">
    <BtnBack class="button-back" />
    <div class="component-container">
      <h1>{{ !overview ? "Pakket Registreren" : "Overzicht" }}</h1>
      <LoadingIcon v-if="loadPers || loadRoom" />
      <div v-else>
        <div v-if="!overview">
          <div class="group">
            <h3>Pakketinformatie</h3>
            <InputField
              label="Afzender:"
              v-model:input="fpackage.Sender"
              :valid="v.SenderHasValue"
              @update:input="v.SenderHasValue = true"
            />
            <InputField
              label="Pakketnaam:"
              v-model:input="fpackage.Name"
              :valid="v.NameHasValue"
              @update:input="v.NameHasValue = true"
            />
            <CBSearchSuggestions
              :options="receivers"
              label="Ontvanger:"
              @select-changed="receiverChanged"
              :selectedOption="selectedReceiver()"
              :valid="v.ReceiverIdValid"
            />
            <CBSearchSuggestions
              :options="rooms"
              label="Afhaalpunt:"
              @selectChanged="collectionPointChanged"
              :selectedOption="selectedCollectionPoint()"
              :valid="v.CollectionPointIdValid"
            />
          </div>
          <div class="group">
            <h3>Registratieinformatie</h3>
            <CBSearchSuggestions
              :options="receivers"
              label="Registreerder:"
              @selectChanged="registratorChanged"
              :valid="v.CreatedByPersonIdValid"
              :selectedOption="selectedRegistrator()"
            />
            <CBSearchSuggestions
              :options="rooms"
              label="Aangekomen op:"
              @selectChanged="registerLocationChanged"
              :valid="v.CreatedAtLocationIdValid"
              :selectedOption="selectedCreatedAtLocation()"
            />
          </div>

          <ul v-if="errorText">
            <li v-for="e in error" :key="e" class="error-text">{{ e }}</li>
          </ul>
        </div>

        <div v-else>
          <div class="container">
            <div class="group">
              <h2>Pakketinformatie</h2>
              <p><strong>Afzender:</strong> {{ fpackage.Sender }}</p>
              <p><strong>Pakketnaam:</strong> {{ fpackage.Name }}</p>
              <p><strong>Ontvanger:</strong> {{ receiverName }}</p>
              <p><strong>Afhaalpunt:</strong> {{ collectionPointName }}</p>
            </div>
            <hr />
            <div class="group">
              <h2>Registratieinformatie</h2>
              <p><strong>Registreerder:</strong> {{ registratorName }}</p>
              <p><strong>Ontvangslocatie:</strong> {{ createdAtPointName }}</p>
            </div>
          </div>
        </div>
        <SmallBtnFinish
          :text="btnText"
          :red="btnText == 'Vorige' ? true : false"
          v-on:click="toggleStep"
          :disabled="loadPostRequest"
        />
        <SmallBtnFinish
          text="Bevestigen"
          @btn-clicked="registerPackage"
          v-if="overview"
          :isLoading="loadPostRequest"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import BtnBack from "@/components/standardUi/BtnBack.vue";
import InputField from "@/components/standardUi/InputField.vue";
import SmallBtnFinish from "@/components/standardUi/SmallBtnFinish.vue";
import CBSearchSuggestions from "@/components/standardUi/CBSearchSuggestions.vue";
import RegisterPackageModel from "@/classes/requests/PackageRequest";
import { pakketService } from "@/services/pakketService/pakketservice";
import { roomService } from "@/services/locatieService/roomservice";
import { personeelService } from "@/services/personeelService/personeelService";
import Person from "@/classes/Person";
import Room from "@/classes/Room";
import PackageValidation from "@/classes/validation/PackageValidation";
import SelectOption from "@/classes/helpers/SelectOption";
import { getCurrentInstance } from "@vue/runtime-core";
import { AxiosError } from "axios";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { roomHelper } from "@/classes/Room";
import Package from "@/classes/Package";

@Options({
  components: {
    BtnBack,
    InputField,
    SmallBtnFinish,
    CBSearchSuggestions,
    LoadingIcon,
  },
})
export default class RegisterPackage extends Vue {
  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  private loadRoom: boolean = true;
  private loadPers: boolean = true;
  private loadPostRequest: boolean = false;

  public fpackage: RegisterPackageModel = new RegisterPackageModel();

  private test: string = "";
  private overview: boolean = false;
  private btnText: string = "Volgende";
  private errorText: boolean = false;
  private error: string[] = ["Niet alle velden zijn ingevuld"];

  // Validation Fields.
  private v: PackageValidation = new PackageValidation();

  // Employee data.
  private receivers: Array<SelectOption> = new Array<SelectOption>();
  private allreceivers: Array<Person> = new Array<Person>();

  // Room data.
  private rooms: Array<SelectOption> = new Array<SelectOption>();
  private allRooms: Array<Room> = new Array<Room>();

  // Props
  private receiverName: String = "";
  private registratorName: String = "";
  private collectionPointName: String = "";
  private createdAtPointName: String = "";

  // Selected Prop Refs.
  private SelectedOption = SelectOption;

  public getPerson(id: String): Person | undefined {
    return this.allreceivers.find((receiver) => receiver.id == id);
  }

  public getRoom(id: String): Room | undefined {
    return this.allRooms.find((room) => room.id == id);
  }

  toggleStep() {
    // Clear errors
    this.errorText = false;
    this.error = [];

    // Validate if fields have values.
    if (!this.v.fieldsHaveValues(this.fpackage)) {
      this.errorText = true;
      this.error.push("Niet alle velden zijn ingevuld.");
    }

    // Check if locations are correct.
    const collectionRoom = this.getRoom(this.fpackage.CollectionPointId);
    this.v.CollectionPointIdValid = !!collectionRoom;

    const createdAtRoom = this.getRoom(this.fpackage.CreatedAtLocationId);
    this.v.CreatedAtLocationIdValid = !!createdAtRoom;

    if (!this.v.locationsCorrect()) {
      this.errorText = true;
      this.error.push("Dit afhaalpunt kon niet gevonden worden.");
    }

    const receiver = this.getPerson(this.fpackage.ReceiverId);
    if (receiver) {
      this.receiverName = receiver.name;
    }
    this.v.ReceiverIdValid = !!receiver;

    const registrator = this.getPerson(this.fpackage.CreatedByPersonId);
    if (registrator) {
      this.registratorName = registrator.name;
    }
    this.v.CreatedByPersonIdValid = !!registrator;

    if (!this.v.personsCorrect()) {
      // People do not exists.
      this.errorText = true;
      this.error.push("Deze persoon kon niet gevonden worden.");
    }

    if (!this.errorText) {
      this.overview = !this.overview;
      if (this.overview) {
        this.btnText = "Vorige";
      } else {
        this.btnText = "Volgende";
      }
    }
  }

  async registerPackage() {
    // Call to backend. Package is filled by emitters.
    this.loadPostRequest = true;
    pakketService
      .post(this.fpackage)
      .then((res) => {
        this.loadPostRequest = false;
        var result = res;
        this.$router.push({
          name: "PackagePage",
          params: { id: result.id },
        });
      })
      .catch((err: AxiosError) => {
        this.emitter.emit("err", err);
        this.loadPostRequest = false;
      });
  }

  receiverChanged(input: SelectOption): void {
    this.fpackage.ReceiverId = input.id;
    this.v.ReceiverIdValid = true;
  }

  collectionPointChanged(input: SelectOption): void {
    this.fpackage.CollectionPointId = input.id;
    this.v.CollectionPointIdValid = true;
    this.collectionPointName = input.name;
  }

  registratorChanged(input: SelectOption): void {
    this.fpackage.CreatedByPersonId = input.id;
    this.v.CreatedByPersonIdValid = true;
  }

  registerLocationChanged(input: SelectOption): void {
    this.fpackage.CreatedAtLocationId = input.id;
    this.v.CreatedAtLocationIdHasValue = true;
    this.createdAtPointName = input.name;
  }

  async mounted() {
    roomService
      .getAll()
      .then((res) => {
        this.allRooms = res;
        this.allRooms.forEach((room) =>
          this.rooms.push(
            new SelectOption(room.id, roomHelper.getLocationString(room))
          )
        );
        this.loadRoom = false;

        // Default.
        this.fpackage.CreatedAtLocationId = this.rooms[0].id;
        this.createdAtPointName = this.selectedCreatedAtLocation().name;
      })
      .catch((err: AxiosError) => {
        this.emitter.emit("err", err);
        this.loadRoom = false;
      });

    personeelService
      .getAll()
      .then((res) => {
        this.allreceivers = res;
        this.allreceivers.forEach((receiver) =>
          this.receivers.push(new SelectOption(receiver.id, receiver.name))
        );
        this.loadPers = false;
      })
      .catch((err: AxiosError) => {
        this.emitter.emit("err", err);
        this.loadPers = false;
      });
  }

  public selectedCollectionPoint(): SelectOption {
    var id = "";
    var name = "";
    var existing = this.fpackage.CollectionPointId;
    if (existing) {
      var found = this.rooms.find((r) => r.id == existing);
      if (found) {
        id = found.id;
        name = found.name;
      }
    }
    return new SelectOption(id, name);
  }

  public selectedCreatedAtLocation(): SelectOption {
    var id = "";
    var name = "";
    var existing = this.fpackage.CreatedAtLocationId;
    if (existing) {
      var found = this.rooms.find((r) => r.id == existing);
      if (found) {
        id = found.id;
        name = found.name;
      }
    }
    return new SelectOption(id, name);
  }

  public selectedReceiver(): SelectOption {
    var id = "";
    var name = "";
    var existing = this.fpackage.ReceiverId;
    if (existing) {
      var found = this.receivers.find((r) => r.id == existing);
      if (found) {
        id = found.id;
        name = found.name;
      }
    }
    return new SelectOption(id, name);
  }

  public selectedRegistrator(): SelectOption {
    var id = "";
    var name = "";
    var existing = this.fpackage.CreatedByPersonId;
    if (existing) {
      var found = this.receivers.find((r) => r.id == existing);
      if (found) {
        id = found.id;
        name = found.name;
      }
    }
    return new SelectOption(id, name);
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.align-left {
  text-align: left;
}
.margin-button {
  margin-right: 5%;
}

hr {
  opacity: 0.4;
  margin: 0 0;
}

.group {
  margin-bottom: 2em;
}

p {
  margin-bottom: 1em;
}
</style>