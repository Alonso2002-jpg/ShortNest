<script setup lang="ts">
import SidebarComponent from "./SidebarComponent.vue";

import {useDeviceStore} from "../stores/useDeviceStore.ts";
import {onBeforeUnmount, onMounted} from "vue";

const deviceStore = useDeviceStore()

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
    <div v-if="!deviceStore.isMobile" class="sidebar" >
     <div>
       <SidebarComponent :compName="'My URLs'" :icon="'pi pi-link'" :selected-option="'urls'"></SidebarComponent>
       <SidebarComponent :compName="'Dashboard (In Progress)'" :icon="'pi pi-chart-bar'" :notClickeable="true" :selected-option="'dashboard'"></SidebarComponent>
       <SidebarComponent :compName="'My Account (In Progress)'" :icon="'pi pi-user'" :notClickeable="true" :selected-option="'account'"></SidebarComponent>
     </div>
      <SidebarComponent :compName="'Home'" :icon="'pi pi-home'" :selected-option="'home'"></SidebarComponent>
    </div>
</template>

<style scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--six-color);
  position: fixed;
  top: 1em;
  left: 2lvw;
  width: 18lvw;
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