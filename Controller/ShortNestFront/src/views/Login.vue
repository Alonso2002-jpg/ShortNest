<script setup lang="ts">
import { ref } from 'vue';
import { useVuelidate } from '@vuelidate/core';
import {minLength, required} from '@vuelidate/validators';
import {Login} from "../models/Login.ts";
import {ErrorRef} from "../models/ErrorRef.ts";
import {useRouter} from "vue-router";
import google from "../assets/img/google.svg";
import x from "../assets/img/x.svg";
import {useAuthStore} from "../stores/useAuthStore.ts";

const router = useRouter();
const authStore = useAuthStore();

const form = ref(new Login());
const errors = ref([new ErrorRef()])
const rules = {
  username: { required },
  password: { required, minLength: minLength(8) },
};
const v$ = useVuelidate(rules, form);

const loginWithGoogle = async () => {
  try {
    await authStore.loginWithGoogle()
  } catch (error) {
    console.log(error)
  }
};

const loginWithX = async () => {
  try {
    await authStore.loginWithX()
  } catch (error) {
    console.log(error)
  }
};

const loginWithUsername = async () => {
  try {
    await authStore.login(form.value);
  } catch (error) {
    const newError = new ErrorRef();
    newError.property = "usernotfound";
    newError.message = "User or Password incorrect";

    errors.value.push(newError)
  }
};
const submitForm = () => {
  v$.value.$validate();
  if (!v$.value.$invalid) {
    errors.value = []
    loginWithUsername();
    console.log(authStore.token)
  }else{
     errors.value = []
     v$.value.$errors.forEach(error => {
     let errorRef = new ErrorRef();
     errorRef.property = error.$property;
     errorRef.message = error.$message.toString();
     errors.value.push(errorRef)
   })
  }
};
const redirectTo = (path:string) => {
  router.push(path);
};
</script>

<template>
  <div class="flex w-full" style="height: 100lvh;width: 100lvw">
  <div class="w-6 h-full div-cont p-8">
      <div class="text-center">
        <h2 class="text-6xl">Login Now!</h2>
        <div class="flex gap-3 align-items-center justify-content-center" style="margin-top: 6lvh; margin-bottom: 4lvh">
          <Button outlined rounded class="border-black-alpha-40 text-black-alpha-90" @click="loginWithGoogle()">
            <Image :src="google"></Image>
            <p>Login with Google</p>
          </Button>
          <Button outlined rounded class="border-black-alpha-40 text-black-alpha-90 " @click="loginWithX()">
            <Image :src="x"></Image>
            <p>Login with X</p>
          </Button>
        </div>
        <p class="text-4xl" style="margin-bottom: 3lvh">Or use your email to login</p>
        <div class="flex flex-column mb-4" v-if="errors.find(e => e.property == 'usernotfound')">
          <span class="text-red-600">{{errors.find( e=> e.property == 'usernotfound')?.message}}</span>
        </div>
      </div>

      <form @submit.prevent="submitForm" class="text-center">
        <div class="field flex flex-column justify-content-center align-items-center">
          <label for="username" class="text-xl text-black-alpha-90">Username</label>
          <InputText
              id="username"
              type="text"
              v-model="form.username"
              placeholder="Username"
              style="width: 27lvw;"
              class="my-2"
          />

        </div>
        <div class="mb-2" v-if="errors.find(e => e.property == 'username')">
          <span class="text-red-600">{{errors.find( e=> e.property == 'username')?.message}}</span>
        </div>
        <div class="field flex flex-column justify-content-center align-items-center my-5">
          <label for="password" class="text-xl text-black-alpha-90">Password</label>
          <Password
              v-model="form.password"
              placeholder="password"
              inputStyle="width: 27lvw;"
              inputClass="my-2"
          />
        </div>
        <div class="flex flex-column mb-4" v-if="errors.find(e => e.property == 'password')">
          <span class="text-red-600">{{errors.find( e=> e.property == 'password')?.message}}</span>
        </div>
        <p class="text-xl cursor-pointer hover:text-gray-600 transition-duration-300" style="margin-top:3lvh">¿Do you forgot your password?</p>
        <div class="field text-center" style="margin-top:  15lvh">
          <Button severity="contrast" type="submit" size="large" outlined rounded class="w-30rem text-black-alpha-90 hover:text-white-alpha-90">Login me!</Button>
        </div>
      </form>
    </div>
  <div class="bg-black-alpha-10 w-6 flex justify-content-center">
      <section class="flex flex-column justify-content-center align-items-center">
        <h1 class="text-7xl">Hi again, buddy!</h1>
        <p class="text-3xl">Don't stop being with us and come here, log in now</p>
        <div class="field flex flex-column justify-content-center align-items-center gap-3" style="margin-top: 7lvh;">
          <b class="text-lg">¿Don't you have an account?</b>
          <Button severity="contrast" type="submit" size="large" outlined @click="redirectTo('/register')" rounded class="w-20rem text-black-alpha-90 hover:text-white-alpha-90">Go to register</Button>
        </div>
      </section>
    </div>
  </div>
</template>

<style scoped>

</style>