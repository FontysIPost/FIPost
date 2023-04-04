<template>
  <div>
    <div class="next-step-container">
      <div v-if="fPackage == null">
        <div>
          Er ging iets mis bij het ophalen van de pakketgegevens. Probeer het
          later opnieuw.
        </div>
      </div>
      <div v-else>
        <div
          v-if="fPackage.routeFinished"
          class="container container-header modern-purple"
        >
          Route voltooid
        </div>
        <div v-else class="container container-header">Nieuwe actie</div>

        <LoadingIcon v-if="loading" />
        <div class="finished-comp" v-else-if="fPackage.routeFinished">
          <TicketComp :ticket="fPackage.tickets[0]" />
          
          <font-awesome-icon class="fc" icon="flag-checkered" />
        </div>
        <div v-else>
          <div v-if="!fPackage.routeFinished">
            <div class="form">
              <!-- <InputField
              label="Afgeleverd door:"
              v-model:input="selectedPersonOption"
              
              
              /> -->
             
              <CBSearchSuggestions
                :options="personOptions"
                :custom="true"
                :valid="personValid"
                @select-changed="personChanged"
                :selectedOption="selectedPersonOption"
              >
                <span class="hw">Afgeleverd door: </span>
              </CBSearchSuggestions>
              <CBSearchSuggestions
                :options="roomOptions"
                :custom="true"
                :valid="roomValid"
                @select-changed="roomChanged"
                :selectedOption="selectedRoomOption"
              >
                <span class="hw">Op locatie: </span>
              </CBSearchSuggestions>
              <div v-if="!showPersonConfirmation">
              <SmallBtnFinish
                        class="finish"
                        @btn-clicked="addTicketAction()"
                        :text="'Toevoegen'"
                        :isLoading="adding"
                    />
              </div>
              <div v-if="showPersonConfirmation" class="confirm-person">
                <hr />
                <div class="container container-header modern-pink">
                  Laatste stap
                </div>

                <Signature></Signature>

                <InputField
                  label="Voer FontysID in:"
                  v-model:input="inputFontysID"
                  @update:input="fontysIDCharacterCheck"
                />
               
                <!--
                <CBSearchSuggestions
                  :options="personOptions"
                  :custom="true"
                  :valid="personConfirmedValid"
                  @select-changed="personConfirmedChanged"
                  :selectedOption="selectedPersonConfirmedOption"
                >
                  <span>
                    Ik bevestig dat het pakket in goede orde is afgeleverd
                  </span>
                </CBSearchSuggestions> -->


                    <!-- Scan de fontyspas voor checkout:
                <StreamBarcodeReader
                    @decode="onDecode"
                    @loaded="onLoaded"
                ></StreamBarcodeReader>
                <ImageBarcodeReader
                    @decode="onDecode"
                    @error="onError"
                ></ImageBarcodeReader> -->

                <div v-if="passcan">
                  <div v-if="completedBy.name !== '' && completedBy != null">
                      <p>{{completedBy.name}}</p>
                    <SmallBtnFinish
                        class="finish"
                        @btn-clicked="addTicketAction()"
                        :text="'Toevoegen'"
                        :isLoading="adding"
                    />
                  </div>
                </div>

              </div>

              <ul v-if="errors">
                <li v-for="e in errors" :key="e" class="error-text">{{ e }}</li>
              </ul>
              
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import axios, { AxiosError } from "axios";

// Components.
import CBSearchSuggestions from "@/components/standardUi/CBSearchSuggestions.vue";
import SelectOption from "@/classes/helpers/SelectOption";
import SmallBtnFinish from "@/components/standardUi/SmallBtnFinish.vue";
import TicketComp from "@/components/route/TicketComp.vue";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { StreamBarcodeReader } from "vue-barcode-reader";
import { ImageBarcodeReader } from "vue-barcode-reader";
import InputField from "@/components/standardUi/InputField.vue";
import  Signature  from "@/components/BarCodeScanner/Signature.vue";
// Types.
import Person from "@/classes/Person";
import Room from "@/classes/Room";
import { roomHelper } from "@/classes/Room";
import Ticket from "@/classes/Ticket";
import TicketRequest from "@/classes/requests/TicketRequest";

