<script setup lang="ts">
import {useRouter} from "vue-router";
import {UrlStorageService} from "../composables/UrlStorageService.ts";
import {onBeforeUnmount, onMounted, ref} from "vue";
import {useDeviceStore} from "../stores/useDeviceStore.ts";
import {UrlStorage} from "../models/UrlStorage.ts";

const urlStorageService = new UrlStorageService()
const store = useDeviceStore();
const router = useRouter();
const urlStorage = ref(new UrlStorage())
const sitePass = ref('')
const corrPass = ref(true)

const routeExist = ref(true);

onMounted(async () => {
  urlStorage.value = await getUrlReal() || new UrlStorage()
})
const getUrlReal = async () => {
  urlStorage.value.urlShortest = router.currentRoute.value.path.substring(1)
  try {
    const response = await urlStorageService.getUrlStorageByShortUrl(urlStorage.value.urlShortest)
    return response.data
  }catch (e: any){
    if (e.response && e.response.status === 404){
      routeExist.value=false;
    }
  }
}
const seeURL = async () => {
  window.location.href = urlStorage.value.urlReal
}

const checkSitePass = async () => {
  const response = await urlStorageService.checkSitePass(urlStorage.value.urlShortest, sitePass.value)
  if(response.data){
    await seeURL()
  }else{
    corrPass.value = false
  }
}
const handleResize = () => {
  store.updateDeviceType();
};

onMounted(() => {
  window.addEventListener('resize', handleResize);
  handleResize();
});

onBeforeUnmount(() => {
  window.removeEventListener('resize', handleResize);
});
</script>

<template>
  <div v-if="routeExist && !urlStorage.withPass" class="flex flex-column justify-content-center align-items-center" style="height: 77.5lvh">
    <p :class="{'text-xl':!store.isMobile, 'text-sm':store.isMobile}">Haz click en el siguiente boton para acceder a tu ruta!</p>
    <Button @click="seeURL" type="button" severity="secondary" label="Continue" size="large" icon="pi pi-play"/>
  </div>
  <div v-if="!routeExist" class="flex flex-column justify-content-center align-items-center" style="height: 77.5lvh">
  <h1>URL no encontrada</h1>
  </div>
  <div v-if="routeExist && urlStorage.withPass" class="flex flex-column gap-3 justify-content-center align-items-center" style="height: 77.5lvh">
    <p class="font-semibold text-xl"><i class="pi pi-lock mr-3 text-red-600"></i>This URL is protected, enter the password to continue</p>
    <Password v-model="sitePass" placeholder="Enter the password to access" toggleMask inputStyle="width: 20lvw;"/>
    <small v-if="!corrPass" class="text-red-700">Incorrect Password- Try again</small>
    <Button v-if="sitePass != ''" @click="checkSitePass()" type="button" severity="secondary" label="Try to access" size="medium" icon="pi pi-unlock"/>
  </div>
</template>

<style scoped>

</style>