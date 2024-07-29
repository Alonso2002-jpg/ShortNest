import { defineStore} from "pinia";

export const useDeviceStore = defineStore('device',{
    state: () => ({
        isMobile: window.innerWidth < 1250,
        isTablet: window.innerWidth < 1024,
    }),
    actions: {
        updateDeviceType(){
            this.isMobile = window.innerWidth < 1250;
        },
    },
})