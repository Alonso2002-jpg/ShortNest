import {defineStore} from "pinia";
import {ref} from "vue";
import axios from "axios";
import {Register} from "../models/Register.ts";
import {Login} from "../models/Login.ts";
import {TokenResponse} from "../models/TokenResponse.ts";

export const useAuthStore = defineStore('auth',{
    state: () => ({
        token:ref(''),
        route: `${import.meta.env.VITE_BASE_URL}/Auth`,
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
        async loginWithGoogle(token:TokenResponse){

            try {
                const response = await axios.post(`${this.route}/login/google`, token);
                this.setToken(response.data.token)
                window.location.href = '/';
            } catch (error) {
                throw error;
            }
        },
        async loginWithX(token:TokenResponse){
            try {
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
        clearAuth() {
            this.token = '';
            window.localStorage.removeItem('token'); 
            delete axios.defaults.headers.common['Authorization']; 
        },
    },
})