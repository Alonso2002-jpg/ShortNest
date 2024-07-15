<script setup lang="ts">
import homeImage from "../assets/img/home-imagen.png";
import Shortener from "./Shortener.vue";
import {useDeviceStore} from "../stores/useDeviceStore.ts";
import {onBeforeUnmount, onMounted} from "vue";

const store = useDeviceStore();

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
  <div class="flex align-items-center" :class="{'justify-content-between':!store.isMobile, 'justify-content-center':store.isMobile}">
    <div v-if="!store.isMobile" class="card flex justify-center align-items-center" style="width: 70vw; height: 80lvh; margin-left: 10lvw">
      <Image :src="homeImage" alt="Image" width="500" />
    </div>
    <div :class="{'m-3 my-8': store.isMobile}" :style="!store.isMobile ? 'margin-right: 10lvw' : ''">
      <h1 class="text-center">Welcome to ShortNest</h1>
      <p class="text-center">ShortNest is a URL shortener that allows you to shorten long URLs to short URLs.</p>
      <Shortener></Shortener>
      <div class="flex justify-content-center gap-5 mt-5">
        <Button type="button" label="Get Started" icon="pi pi-users"/>
        <Button type="button" label="More Info" icon="pi pi-users"/>
      </div>
    </div>
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