// Services.
import { roomService } from "@/services/locatieService/roomservice";
import { personeelService } from "@/services/personeelService/personeelService";
import { getCurrentInstance } from "@vue/runtime-core";
import { Emit } from "vue-property-decorator";
import { pakketService } from "@/services/pakketService/pakketservice";
import Package from "@/classes/Package";
import { Prop } from "vue-property-decorator";

@Options({
  components: {
    SmallBtnFinish,
    CBSearchSuggestions,
    LoadingIcon,
    TicketComp,
    StreamBarcodeReader,
    ImageBarcodeReader,
    Signature,
    InputField
  },
})
export default class CreateTicket extends Vue {
  public ticket: Ticket = {
    id: "",
    location: roomHelper.getEmptyRoom(),
    finishedAt: 0,
    completedByPerson: "",
    receivedByPerson: "",
  } as Ticket;

  // Default.
  private loading: Boolean = true;
  private adding: boolean = false;
  @Prop()
  private fPackage!: Package;
  private showPersonConfirmation = false;

  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  // Errors.
  private errors: String[] = [];

  // Employee data.
  private selectedPersonOption: SelectOption = new SelectOption("", "");
  private selectedPersonConfirmedOption: SelectOption = new SelectOption(
    "",
    ""
  );
  
  private personOptions: Array<SelectOption> = new Array<SelectOption>();
  private persons: Array<Person> = new Array<Person>();
  private personValid: Boolean = true;
  private personConfirmedValid: Boolean = true;
  private completedBy: Person = new Person("","", "");
  private textInputId = '';
  private completedByName = '';
  private passcan = false;
  private inputFontysID = '';

  private personChanged(personOption: SelectOption) {
    this.selectedPersonOption = personOption;
    this.personValid = true;
    this.errors = [];
  }

  private personConfirmedChanged(personOption: SelectOption) {
    this.selectedPersonConfirmedOption = personOption;
    this.personConfirmedValid = true;
    this.errors = [];
  }

  // Room data.
  private selectedRoomOption: SelectOption = new SelectOption("", "");
  private roomOptions: Array<SelectOption> = new Array<SelectOption>();
  private rooms: Array<Room> = new Array<Room>();
  private roomValid: Boolean = true;
  private roomChanged(roomOption: SelectOption) {
    this.selectedRoomOption = roomOption;
    this.roomValid = true;
    this.errors = [];
    if (
      this.fPackage.collectionPoint != null &&
      roomOption.id == this.fPackage.collectionPoint.id
    ) {
      this.showPersonConfirmation = true;
    } else {
      this.showPersonConfirmation = false;
    }
  }

  private async runValidation() {
    this.errors = [];
    //  if (this.persons.some((p) => p.id == this.selectedPersonOption.id)) {
     this.personValid = true;
    //  else {
    //    this.errors.push("Deze persoon kon niet gevonden worden.");
    //    this.personValid = false;
    //  }

    if (this.showPersonConfirmation) {
      if (
        this.completedBy.id != null && this.completedBy.id !== ""
      ) {
        console.log(this.completedBy.id)
        this.personConfirmedValid = true;
      } else {
        this.errors.push("Deze persoon kon niet gevonden worden.");
        this.personConfirmedValid = false;
      }
    }

    if (this.rooms.some((r) => r.id == this.selectedRoomOption.id)) {
      this.roomValid = true;
    } else {
      this.errors.push("Deze ruimte kon niet gevonden worden.");
      this.roomValid = false;
    }
  }

  private async addTicketAction() {

    if (!this.adding) {
      this.adding = true;
      await this.runValidation();
      if (this.errors.length < 1) {
        console.log(this.selectedPersonOption.id)
            //console.log(this.selectedRoomOption.id)
            //console.log(this.fPackage.id)
            //console.log(this.showPersonConfirmation)
            
            //console.log(this.completedBy.id)
        await pakketService
          .createTicket({
            
            locationId: this.selectedRoomOption.id,
            packageId: this.fPackage.id,
            //completedByPersonId: this.inputResult,
            completedByPersonId: this.selectedPersonOption.id.toString(),
            receivedByPersonId: this.showPersonConfirmation
              ? this.completedBy.id
              : "",
          } as TicketRequest)
          .then((res) => {
            this.adding = false;
            console.log(this.selectedPersonOption.id)
          })
          .catch((err) => {
            
          });
        this.newTicket();
      } else {
        this.adding = false;
      }
    }
  }
  beforeMount(){
    this.completedBy = new Person("","","");
    
  }
  private async scanFontyspas(){
    await personeelService
    .getByFontysId(this.textInputId)
    .then((res) => {
      this.completedBy = res;
      this.completedByName = res.name;
      console.log(this.completedBy);
      this.passcan = true;
    })
  }

