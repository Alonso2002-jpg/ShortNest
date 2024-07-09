<script setup lang="ts">
import {ref} from "vue";
import { UrlStorageService } from "../composables/UrlStorageService";
import {UrlStorage} from "../models/UrlStorage.ts";

const frontUrl = import.meta.env.VITE_FRONT_URL

let urlStorage = ref('')
let badUrl = ref(false)
let goodProcess = ref(false)

    
let urlStorageService = new UrlStorageService()
let shorten = async () =>{
  if(!isValidUrl()){
    badUrl.value = true
    return
  }
  const urlStorageToSave = new UrlStorage()
  urlStorageToSave.urlReal = urlStorage.value
  try {
    const response = await urlStorageService.createUrlStorage(urlStorageToSave)
    urlStorage.value = frontUrl + '/' + response.data.urlShortest
    goodProcess.value = true
    badUrl.value = false
  }catch (e){
    console.log(e)
  }
}

let isValidUrl = () => {
  let urlRegexp = /^(http:\/\/|https:\/\/)/
  return urlStorage.value && !urlStorage.value.includes(import.meta.env.VITE_FRONT_URL) && urlRegexp.test(urlStorage.value)
}
</script>

<template>
  <div class="flex flex-column gap-2 py-3">
    <p v-if="goodProcess" class="text-1xl m-0 font-bold">URL Acortada correctamente</p>
    <div class="flex gap-2">
      <InputText id="username" class="w-full" v-model="urlStorage" aria-describedby="url-user" />
      <div class="flex" v-if="goodProcess">
        <Button @click="shorten" severity="contrast" class="px-3" icon="pi pi-clipboard" />
        <Button @click="goodProcess = false" severity="contrast" icon="pi pi-refresh" />
      </div>
    </div>
    <small v-if="!badUrl" id="username-help">Ingrese la URL que desea acortar.</small>
    <small v-if="badUrl" id="username-help" class="text-red-600">Ingrese una URL valida para acortar.</small>
  </div>
</template>

<style scoped>
.p-button{
  background: #44342e;
  color: #c2ba92;
  border-color: #c2ba92;
}

.p-button:hover{
  background: #603e35;
  color: #c2ba92;
  border-color: #c2ba92;
}

</style>