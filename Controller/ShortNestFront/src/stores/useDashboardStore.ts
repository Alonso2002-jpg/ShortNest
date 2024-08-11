import {defineStore} from "pinia";
import {ref} from "vue";

export const useDashboardStore = defineStore('dashboard',{
    state: () => ({
        selectedOption: ref('urls'),
    }),
    actions: {
        setSelectedOption(option:string){
            this.selectedOption = option;
            console.log(this.selectedOption)
        }
    }
});