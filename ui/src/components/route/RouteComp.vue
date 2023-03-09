<template>
  <div class="route-comp">
    <div v-if="tickets.length > 0">
      <div v-for="ticket in tickets" :key="ticket.id">
        <TicketComp class="ticket" :ticket="ticket" />
      </div>
    </div>
    <div v-else>
      <div>Er zijn geen tickets beschikbaar.</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import TicketComp from "@/components/route/TicketComp.vue";
import Ticket from "@/classes/Ticket";
import { Prop } from "vue-property-decorator";

@Options({
  components: {
    TicketComp,
  },
})
export default class RouteComp extends Vue {
  @Prop()
  private tickets: Ticket[] = [];

}
</script>

<style scoped lang="scss">
@import "@/styling/main.scss";

.route-comp {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  justify-content: flex-start;
  row-gap: 2em;
  height: 100%;
}

.ticket {
  margin: 1.5em 0;
}
</style>