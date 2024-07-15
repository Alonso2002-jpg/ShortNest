<script setup lang="ts">
import logoImagen from "../assets/img/shortnestlogo2.png";
import { useDeviceStore } from "../stores/useDeviceStore.ts";
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
  <footer class="footer">
    <div class="mob-nav" :class="{'flex justify-content-between align-items-center mx-7': !store.isMobile}">
        <Image v-if="!store.isMobile" :src="logoImagen" alt="Image" width="150" />
        <section>
          <p class="m-0"><strong>ShortNest</strong> by <a >Jorge Alonso Cruz Vera.</a></p>
          <p>Follow this project on the GitHub repository by <strong><a href="https://github.com/Alonso2002-jpg/ShortNest">clicking here</a></strong></p>
        </section>
        <section>
          <p>Follow me here</p>
          <div class="flex justify-content-center align-items-center">
            <a href="https://www.facebook.com/toyito.totoro.love/?locale=es_ES">
              <Button icon="pi pi-facebook" severity="secondary" text raised rounded aria-label="Facebook" />
            </a>
            <a href="https://x.com/Alonshi2002">
              <Button icon="pi pi-twitter" severity="secondary" text raised rounded aria-label="X" />
            </a>
            <a href="www.linkedin.com/in/jorge-alonso-cruz">
              <Button icon="pi pi-linkedin" severity="secondary" text raised rounded aria-label="LinkedIn" />
            </a>
            <a href="https://github.com/Alonso2002-jpg">
              <Button icon="pi pi-github" severity="secondary" text raised rounded aria-label="GitHub" />
            </a>
          </div>
        </section>
    </div>
  </footer>
</template>

<style scoped>
.footer {
  padding: 2rem 1.5rem;
  background-color: var(--second-color);
  color: var(--fourth-color);
  text-align: center;
}
</style>