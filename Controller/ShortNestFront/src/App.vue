<script setup lang="ts">
import { useRouter } from "vue-router";
import { onMounted,ref, watch } from "vue";
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";

const withoutComple = ['/register', '/login', '/dashboard'];
const router = useRouter();
const withComple = ref(false);

onMounted(() => {
  verifyRoute(router.currentRoute.value.path);
});


watch(() => router.currentRoute.value.path, (newPath) => {
  verifyRoute(newPath);
});

const verifyRoute = (path:string) => {
  withComple.value = !withoutComple.includes(path);
}
</script>

<template>
  <Navbar v-if="withComple"></Navbar>
  <router-view></router-view>
  <Footer v-if="withComple"></Footer>
</template>

<style scoped>
</style>
