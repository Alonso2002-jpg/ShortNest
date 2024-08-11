<script setup lang="ts">
import {useDashboardStore} from "../stores/useDashboardStore.ts";
import UrlsManagement from "../views/UrlsManagement.vue";

const dashStore = useDashboardStore()
</script>

<template>
<section class="main-dash p-6">
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



</style>