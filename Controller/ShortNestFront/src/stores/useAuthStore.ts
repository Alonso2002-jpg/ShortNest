import {defineStore} from "pinia";
import {ref} from "vue";
import axios from "axios";
import {Register} from "../models/Register.ts";
import {Login} from "../models/Login.ts";
import {TokenResponse} from "../models/TokenResponse.ts";
import {getAuth, GoogleAuthProvider, signInWithPopup, TwitterAuthProvider} from "firebase/auth";
export const useAuthStore = defineStore('auth',{
    state: () => ({
        roles: ref(['']),
        token:ref(''),
        route: `${import.meta.env.VITE_BASE_URL}/Auth`,
        googleProvider: new GoogleAuthProvider(),
        xProvider: new TwitterAuthProvider(),
        auth : getAuth(),
    }),
    actions: {
        async login(login:Login){
            
            try {
                const response = await axios.post(`${this.route}/login`, login);
                this.setToken(response.data.token)
                window.location.href = '/';
            } catch (error) {
                throw error;
            }
        },
        async loginWithGoogle(){

            try {
                const result:any = await signInWithPopup(this.auth, this.googleProvider);
                const token = new TokenResponse();
                token.token = result.user.accessToken;
                const response = await axios.post(`${this.route}/login/google`, token);
                this.setToken(response.data.token)
                window.location.href = '/';
            } catch (error) {
                throw error;
            }
        },
        async loginWithX(){
            try {
                const result:any = await signInWithPopup(this.auth, this.xProvider);
                const token = new TokenResponse();
                token.token = result.user.accessToken;
                const response = await axios.post(`${this.route}/login/x`, token);
                this.setToken(response.data.token)
                window.location.href = '/';
            } catch (error) {
                throw error;
            }
        },
        async register(register:Register){
            try {
                const response = await axios.post(`${this.route}/register`, register);
                this.setToken(response.data.token)
                window.location.href = '/';
            } catch (error) {
                console.error(error);
                throw error;
            }
            
        },
        setToken(token:string){
            this.token = token;
            window.localStorage.setItem('token', this.token);
        },
        setRoles(roles:string[]){
            this.roles = roles;
        },
        isToken(){
            return localStorage.getItem('token') !== null;
        },
        clearAuth() {
            this.token = '';
            window.localStorage.removeItem('token'); 
            delete axios.defaults.headers.common['Authorization']; 
        },
    },
})