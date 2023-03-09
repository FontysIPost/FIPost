<template>
  <div>
    <LoadingIcon v-if="loading" />
    <div v-else>

     <!-- <TableComponent
        :items="items"
        :editable="true"
        @cell-clicked="CellClicked"
      />  -->

      <BuildingTable
        :items="items"
        @building-clicked="BuildingClicked"
      />
      <Pagination
        v-if="allBuildings.length > visibleItemsPerPageCount"
        :page-count="pageCount"
        :visible-items-per-page-count="visibleItemsPerPageCount"
        :visible-pages-count="Math.min(pageCount, 5)"
        @nextPage="loadPage('next')"
        @previousPage="loadPage('previous')"
        @loadPage="loadPage"
      />
      <!-- Edit modal -->
      <BuildingModal v-if="modalOpen" @close-location="CloseModal()">
        <BuildingInfo
          :ColumnType="ColumnType"
          :buildingId="buildingId"
          @reload-table="ReloadTable"
        />
      </BuildingModal>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import TableComponent from "@/components/standardUi/TableComponent.vue";
import BuildingInfo from "@/components/location/BuildingInfo.vue";
import BuildingModal from "@/components/location/BuildingModal.vue";
import { ColumnType } from "@/classes/table/ColumnType";
import Room from "@/classes/Room";
import Building from "@/classes/Building"
import { roomService } from "@/services/locatieService/roomservice";
import { getCurrentInstance } from "@vue/runtime-core";
import { AxiosError } from "axios";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { TableCell } from "@/classes/table/TableCell";
import Pagination from "@/components/standardUi/Pagination/BasePagination.vue";
import BuildingTable from "@/components/location/BuildingTable.vue";
import { buildingService } from "@/services/locatieService/buildingservice";
@Options({
  components: {
    Pagination,
    TableComponent,
    LoadingIcon,
    BuildingInfo,
    BuildingModal,
    BuildingTable
  },
})
export default class BuildingOverviewTable extends Vue {
  private loading: boolean = true;
  private error: boolean = false;

  /* LocationInfo Modal */
  public ColumnType: ColumnType = ColumnType.BUILDING;
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
  private buildings: Array<Building> = new Array<Building>();
  private allBuildings: Array<Building> = new Array<Building>();

  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  private visibleItemsPerPageCount = 10;
  private pageCount = 0;

  beforeMount() {

    this.GetBuildings();
    console.log("mount building table")
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
      });
  }
  async GetBuildings(){
    buildingService
    .getAll()
    .then((res) => {
      this.allBuildings = res;
      this.pageCount = Math.ceil(
        this.allBuildings.length / this.visibleItemsPerPageCount
      );
      this.loadPage(1);
      this.loading = false;
    })
    .catch((err: AxiosError) =>{
      this.emitter.emit("err", err);
      this.loading = false;
    });
  }

public BuildingClicked(id: string){
    console.log("building-clicked: " + id);
    // this.roomId = id;
    this.buildingId = id;
    console.log("this.buildingid = " + this.buildingId)
    this.modalOpen = true;
}


  // public CellClicked(cell: TableCell): void {
  //   if (cell) {
  //     this.ColumnType = cell.type as ColumnType;

  //     if (this.ColumnType == ColumnType.CITY) {
  //       this.cityId = cell.id;
  //     } else if (this.ColumnType == ColumnType.BUILDING) {
  //       this.buildingId = cell.id;
  //     } else if (this.ColumnType == ColumnType.ROOM) {
  //       this.roomId = cell.id;
  //     } else {
  //       return;
  //     }
  //     this.modalOpen = true;
  //   }
  // }

  //Format objects to display in the table
GenerateTableObjects(buildings: Building[]){
  this.items = new Array<Object>();
  buildings.forEach((value) =>{
    this.items.push({
      Gebouw: {
        id: value.id,
        displayName: value.name,
        displayadres: value.address,
        type: ColumnType.BUILDING,
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
    this.GetBuildings();
  }

  public loadPage(value: number) {
    const pageIndex = (value - 1) * this.visibleItemsPerPageCount;
    this.buildings = this.allBuildings.slice(
      pageIndex,
      pageIndex + this.visibleItemsPerPageCount
    );
    this.GenerateTableObjects(this.buildings);
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
