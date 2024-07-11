<script setup lang="ts">
import {ref} from "vue";

const items = ref([
  {
    label: 'Home',
    icon: 'pi pi-home'
  },
  {
    label: 'Features',
    icon: 'pi pi-star'
  },
  {
    label: 'Pricing',
    icon: 'pi pi-dollar'
  },
  {
    label: 'Contact',
    icon: 'pi pi-envelope'
  }])
</script>

<template>
  <div class="flex align-items-center justify-content-between px-3 bg-black-alpha-10">
    <div class="card flex align-items-center">
      <h2>ShortNest</h2>
      <nav class="items">
        <ul class="flex align-items-center justify-content-center list-none gap-4">
          <li v-for="item in items" :key="item.label">
            <a href="/" class="flex align-items-center p-2 border-round gap-1 text-black-alpha-80 no-underline ">
              <i :class="item.icon"></i>
              <span>{{item.label}}</span>
            </a>
          </li>
        </ul>
      </nav>
    </div>
    <div class="flex gap-2">
      <Button type="button" severity="contrast" label="Login" icon="pi pi-users"/>
      <Button type="button" severity="secondary" label="Register" icon="pi pi-users" class="button-hover" />
    </div>
  </div>
</template>

<style scoped>
.items a:hover{
  background: #bbbbbb;
}

.p-button-contrast{
  background: var(--fourth-color);
  color: var(--second-color);
  border-color: var(--second-color);
}

.p-button-contrast:not(:disabled):hover{
  background: #b2aea5;
  color: #44342e;
  border-color: #44342e;
}

.button-hover{
  background: var(--second-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}

.button-hover:not(:disabled):hover{
  background: var(--first-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}
</style>