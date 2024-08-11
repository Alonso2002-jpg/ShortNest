<script setup lang="ts">
import {useDashboardStore} from "../stores/useDashboardStore.ts";

defineProps({
  compName: String,
  icon: String,
  dir: String,
  selectedOption: {
    type: String,
    required: true
  }
})
const dashStore = useDashboardStore();
</script>

<template>
  <section class="h-5rem sideComp" :class="{'isSelected': dashStore.selectedOption==selectedOption}" @click="dashStore.setSelectedOption(selectedOption)">
    <a href="#" class="flex text-lg w-full h-full align-items-center px-4 py-2 text-gray-700" >
      <i :class="icon"></i>
      <span class="mx-4">{{compName}}</span>
    </a>
  </section>
</template>

<style scoped>
.isSelected{
  background-color: #c9c8c2;
  color: var(--first-color);
}

.sideComp:hover{
  background-color: #c9c8c2;
  transition: all 0.3s;
  cursor: pointer;
}
</style>