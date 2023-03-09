<template>
  <span @click="onClick">
    {{pageNumber}}
  </span>
</template>

<script lang="ts">

import {defineComponent} from "vue";

const BasePagination = defineComponent({
  props: {
    pageNumber: {
      type: Number,
      required: true
    }
  },
  methods: {
    onClick(){
      this.$emit("loadPage", this.pageNumber)
    }
  }
})

export default BasePagination;
</script>

<style scoped lang="scss">

</style>