  private async fontysIDCharacterCheck(input: string){
    if(this.inputFontysID.length == 6 ){
      this.onDecode(this.inputFontysID)
    }
  }

  private async keydowntest(input: string) {
    console.log(input);
  }
  private async onDecode (result) {
    console.log(result)
    this.textInputId = result;
    await personeelService
        .getByFontysId(this.textInputId)
        .then((res) => {
          this.completedBy = res;
          this.completedByName = res.name;
          console.log(this.completedBy);
          this.passcan = true;
        })
    console.log(result)

  }

  @Emit("new-ticket")
  newTicket() {}

  async mounted() {
    await roomService
      .getAll()
      .then((res) => {
        this.rooms = res;
        this.rooms.forEach((room) =>{
          if(room.id != this.fPackage.tickets[0].location.id){
            this.roomOptions.push(
              new SelectOption(
                room.id,
                room.building.address.city.name +
                  ", " +
                  room.building.name +
                  ", " +
                  room.name
              )
            )
          }
      });
      })
      .catch((err: AxiosError) => {
        this.emitter.emit("err", err);
      });

    // await personeelService
      // .getAll()
      // .then((res) => {
      var person;
      axios.get('https://localhost:44369/api/Authentication/singleUser', {
        headers: {
          'Authorization' :  'Bearer ' + localStorage.getItem('token')
        }
      })
      .then((response)=>{
        // if(response.data.id !== null && response.data.id !== "" && response.data.id !== 0){
        //   this.showPersonConfirmation = true;
        // }
        person = response.data;
        this.persons = person;
        console.log(this.persons)
        
        this.selectedPersonOption = new SelectOption(person.id, person.name)
        console.log(this.selectedPersonOption)
        this.completedBy = person
        //console.log(this.personOptions)
      // });
      
        // this.personOptions.push(new SelectOption(person.id, person.name));
        
        // this.persons = response;
      //   this.persons.forEach((receiver) =>
      //      this.personOptions.push(new SelectOption(receiver.id, receiver.name))
      //    );
      // })
      });
      // .catch((err: AxiosError) => {
      //   this.emitter.emit("err", err);
      //   console.log(err)
      // });
     this.loading = false;
  }
}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.next-step-container {
  color: $black-color;
  text-align: left;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background: white;
  overflow: hidden;
  padding: 3em;
  box-shadow: $shadow;
  border-radius: $border-radius;
  row-gap: 1em;

  @media only screen and (max-width: 600px) {
    padding: 2em;
  }
}

.form {
  margin-top: 2em;
}

.message {
  text-decoration: underline;
}

.finish {
  cursor: pointer;
  align-self: flex-start;
}

.confirm-person {
  display: flex;
  flex-direction: column;
  row-gap: 1em;

  hr {
    width: 100%;
    height: 2px;
    margin: 1.5em auto;
    border: none;
    background-color: rgba($color: #000000, $alpha: 0.1);
  }
}

blockquote {
  padding: 0.8em;
  font-size: 0.8em;
  margin: 0;
  border-radius: $small-border-radius;
}

.hw {
  min-width: 200px;
}

.finished-comp {
  display: flex;
  justify-content: left;
  align-content: center;
  padding: 2em;
}

.fc {
  font-size: 3em;
  color: $modern-purple-color;
  margin-left: 20%;
}

@media only screen and (max-width: 700px) {
  .finished-comp {
    display: flex;
    flex-direction: column;
  }

  .fc {
    font-size: 0px;
  }
}

.input {
  user-select: none;
  border: 0px;
  width: 100%;
  height: 100%;
  background-color: $background-color;
  padding: 0px 0px;
}

.error {
  background: #ffc9cf;
}

.input:focus {
  outline: none;
  border: 0px;
}
</style>
