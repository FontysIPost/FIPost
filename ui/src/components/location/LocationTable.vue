<template>
     <div class="component-container overflow table-container">
    <table class="table" aria-describedby="Data table">
      <thead>
        <tr>
          <th
            v-for="(value, name, index) in items[0]"
            :id="index"
            :key="value"
            @click="sortBy(index)"
            :class="{ active: sortKey === index }"
          >
            {{ name }}
            <span
              class="arrow"
              :class="sortOrders[index] > 0 ? 'asc' : 'dsc'"
            />
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="entry in filteredItems" :key="entry" class="hoverElement" >
            
          <td @click="$emit('row-clicked', entry.Ruimte.id)"
            v-for="cell in entry"
            :key="cell"
          >
            {{ cell.displayName }} 
          </td>
          <td class="editColumn" @click="$emit('row-clicked', entry.Ruimte.id)">
              <span class="hide" />
              
          </td>
        </tr>

      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { TableCell } from "@/classes/table/TableCell";
import { ref } from "vue";
import { Emit, Prop } from "vue-property-decorator";
import { Vue } from "vue-class-component";

export default class TableComponent extends Vue {
  @Prop() public hovering: Object = ref(false);
  private items: Array<Object> = new Array<Object>();
  @Prop() public allItems!: Array<Object>;
  @Prop() public sortOrders: Array<number> = [];
  @Prop() public editable: Boolean = false;
  @Prop() public firstItem;
  @Prop() public lastItem;
  private sortKey: number = 0;
  
  
  @Emit("cell-clicked")
  @Emit("row-clicked")

ClickedTest(){
    console.log("clicked");
}

  // Gets called automatically.
  beforeUpdate() {
    if (!(this.sortOrders.length > 0)) {
      let items = this.items as Object[];
      if (items.length > 0) {
        this.InitSortOrders(Object.keys(items[0]).length);
      } else {
        console.error("No items for table");
      }
    }
  }
  
  
  get filteredItems(): Object[] {
    let filtered = this.allItems as Object[];

    //TODO: filter items here

    return this.sortedItems(filtered);
  }

  InitSortOrders(amount: number) {
    for (let i = 0; i < amount; i++) this.sortOrders[i] = 1;
  }

  sortBy(key: number) {
    this.sortKey = key;
    this.sortOrders[key] = this.sortOrders[key] * -1;
  }

  sortedItems(filtereditems: Object[]): Object[] {
    this.items.length = 0;
    const sortKey = this.sortKey;
    const order = this.sortOrders[sortKey] || 1;
    filtereditems.sort(function (a, b) {
      let x: TableCell = a[Object.keys(a)[sortKey]].displayName;
      let y: TableCell = b[Object.keys(b)[sortKey]].displayName;
      return (x === y ? 0 : x > y ? 1 : -1) * order;
    });     
    for (let i = this.firstItem; i < this.lastItem; i++) this.items.push(filtereditems[i]);
    return this.items;
  }
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";

.table-container {
  padding: 0 !important;
}

table {
  border-collapse: collapse;
  width: 100%;
  table-layout: fixed;
}

thead tr {
  box-shadow: $shadow;
}

thead {
  width: 100%;
}

th {
  cursor: pointer;
  color: $black-color;
  text-decoration: none;
}

table td,
th {
  padding: 0.75em !important;
  width: 25%;
}
@media only screen and (max-width: 650px) {
  th {
    width: 20%;
  }
}

td:first-child {
  font-weight: bold;
}

.hide {
  display: none;
  padding-left: 1rem;
  max-height: 1rem;
  content: url("~@/assets/icons8-edit.svg");
}

.hoverElement:hover {
  .hide {
    display: inline;
  }
}

.active {
  text-decoration: underline;
}

.arrow {
  display: inline-block;
  vertical-align: middle;
  width: 0;
  height: 0;
  margin-left: 5px;
  opacity: 0.66;
}

.asc {
  border-left: 4px solid transparent;
  border-right: 4px solid transparent;
  border-bottom: 4px solid $black-color;
}

.dsc {
  border-left: 4px solid transparent;
  border-right: 4px solid transparent;
  border-top: 4px solid $black-color;
}

td {
  cursor: pointer;
}
</style>