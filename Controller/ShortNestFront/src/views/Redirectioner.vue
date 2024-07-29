<script setup lang="ts">
import {useRouter} from "vue-router";
import {UrlStorageService} from "../composables/UrlStorageService.ts";
import {onBeforeUnmount, onMounted, ref} from "vue";
import {useDeviceStore} from "../stores/useDeviceStore.ts";

const urlStorageService = new UrlStorageService()
const store = useDeviceStore();
const router = useRouter();
let routeExist = ref(true);
let urlReal = ref('')
onMounted(async () => {
  urlReal.value = await getUrlReal() || ''
})
const getUrlReal = async () => {
  const urlShortest = router.currentRoute.value.path.substring(1)
  try {
    const response = await urlStorageService.getUrlStorageByShortUrl(urlShortest)
    return response.data.urlReal
  }catch (e: any){
    if (e.response && e.response.status === 404){
      routeExist.value=false;
    }
  }
}
const seeURL = async () => {
  window.location.href = urlReal.value
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
  <div class="flex flex-column justify-content-center align-items-center" style="height: 77.5lvh">
    <p v-if="routeExist" :class="{'text-xl':!store.isMobile, 'text-sm':store.isMobile}">Haz click en el siguiente boton para acceder a tu ruta!</p>
    <h1 v-else>URL no encontrada</h1>
    <Button v-if="routeExist" @click="seeURL" type="button" severity="secondary" label="Continue" size="large" icon="pi pi-play"/>
  </div>
</template>

<style scoped>

</style>