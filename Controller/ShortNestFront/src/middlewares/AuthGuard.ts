import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { jwtDecode } from 'jwt-decode';
import {DecodedToken} from "../models/DecodedToken.ts";
import {useAuthStore} from "../stores/useAuthStore.ts";


export const authGuard = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {

    const token = localStorage.getItem('token');
    const authStore = useAuthStore();
    if (!token) {
        next('/login');
    } else {
        try {
            const decodedToken = jwtDecode<DecodedToken>(token);
            authStore.setRoles(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].split(',') as string[]);
            const requiredRoles = to.meta.roles as string[];
            if (decodedToken.exp * 1000 < Date.now()) {
                next('/login');
            } else if (requiredRoles && !requiredRoles.some(role => authStore.roles.includes(role))) {
                next('/unauthorized');
            } else {
                next();
            }
        } catch (error) {
            next('/login');
        }
    }
};