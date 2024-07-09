import { createRouter, createWebHistory } from 'vue-router'
import Redirectioner from "./views/Redirectioner.vue";
import Home from "./views/Home.vue";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: Home,
        },
        {
            path: '/:id',
            component: Redirectioner,
        },
    ],
})
