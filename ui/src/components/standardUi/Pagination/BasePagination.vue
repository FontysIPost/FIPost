<!--
  Pagination component is adapted from https://css-tricks.com/creating-a-reusable-pagination-component-in-vue/#top-of-site
-->

<template>
  <div>
    <button :disabled="isPreviousButtonDisabled" @click="pageChangeHandleValue('previous')">
      ←
    </button>
    <BasePaginationTrigger
        class="trigger"
        v-for="paginationTrigger in paginationTriggers"
        :class="{'trigger-current': paginationTrigger === currentPage}"
        :key="paginationTrigger"
        :pageNumber="paginationTrigger"
        @loadPage="pageChangeHandleValue"
    />
    <button :disabled="isNextButtonDisabled" @click="pageChangeHandleValue('next')">
      →
    </button>
  </div>
</template>

<script lang="ts">
import {defineComponent} from "vue";
import BasePaginationTrigger from "@/components/standardUi/Pagination/BasePaginationTrigger.vue";

const BasePagination = defineComponent ({
  components: {
    BasePaginationTrigger
  },
  props: {
    pageCount: {type: Number, required: true},
    visibleItemsPerPageCount: {type: Number, required: true},
    visiblePagesCount: {type: Number, default: 5}
  },
  data() {
    return {
      currentPage: 1
    }
  },
  computed: {
    isPreviousButtonDisabled(): boolean {
      return this.currentPage === 1;
    },
    isNextButtonDisabled(): boolean {
      return this.currentPage === this.pageCount;
    },
    paginationTriggers(): any {
      const currentPage = this.currentPage;
      const pageCount = this.pageCount;
      const visiblePagesCount = this.visiblePagesCount;
      const visiblePagesThreshold = Math.ceil((visiblePagesCount - 1) / 2);
      const paginationTriggersArray = Array(this.visiblePagesCount -1).fill(0);

      //case 1: selected page number is smaller than half of the list width (e.g. 1-2-3-4-18)
      if(currentPage <= visiblePagesThreshold + 1){
        paginationTriggersArray[0] = 1;
        const paginationTriggers = paginationTriggersArray.map(
            (paginationTrigger, index) => {
              return paginationTriggersArray[0] + index;
            }
        );
        paginationTriggers.push(pageCount);
        return paginationTriggers;
      }

      //case 2: selected page number is bigger than half of the list width counting from the end of the list (e.g. 1-15-16-17-18)
      if(currentPage >= pageCount - visiblePagesThreshold + 1){
        const paginationTriggers = paginationTriggersArray.map(
            (paginationTrigger, index) => {
              return pageCount - index;
            }
        );
        paginationTriggers.reverse().unshift(1)
        return paginationTriggers;
      }

      //case 3: all other cases (e.g. 1-4-5-6-18)
      paginationTriggersArray[0] = currentPage - visiblePagesThreshold + 1;
      const paginationTriggers = paginationTriggersArray.map(
          (paginationTrigger, index) => {
            return paginationTriggersArray[0] + index;
          }
      );
      paginationTriggers.unshift(1);
      paginationTriggers[paginationTriggers.length - 1] = pageCount;
      return paginationTriggers
    }
  },
  methods: {
    pageChangeHandleValue(value){
      switch(value){
        case 'next':
          this.currentPage += 1;
          break
        case 'previous':
          this.currentPage -= 1;
          break
        default:
          this.currentPage = value;
      }

      this.$emit("loadPage", this.currentPage);
    },
  },
})

export default BasePagination;
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.trigger {
  padding: 4px 8px ;
  cursor: pointer;
}

.trigger:hover{
  color: $pink-color;
  font-weight: bolder;
  text-decoration: underline;
}

.trigger-current {
  background-color: $modern-purple-color;
  color: white;
  border-radius: 20%;
  font-weight: bolder;
  text-decoration: none;
}

.trigger-current:hover{
  color: white;
  text-decoration: none;
}

button {
  background-color: transparent;
  padding: 4px;
  color: $modern-purple-color;
  border: 1px solid $modern-purple-color;
  border-radius: 25%;
  margin: 0px 5px;
}

</style>