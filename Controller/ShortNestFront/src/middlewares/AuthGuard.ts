import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { jwtDecode } from 'jwt-decode';

interface DecodedToken {
    aud: string;
    exp: number;
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
    iss: string;
}

export const authGuard = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {

    const token = localStorage.getItem('token');
    console.log(from.meta.roles)
    if (!token) {
        next('/login');
    } else {
        try {
            const decodedToken = jwtDecode<DecodedToken>(token);
            const requiredRoles = to.meta.roles as string[];
            if (decodedToken.exp * 1000 < Date.now()) {
                next('/login');
            } else if (requiredRoles && !requiredRoles.some(role => decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].includes(role))) {
                next('/unauthorized');
            } else {
                next();
            }
        } catch (error) {
            next('/login');
        }
    }
};