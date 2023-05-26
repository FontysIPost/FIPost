import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/index'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCheckCircle, faSortDown,faSortUp, faSearch, faFlagCheckered, faCheck, faPlusSquare, faHome } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import mitt from 'mitt';
import { VueSignaturePad } from 'vue-signature-pad';
const emitter = mitt();
library.add(faCheckCircle, faSortDown, faSortUp, faSearch, faFlagCheckered, faCheck, faPlusSquare, faHome)

var app = createApp(App);
app.config.globalProperties.emitter = emitter;

app.
use(router, VueSignaturePad).
component('font-awesome-icon', FontAwesomeIcon).
component("VueSignaturePad", VueSignaturePad).
mount('#app')