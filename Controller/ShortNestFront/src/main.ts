import { createApp } from 'vue'
import './style.css'
import 'primeflex/primeflex.css'
import 'primeflex/themes/primeone-light.css'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import Aura from '@primevue/themes/aura';
import Button from "primevue/button";
import "primeicons/primeicons.css";
import Menubar from "primevue/menubar";
import Image from "primevue/image";
import Card from "primevue/card";
import InputText from "primevue/inputtext";
import router from "./router.ts";
import {createPinia} from "pinia";
import Drawer from "primevue/drawer";

const app = createApp(App);
app.use(router)
app.use(createPinia())
app.use(PrimeVue, {
    theme: {
        preset: Aura
    }
});
app.component('Button', Button);
app.component('Menubar', Menubar);
app.component('Image', Image);
app.component('Card', Card);
app.component('InputText', InputText)
app.component('Drawer', Drawer)

app.mount('#app')