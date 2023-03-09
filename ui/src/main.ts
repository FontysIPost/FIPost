import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/index'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCheckCircle, faSortDown,faSortUp, faSearch, faFlagCheckered, faCheck, faPlusSquare, faHome } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import mitt from 'mitt';
const emitter = mitt();
library.add(faCheckCircle, faSortDown, faSortUp, faSearch, faFlagCheckered, faCheck, faPlusSquare, faHome)

var app = createApp(App);
app.config.globalProperties.emitter = emitter;

app.
use(router).
component('font-awesome-icon', FontAwesomeIcon).
mount('#app')
