<script setup lang="ts">
import {onBeforeUnmount, onMounted, ref} from "vue";
import {useDeviceStore} from "../stores/useDeviceStore.ts";
import {useRouter} from "vue-router";
import {useAuthStore} from "../stores/useAuthStore.ts";
const router = useRouter();
const authStore = useAuthStore();
const store = useDeviceStore();
const token = localStorage.getItem('token');
const redirectTo = (path:string) => {
  router.push(path);
};

const items = ref([
  {
    label: 'Home',
    icon: 'pi pi-home',
    url: ''
  },
  {
    label: 'Features',
    icon: 'pi pi-star',
    url: ''
  },
  {
    label: 'Pricing',
    icon: 'pi pi-dollar',
    url: ''
  },
  {
    label: 'Contact',
    icon: 'pi pi-envelope',
    url: ''
  }])
let visibleRight = ref(false)
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
  <div class="flex align-items-center justify-content-between px-3 bg-black-alpha-10">
    <div class="card flex align-items-center">
      <h2 @click="router.push('/')" class="cursor-pointer">ShortNest</h2>
      <nav v-if="!store.isMobile" class="items">
        <ul class="flex align-items-center justify-content-center list-none gap-4">
          <li v-for="item in items" :key="item.label">
            <a :href="'/'+item.label" class="flex align-items-center p-2 border-round gap-1 text-black-alpha-80 no-underline">
              <i :class="item.icon"></i>
              <span>{{item.label}}</span>
            </a>
          </li>
          <li>
            <a v-if="authStore.isToken()" href="dashboard" class="isSelected flex align-items-center p-2 border-round gap-1 hover:text-black-alpha-80 no-underline">
              <i class="pi pi-crown"></i>
              <span>Dashboard</span>
            </a>
          </li>
        </ul>
      </nav>
    </div>
    <div v-if="store.isMobile">
      <Button severity="contrast" icon="pi pi-align-justify" @click="visibleRight = true" />
      <Drawer v-model:visible="visibleRight" header="Menu" position="right">
        <div class="flex flex-column mob-nav gap-2">
          <p><a href="/"> <i class="pi pi-home"></i> Home</a></p>
          <p><a href="/"> <i class="pi pi-star"></i> Features</a></p>
          <p><a href="/"> <i class="pi pi-dollar"></i> Pricing</a></p>
          <p><a href="/"> <i class="pi pi-envelope"></i> Contact</a></p>
          <p class="isSelected py-2 border-round-3xl"><a href="/dashboard" class="flex justify-content-center align-items-center gap-2"> <i class="pi pi-crown"></i> Dashboard</a></p>
          <div v-if="store.isMobile && !token" class="flex justify-content-between my-8">
            <Button type="button" severity="secondary" label="Login" @click="redirectTo('/login')" icon="pi pi-user"/>
            <Button type="button" severity="secondary" label="Register" @click="redirectTo('/register')" icon="pi pi-users" />
          </div>
          <div v-if="store.isMobile && token" class="flex justify-content-center align-items-center my-8 gap-2">
            <Button type="button" severity="secondary" label="Logout" @click="authStore.clearAuth();redirectTo('/login');" icon="pi pi-sign-out" />
          </div>
        </div>
      </Drawer>
    </div>
    <div v-if="!store.isMobile && !token" class="flex gap-2">
      <Button type="button" severity="contrast" label="Login" @click="redirectTo('/login')" icon="pi pi-users"/>
      <Button type="button" severity="secondary" label="Register" @click="redirectTo('/register')" icon="pi pi-users" class="button-hover" />
    </div>
    <div v-if="!store.isMobile && token" class="flex gap-2">
      <Button type="button" severity="secondary" label="Logout" @click="authStore.clearAuth();redirectTo('/login');" icon="pi pi-sign-out" class="button-hover" />
    </div>
  </div>
</template>

<style scoped>
.items a:hover{
  background: #bbbbbb;
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

.isSelected{
  background: var(--first-color);
  color: var(--fourth-color);
  border-color: var(--fourth-color);
}
</style>