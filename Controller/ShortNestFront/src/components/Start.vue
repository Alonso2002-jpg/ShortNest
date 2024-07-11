<script setup lang="ts">
import homeImage from "../assets/img/home-imagen.png";
import Shortener from "./Shortener.vue";
</script>

<template>
  <div class="flex justify-content-between align-items-center">
    <div class="card flex justify-center align-items-center " style="width: 70vw; height: 80lvh; margin-left: 10lvw">
      <Image :src="homeImage" alt="Image" width="500" />
    </div>
    <div style="margin-right: 10lvw">
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