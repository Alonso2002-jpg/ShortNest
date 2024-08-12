import { createRouter, createWebHistory } from 'vue-router'
import Redirectioner from "./views/Redirectioner.vue";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";
import Register from "./views/Register.vue";
import Unauthorized from "./views/Unauthorized.vue";
import Dashboard from "./views/Dashboard.vue";
import {authGuard} from "./middlewares/AuthGuard.ts";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: Home
        },
        {
            path: '/:id',
            component: Redirectioner,
            // meta: { roles: ['ADMIN'] },
            // beforeEnter: authGuard
        },
        {
            path: '/login',
            component: Login
        },
        {
            path: '/register',
            component: Register
        },
        {
          path: '/unauthorized',
          component: Unauthorized
        },
        {
            path: '/dashboard',
            component: Dashboard,
            meta: { roles: ['USER'] },
            beforeEnter: authGuard
        },
        {
            path: '/:pathMatch(.*)*',
            redirect: '/'
        }
    ],
})

