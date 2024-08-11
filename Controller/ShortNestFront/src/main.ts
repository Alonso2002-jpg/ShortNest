import { createApp } from 'vue'
import { initializeApp } from 'firebase/app';
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
import FloatLabel from "primevue/floatlabel";
import Password from "primevue/password";
import Divider from 'primevue/divider';
import Checkbox from "primevue/checkbox";
import axios from "axios";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import DataView from "primevue/dataview";
import SelectButton from "primevue/selectbutton";
import ConfirmDialog from "primevue/confirmdialog";
import ConfirmationService from 'primevue/confirmationservice';
import ProgressSpinner from 'primevue/progressspinner';
import ToastService from 'primevue/toastservice';
import Toast from "primevue/toast";
import Dialog from 'primevue/dialog';

if(window.localStorage.getItem('token') !== null){
    const token = window.localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

const firebaseConfig = {
    apiKey: import.meta.env.VITE_API_KEY_FRBASE,
    authDomain:import.meta.env.VITE_AUTH_DOMAIN_FRBASE,
    projectId: import.meta.env.VITE_PROY_ID_FRBASE,
    storageBucket: import.meta.env.VITE_STOR_BUCKET_FRBASE,
    messagingSenderId: import.meta.env.VITE_SENDER_ID_FRBASE,
    appId: import.meta.env.VITE_APP_ID_FRBASE,
    measurementId: import.meta.env.VITE_MEASUREMENT_ID_FRBASE
};

initializeApp(firebaseConfig);
const app = createApp(App);
app.use(router)
app.use(createPinia())
app.use(PrimeVue, {
    theme: {
        preset: Aura
    }
});
app.use(ConfirmationService);
app.use(ToastService);
app.component('Button', Button);
app.component('SelectButton', SelectButton);
app.component('Menubar', Menubar);
app.component('Image', Image);
app.component('Card', Card);
app.component('InputText', InputText)
app.component('Password', Password)
app.component('Drawer', Drawer)
app.component('FloatLabel', FloatLabel)
app.component('Divider', Divider)
app.component('Checkbox', Checkbox)
app.component('DataTable', DataTable)
app.component('DataView', DataView)
app.component('Column', Column)
app.component('ConfirmDialog', ConfirmDialog)
app.component('Toast', Toast)
app.component('ProgressSpinner', ProgressSpinner)
app.component('Dialog', Dialog)

app.mount('#app')