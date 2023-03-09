<template>
  <div>
    <LoadingIcon v-if="loading" />
    <div v-else>

     <!-- <TableComponent
        :items="items"
        :editable="true"
        @cell-clicked="CellClicked"
      />  -->

      <LocationTable
        :allItems="allItems"
        :firstItem="firstItem"
        :lastItem="lastItem"
        @row-clicked="RowClicked"
      />
      <Pagination
        v-if="allRooms.length > visibleItemsPerPageCount"
        :page-count="pageCount"
        :visible-items-per-page-count="visibleItemsPerPageCount"
        :visible-pages-count="Math.min(pageCount, 5)"
        @nextPage="loadPage('next')"
        @previousPage="loadPage('previous')"
        @loadPage="loadPage"
      />
      <LocationModal v-if="modalOpen" @close-location="CloseModal()">
        <LocationInfo
          :ColumnType="ColumnType"
          :cityId="cityId"
          :buildingId="buildingId"
          :roomId="roomId"
          @reload-table="ReloadTable"
        />
      </LocationModal>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import TableComponent from "@/components/standardUi/TableComponent.vue";
import LocationInfo from "@/components/location/LocationInfo.vue";
import LocationModal from "@/components/location/LocationModal.vue";
import { ColumnType } from "@/classes/table/ColumnType";
import Room from "@/classes/Room";
import { roomService } from "@/services/locatieService/roomservice";
import { getCurrentInstance } from "@vue/runtime-core";
import { AxiosError } from "axios";
import LoadingIcon from "@/components/standardUi/LoadingIcon.vue";
import { TableCell } from "@/classes/table/TableCell";
import Pagination from "@/components/standardUi/Pagination/BasePagination.vue";
import LocationTable from "@/components/location/LocationTable.vue";
@Options({
  components: {
    Pagination,
    TableComponent,
    LoadingIcon,
    LocationInfo,
    LocationModal,
    LocationTable
  },
})
export default class LocationOverviewTable extends Vue {
  private loading: boolean = true;
  private error: boolean = false;

  /* LocationInfo Modal */
  public ColumnType: ColumnType = ColumnType.ROOM;
  public cityId: string = "";
  public buildingId: string = "";
  public roomId: string = "";
  public firstItem;
  public lastItem;

  public modalOpen: boolean = false;
  public CloseModal(): void {
    this.modalOpen = false;
  }

  /* LocationTable */
  private allItems: Array<Object> = new Array<Object>();
  private allRooms: Array<Room> = new Array<Room>();

  private emitter = getCurrentInstance()?.appContext.config.globalProperties
    .emitter;

  private visibleItemsPerPageCount = 8;
  private pageCount = 0;

  beforeMount() {
    this.GetRooms();
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

  public RowClicked(id: string) {
    console.log("row-clicked: " + id);
    this.roomId = id;
    this.modalOpen = true;
    console.log(this.allItems);
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
   GenerateAllTableObjects(allrooms : Room[]) {
   this.allItems = new Array<Object>();
    allrooms.forEach((value) => {
      this.allItems.push({
        Stad: {
          id: value.building.address.city.id,
          displayName: value.building.address.city.name,
          type: ColumnType.CITY,
        } as TableCell,
        Gebouw: {
          id: value.building.id,
          displayName:
            value.building.name +
            ", " +
            value.building.address.street +
            " " +
            value.building.address.number,
          type: ColumnType.BUILDING,
        } as TableCell,
        Ruimte: {
          id: value.id,
          displayName: value.name,
          type: ColumnType.ROOM,
        } as TableCell,
      });
    });
   }

  ReloadTable(): void {
    this.allItems = [];
    this.modalOpen = false;
    this.GetRooms();
  }

  public loadPage(value: number) {
    this.firstItem = value * this.visibleItemsPerPageCount - this.visibleItemsPerPageCount;
    this.lastItem = value * this.visibleItemsPerPageCount;
    //this.GenerateTableObjects(this.rooms);
    this.GenerateAllTableObjects(this.allRooms);
  }
}
</script>

<style lang="scss" scoped>
  @import "@/styling/main.scss";
</style>
