<script setup lang="ts">
import {useDashboardStore} from "../stores/useDashboardStore.ts";
import UrlsManagement from "../views/UrlsManagement.vue";
import {useDeviceStore} from "../stores/useDeviceStore.ts";
import {onBeforeUnmount, onMounted} from "vue";

const deviceStore = useDeviceStore()
const dashStore = useDashboardStore()

const handleResize = () => {
  deviceStore.updateDeviceType();
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
<section v-if="!deviceStore.isMobile" class="main-dash p-6">
  <UrlsManagement v-if="dashStore.selectedOption == 'urls'"></UrlsManagement>
</section>
  <section v-if="deviceStore.isMobile" class="main-dash-mobile p-4">
    <UrlsManagement v-if="dashStore.selectedOption == 'urls'"></UrlsManagement>
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

.main-dash-mobile{
  background-color: var(--seven-color);
  position: fixed;
  width: 100lvw;
  height: 100lvh;
  z-index: 100;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  transition: all 0.3s;
}


</style>