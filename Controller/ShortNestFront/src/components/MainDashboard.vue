<script setup lang="ts">

import {onMounted, ref} from "vue";
import {UrlStorageService} from "../composables/UrlStorageService.ts";
import {UrlStorage} from "../models/UrlStorage.ts";
import {useConfirm} from "primevue/useconfirm";
import {useToast} from "primevue/usetoast";

const isLoading = ref(true)
const visible = ref(false)
const toEdit = ref(new UrlStorage())
const urlsByUser = ref([new UrlStorage()])
const urlStorageService = new UrlStorageService()
const confirm = useConfirm();
const toast = useToast();

const frontUrl = import.meta.env.VITE_FRONT_URL

onMounted(async () => {
  const response = await urlStorageService.getUrlStorageByUserId()
  urlsByUser.value = response.data
  isLoading.value = false
})

const deleteUrlStorage = async (id: string) => {
    confirm.require({
      message: 'Te gustaria eliminar esta URL?',
      header: 'Zona de peligro',
      icon: 'pi pi-info-circle',
      rejectLabel: 'Cancelar',
      rejectProps: {
        label: 'Cancelar',
        severity: 'secondary',
        outlined: true
      },
      acceptProps: {
        label: 'Eliminar',
        severity: 'danger'
      },
      accept: () => {
        urlStorageService.deleteUrlStorage(id)
        urlsByUser.value = urlsByUser.value.filter(url => url.id !== id)
        toast.add({ severity: 'info', summary: 'Eliminado', detail: 'La url ha sido eliminada.', life: 3000 });
      },
      reject: () => {
        toast.add({ severity: 'error', summary: 'No Eliminado', detail: 'No se ha eliminado el url.', life: 3000 });
      }
    });
}
const updateUrlStorage = async (id:string) => {
  if(!validUrl(toEdit.value.urlReal)){
    toast.add({ severity: 'error', summary: 'URL Invalida', detail: 'La url no es valida.', life: 3000 });
    return
  }
  try {
    await urlStorageService.updateUrlStorage(toEdit.value)
    urlsByUser.value = urlsByUser.value.map(url => url.id === id ? toEdit.value : url)
    toast.add({ severity: 'success', summary: 'Actualizado', detail: 'La url ha sido actualizada.', life: 3000 });
    visible.value = false
  }catch (e){
    console.log(e)
    toast.add({ severity: 'error', summary: 'No Actualizado', detail: 'No se ha actualizado el url.', life: 3000 });
  }
}

const validUrl = (url: string) => {
  let urlRegexp = /^(http:\/\/|https:\/\/)/
  return url && url.length > 0 && !url.includes(frontUrl) && urlRegexp.test(url)
}

const goToEdit= (url: UrlStorage) => {
  toEdit.value.id = url.id
  toEdit.value.urlReal = url.urlReal
  toEdit.value.urlShortest = url.urlShortest
  
  visible.value = true
}

const headerStyles = {
  color: 'black',
  'font-size': '1.3em',
}

const dataStyles ={
  color: 'black',
  'font-size': '1.1em',
}
</script>

<template>
<section class="main-dash p-6">
  <div class="bg-white-alpha-50 flex flex-column align-items-center pt-5 h-full">
    <h1 class="text-4xl">Gestion de direcciones</h1>
    <Divider />
    <Toast />
    <ConfirmDialog></ConfirmDialog>
      <DataTable v-if="urlsByUser.length>0 && isLoading == false" :value="urlsByUser" stripedRows tableStyle="min-width: 60lvw">
        <Column field="urlReal" header="Real URL" :headerStyle="headerStyles" :bodyStyle="dataStyles"></Column>
        <Column field="urlShortest" header="Shortest URL" :headerStyle="headerStyles" :bodyStyle="dataStyles"></Column>
        <Column header="Acciones" :headerStyle="headerStyles">
          <template #body="slotProps" >
            <div class="flex justify-content-left align-items-center gap-3">
              <Button icon="pi pi-pencil" class="p-button-rounded p-button-info p-mr-2" @click="goToEdit(slotProps.data)"></Button>
              <Button icon="pi pi-trash" class="p-button-rounded p-button-danger" @click="deleteUrlStorage(slotProps.data.id)"></Button>
            </div>
          </template>
        </Column>
      </DataTable>
    
    <div v-else-if="urlsByUser.length <= 0 && !isLoading" class="flex flex-column justify-content-center align-items-center h-full">
      <h1 class="text-4xl">No tiene URLs registradas</h1>
      <div class="my-3 flex align-items-center justify-content-center">
        <a href="/" class="homeButton flex align-items-center p-2 px-5 border-round gap-1 hover:text-white-alpha-80 no-underline">
          <i class="pi pi-home"></i>
          <span>Back to Home to Short a URL</span>
        </a>
      </div>
    </div>
    
    <div v-else-if="isLoading" class="flex justify-content-center align-items-center h-full">
      <ProgressSpinner
          style="width: 10lvw; height: 100%"
          strokeWidth="4"
      />
    </div>

    <div class="card flex justify-content-center">
      <Dialog v-model:visible="visible" modal header="Edit URL" :style="{ width: '35rem' }">
        <template #header>
          <div class="inline-flex align-items-center justify-content-center gap-2">
            <i class="pi pi-link"></i>
            <span class="font-bold whitespace-nowrap text-xl">Update your URL</span>
          </div>
        </template>
        <span class="text-surface-500 dark:text-surface-400 block mb-6">Update the data of your shortened URL, what the user will see, and the destination URL.</span>
        <div class="flex align-items-center gap-4 mb-4">
          <label for="realurl" class="font-semibold w-7rem">Original URL</label>
          <InputText id="realurl" class="flex-auto" autocomplete="off" v-model:="toEdit.urlReal"/>
        </div>
        <div class="flex align-items-center gap-5 mb-6">
          <label for="shorturl" class="font-semibold w-9rem">Shortest URL</label>
          <div class="flex flex-column w-full">
            <InputText id="shorturl" class="flex-auto" autocomplete="off" disabled="true" v-model="toEdit.urlShortest"/>
            <small>(The shortened URL will be: : {{frontUrl + '/' +toEdit.urlShortest}})</small>
          </div>
        </div>
        <div class="flex justify-content-end gap-2">
          <Button type="button" label="Cancel" severity="secondary" @click="visible=false"></Button>
          <Button type="button" severity="contrast" label="Save" @click="updateUrlStorage(toEdit.id)" />
        </div>
      </Dialog>
    </div>
  </div>
</section>
</template>

<style scoped>
.main-dash{
  background-color: var(--seven-color);
  position: fixed;
  top: 1em;
  left: 23lvw;
  width: 74.5lvw;
  height: 96lvh;
  z-index: 100;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  transition: all 0.3s;
  
}

.homeButton{
  background: var(--first-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}


</style>