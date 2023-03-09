<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper" @click="Close()">
        <div class="modal-container" @click.stop.prevent>
          <div class="modal-header">
            <h3 name="header">{{ header }}</h3>
          </div>
          <div class="modal-body">
            <slot> </slot>
            <span name="body"> {{ body }} </span>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import { Emit, Prop } from "vue-property-decorator";
import BtnFinish from "@/components/standardUi/BtnFinish.vue";

@Options({
  components: {
    BtnFinish,
  },
  emits: ["close-location"],
})
export default class BuildingModal extends Vue {
  @Prop()
  public header: string = "";
  @Prop()
  public body: string = "";

  @Emit("close-location")
  Close() {}
}
</script>

<style lang="scss" scoped>
@import "@/styling/main.scss";
.modal-mask {
  position: fixed;
  z-index: 2;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 300px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
  overflow: hidden;
}

.modal-header h3 {
  margin-top: 0;
  font-family: $font-family;
  color: $modern-purple-color;
}

.modal-body {
  margin: 20px 0;
  font-family: $font-family;
  color: $light-black-color;
}

.modal-footer {
  height: 100%;
  margin: 20px 0;
}

.modal-default-button {
  float: right;
  background-color: $modern-purple-color;
  border: none;
  width: 50px;
  height: 30px;
  border-radius: $small-border-radius;
  box-shadow: $shadow;
  font-family: $font-family;
  color: white;
  cursor: pointer;
}

.modal-enter {
  opacity: 0;
}

.modal-leave-active {
  opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}
</style>

