<script setup lang="ts">
import { useRouter } from "vue-router";
import { UrlStorageService } from "../composables/UrlStorageService.ts";

const urlStorageService = new UrlStorageService()
const router = useRouter();

const getUrlReal = async () => {
  const urlShortest = router.currentRoute.value.path.substring(1)
  try {
    const response = await urlStorageService.getUrlStorageByShortUrl(urlShortest)
    return response.data.urlReal
  }catch (e){
    console.log(e)
  }
}
const seeURL = async () => {
  const urlReal = await getUrlReal() || ''
  window.location.href = urlReal
}
</script>

<template>
  <div class="flex flex-column justify-content-center align-items-center" style="height: 92.5lvh">
    <p class="text-xl">Haz click en el siguiente boton para acceder a tu ruta!</p>
    <Button @click="seeURL" type="button" severity="secondary" label="Continue" size="large" icon="pi pi-play"/>
  </div>
</template>

<style scoped>

</style>