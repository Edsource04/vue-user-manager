import 'bootstrap/dist/css/bootstrap.min.css'
import 'sweetalert2/dist/sweetalert2.min.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import './assets/main.css'

const app = createApp(App)

import 'bootstrap/dist/js/bootstrap.bundle.min.js'

app.use(router)

app.mount('#app')
