<!-- 
    input of type datetime-local is not supported in firefox desktop or safari desktop
    TODO: change with plugin or implement own solution
-->

<template>
    <form>
        <label :for="name">{{labelText}}</label><br>
        <input type="datetime-local" :id="name" :name="name" v-model="date" @change="updateInput()" />
    </form>
</template>

<script lang="ts">
    import {Options, Vue} from 'vue-class-component';

    @Options({
        props:{
            name: String,
            labelText: String
        },
        emits: ['inputChanged']
    })

    export default class DateTimeInput extends Vue {
        date = new Date();

        updateInput() {
            console.log(this.date);
            this.$emit('inputChanged', this.date);
        }
    }
</script>

<style lang="scss">
    @import "@/styling/main.scss";

    input {
        border-radius: 0.4rem;
        border-width: 0;
        background: $background-color;
        padding-left: 0.8rem;
        padding-right: 0.8rem;
        width: 240px;
        height: 2rem;
        margin: auto 0;
    }

    input:focus {
        outline: none;
    }

    label {
        font-weight: bold;
    }
</style>