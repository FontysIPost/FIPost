<template>
    <div class="container">
      <div class="container">
        <VueSignaturePad
          id="signature"
          width="30em"
          height="10em"
          position="absolute"
          ref="signaturePad"
          :options="options"
        />
      </div>
      <div class="buttons">
        <button class="finish" @click="undo">Undo</button>
        <button @click="save">Save</button>
        <!-- <button @click="change">Change Color</button>
        <button @click="resume">Resume Color</button> -->
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "App",
    data: () => ({
      options: {
        penColor: "black",
      },
    }),
    methods: {
      undo() {
        this.$refs.signaturePad.undoSignature();
      },
      save() {
        const { isEmpty, data } = this.$refs.signaturePad.saveSignature();
        alert("Open DevTools see the save data.");
        console.log(isEmpty);
        console.log(data);
      },
      change() {
        this.options = {
          penColor: "#00f",
        };
      },
      resume() {
        this.options = {
          penColor: "#c0f",
        };
      },
    },
  };
  </script>
  
  <style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
  #signature {
    border: double 3px transparent;
    /* border-radius: 5px; */
    background-image: linear-gradient(white, white),
      radial-gradient(circle at top left, black, black);
    background-origin: border-box;
    background-clip: content-box, border-box;
  }
  .container {
    width: "500px";
    height: auto;
    /* padding: 8px 16px; */
  }
  .buttons {
    display: flex;
    gap: 8px;
    justify-content: center;
    margin-top: 8px;
  }
  .signaturepad{
    
  }
  </style>