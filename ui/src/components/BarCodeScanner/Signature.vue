<template>
    <div class="signature-container">
        <div class="signature-pad-container">
            <VueSignaturePad
                id="signature"
                width="100%"
                height="10em"
                position="absolute"
                ref="signaturePad"
                :options="options"
            />
        </div>
        <div class="buttons">
            <button class="undo" @click="undo">Undo</button>
            <button class="save" @click="save">Save</button>
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
.signature-container {
    width: 100%;
    max-width: 500px;
    margin: 0 auto;
}

.signature-pad-container {
    position: relative;
}

#signature {
    border: double 2px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at top left, black, black);
    background-origin: border-box;
    background-clip: content-box, border-box;
}

.buttons {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 16px;
}

button {
    padding: 8px 16px;
    font-size: 16px;
}

.undo {
    background-color: #ccc;
    margin-right: 10px;
    border: none;
    border-radius: 4px;
    color: #333;
}

.save {
    background-color: #3f3cc9;
    margin-left: 10px;
    border: none;
    border-radius: 4px;
    color: #fff;
}
</style>
