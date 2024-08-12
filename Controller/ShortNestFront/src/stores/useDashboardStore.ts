import {defineStore} from "pinia";
import {ref} from "vue";

export const useDashboardStore = defineStore('dashboard',{
    state: () => ({
        selectedOption: ref('urls'),
    }),
    actions: {
        setSelectedOption(option:string){
            console.log(option)
            option != 'home' ? this.selectedOption = option : window.location.href = '/';
        }
    }
});