<script setup lang="ts">
import {ref} from "vue";
import { UrlStorageService } from "../composables/UrlStorageService";
import {UrlStorage} from "../models/UrlStorage.ts";
import ClipboardJS from 'clipboard';

const frontUrl = import.meta.env.VITE_FRONT_URL

let urlStorage = ref('')
let badUrl = ref(false)
let goodProcess = ref(false)
let urlShortest = ref('')
let copied = ref(false)
    
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
    urlShortest.value = frontUrl + '/' + response.data.urlShortest
    goodProcess.value = true
    badUrl.value = false
  }catch (e){
    console.log(e)
  }
}

new ClipboardJS('#copy-button', {
  text: function() {
    copied.value = true;
    return urlShortest.value
  }
})

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
      <Button v-if="!goodProcess" @click="shorten" severity="contrast" label="Shorten" class="px-4" icon="pi pi-arrow-down-left-and-arrow-up-right-to-center" />
      <div class="flex" v-if="goodProcess">
        <Button id="copy-button" severity="contrast" class="px-3" icon="pi pi-clipboard" />
        <Button @click="goodProcess = false; copied=false" severity="contrast" icon="pi pi-refresh" />
      </div>
    </div>
    <small v-if="!badUrl && !copied" id="username-help">Ingrese la URL que desea acortar.</small>
    <small v-if="badUrl" id="username-help" class="text-red-900">Ingrese una URL valida para acortar.</small>
    <small v-if="copied" id="username-help">Copiado!</small>
  </div>
</template>

<style scoped>
.p-button{
  background: var(--second-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}

.p-button:not(:disabled):hover{
  background: var(--first-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}

</style>