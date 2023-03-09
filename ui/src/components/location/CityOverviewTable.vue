<template>
  <div>
    <LoadingIcon v-if="loading" />
    <div v-else>

     <!-- <TableComponent
        :items="items"
        :editable="true"
        @cell-clicked="CellClicked"
      />  -->

      <CityTable
        :items="items"
        @row-clicked="RowClicked"
      />
      <Pagination
        v-if="allCities.length > visibleItemsPerPageCount"
        :page-count="pageCount"
        :visible-items-per-page-count="visibleItemsPerPageCount"
        :visible-pages-count="Math.min(pageCount, 5)"
        @nextPage="loadPage('next')"
        @previousPage="loadPage('previous')"
        @loadPage="loadPage"
      />
      <!-- Edit modal -->
      <CityModal v-if="modalOpen" @close-location="CloseModal()">
        <CityInfo
          :ColumnType="ColumnType"
          :cityId="cityId"
          @reload-table="ReloadTable"
        />
      </CityModal>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import TableComponent from "@/components/standardUi/TableComponent.vue";
import CityInfo from "@/components/location/CityInfo.vue";
import CityModal from "@/components/location/CityModal.vue";
import { ColumnType } from "@/classes/table/ColumnType";
import Room from "@/classes/Room";
import City from "@/classes/City"
import { roomService } from "@/services/locatieService/roomservice";
import { getCurrentInstance } from "@vue/runtime-core";
import { AxiosError } from "axios";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { TableCell } from "@/classes/table/TableCell";
import Pagination from "@/components/standardUi/Pagination/BasePagination.vue";
import CityTable from "@/components/location/CityTable.vue";
import { cityService } from "@/services/locatieService/cityservice";
@Options({
  components: {
    Pagination,
    TableComponent,
    LoadingIcon,
    CityInfo,
    CityModal,
    CityTable
  },
})
export default class CityOverviewTable extends Vue {
  private loading: boolean = true;
  private error: boolean = false;

  /* LocationInfo Modal */
  public ColumnType: ColumnType = ColumnType.CITY;
  public cityId: string = "";
  public buildingId: string = "";
  public roomId: string = "";

  public modalOpen: boolean = false;
  public CloseModal(): void {
    this.modalOpen = false;
  }

  /* LocationTable */
  private items: Array<Object> = new Array<Object>();
  private allRooms: Array<Room> = new Array<Room>();
  private rooms: Array<Room> = new Array<Room>();
  private cities: Array<City> = new Array<City>();
  private allCities: Array<City> = new Array<City>();

  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  private visibleItemsPerPageCount = 20;
  private pageCount = 0;

  beforeMount() {
    this.GetRooms();
    this.GetCities();
  }

  async GetRooms() {
    roomService
      .getAll()
      .then((res) => {
        this.allRooms = res;
        this.pageCount = Math.ceil(
          this.allRooms.length / this.visibleItemsPerPageCount
        );
        this.loadPage(1);
        this.loading = false;
      })
      .catch((err: AxiosError) => {
        this.emitter.emit("err", err);
        this.loading = false;
        this.$router.push("/login");
      });
  }
  async GetCities(){
    cityService
    .getAll()
    .then((res) => {
      this.allCities = res;
      this.pageCount = Math.ceil(
        this.allCities.length / this.visibleItemsPerPageCount
      );
      this.loadPage(1);
      this.loading = false;
    })
    .catch((err: AxiosError) =>{
      this.emitter.emit("err", err);
      this.loading = false;
    });
  }

public RowClicked(id: string){
    console.log("row-clicked: " + id);
    // this.roomId = id;
    this.cityId = id;
    console.log("this.cityid= " + this.cityId)
    this.modalOpen = true;

}


  public CellClicked(cell: TableCell): void {
    if (cell) {
      this.ColumnType = cell.type as ColumnType;

      if (this.ColumnType == ColumnType.CITY) {
        this.cityId = cell.id;
      } else if (this.ColumnType == ColumnType.BUILDING) {
        this.buildingId = cell.id;
      } else if (this.ColumnType == ColumnType.ROOM) {
        this.roomId = cell.id;
      } else {
        return;
      }
      this.modalOpen = true;
    }
  }

  //Format objects to display in the table
GenerateTableObjects(cities: City[]){
  this.items = new Array<Object>();
  cities.forEach((value) =>{
    this.items.push({
      Stad: {
        id: value.id,
        displayName: value.name,
        type: ColumnType.CITY,
      } as TableCell,
    });
  });
}

  // GenerateTableObjects(rooms: Room[]) {
  //   this.items = new Array<Object>();
  //   rooms.forEach((value) => {
  //     this.items.push({
  //       Stad: {
  //         id: value.building.address.city.id,
  //         displayName: value.building.address.city.name,
  //         type: ColumnType.CITY,
  //       } as TableCell,
        // Gebouw: {
        //   id: value.building.id,
        //   displayName:
        //     value.building.name +
        //     ", " +
        //     value.building.address.street +
        //     " " +
        //     value.building.address.number,
        //   type: ColumnType.BUILDING,
        // } as TableCell,
        // Ruimte: {
        //   id: value.id,
        //   displayName: value.name,
        //   type: ColumnType.ROOM,
        // } as TableCell,
  //     });
  //   });
  // }

  ReloadTable(): void {
    this.items = [];
    this.modalOpen = false;
    this.GetCities();
  }

  public loadPage(value: number) {
    const pageIndex = (value - 1) * this.visibleItemsPerPageCount;
    this.cities = this.allCities.slice(
      pageIndex,
      pageIndex + this.visibleItemsPerPageCount
    );
    this.GenerateTableObjects(this.cities);
  }


  // ReloadTable(): void {
  //   this.items = [];
  //   this.modalOpen = false;
  //   this.GetRooms();
  // }

  // public loadPage(value: number) {
  //   const pageIndex = (value - 1) * this.visibleItemsPerPageCount;
  //   this.rooms = this.allRooms.slice(
  //     pageIndex,
  //     pageIndex + this.visibleItemsPerPageCount
  //   );
  //   this.GenerateTableObjects(this.rooms);
  // }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";
</style>
