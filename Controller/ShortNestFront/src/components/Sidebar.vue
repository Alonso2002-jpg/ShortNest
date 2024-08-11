<script setup lang="ts">
import SidebarComponent from "./SidebarComponent.vue";

</script>

<template>
    <div class="sidebar" >
<!--      <SidebarComponent  :compName="'General Stats'" :icon="'pi pi-chart-bar'"></SidebarComponent>-->
     <div>
       <SidebarComponent :compName="'My URLs'" :icon="'pi pi-link'" :selected-option="'urls'"></SidebarComponent>
       <SidebarComponent :compName="'My Account'" :icon="'pi pi-user'" :selected-option="'account'"></SidebarComponent>
     </div>
      <div class="my-3 flex align-items-center justify-content-center">
        <a href="/" class="homeButton flex align-items-center p-2 px-5 border-round gap-1 hover:text-white-alpha-80 no-underline">
          <i class="pi pi-home"></i>
          <span>Home</span>
        </a>
      </div>
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