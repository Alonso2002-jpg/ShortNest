<script setup lang="ts">
import { ref } from 'vue';
import { useVuelidate } from '@vuelidate/core';
import {email, minLength, required} from '@vuelidate/validators';
import {ErrorRef} from "../models/ErrorRef.ts";
import {Register} from "../models/Register.ts";
import x from "../assets/img/x.svg";
import google from "../assets/img/google.svg";
import {useRouter} from "vue-router";
import {useAuthStore} from "../stores/useAuthStore.ts";

const router = useRouter();
const auth = useAuthStore();

const redirectTo = (path:string) => {
  router.push(path);
};
const form = ref(new Register());
const errors = ref([new ErrorRef()])

const rules = {
  name: { required },
  lastname: { required },
  email: { required, email },
  username: { required },
  password: { required, minLength: minLength(8) },
  confirmPassword: { required, minLength: minLength(8)},
};

const v$ = useVuelidate(rules, form);

const submitForm = () => {
  v$.value.$validate();
  if (!v$.value.$invalid) {
    errors.value = []
    auth.register(form.value);
  }else{
    errors.value = []
    v$.value.$errors.forEach(error => {
      let errorRef = new ErrorRef();
      errorRef.property = error.$property;
      errorRef.message = error.$message.toString();
      errors.value.push(errorRef)
    })
    console.log(errors)
  }
};
</script>

<template>
  <div class="flex w-full" style="height: 100lvh;width: 100lvw">
    <div class="bg-black-alpha-10 w-6 flex justify-content-center">
      <section class="flex flex-column justify-content-center align-items-center">
        <h1 class="text-7xl">Welcome to ShortNest!</h1>
        <p class="text-3xl">Come with us and discover more than a simple URL</p>
        <div class="field flex flex-column justify-content-center align-items-center gap-3" style="margin-top: 7lvh;">
          <b class="text-lg">¿Do you already have an account?</b>
          <Button severity="contrast" type="submit" size="large" outlined @click="redirectTo('/login')" rounded class="w-20rem text-black-alpha-90 hover:text-white-alpha-90">Go to login</Button>
        </div>
      </section>
    </div>
    <div class="w-6 h-full div-cont p-8">
        <div class="text-center">
          <h2 class="text-6xl">Create your account</h2>
          <div class="flex gap-3 align-items-center justify-content-center" style="margin-top: 6lvh; margin-bottom: 4lvh">
            <Button outlined rounded class="border-black-alpha-40 text-black-alpha-90" @click="auth.loginWithGoogle()">
              <Image :src="google"></Image>
              <p>Login with Google</p>
            </Button>
            <Button outlined rounded class="border-black-alpha-40 text-black-alpha-90 " @click="auth.loginWithX()">
              <Image :src="x"></Image>
              <p>Login with X</p>
            </Button>
          </div>
          <p class="text-4xl" style="margin-bottom: 3lvh">Or use your email to register</p>
        </div>
      <b class="text-2xl">Personal Info</b>
      <Divider></Divider>
        <form @submit.prevent="submitForm">
          <div class="field flex justify-content-center align-items-center gap-3 text-center">
            <div>
              <label for="name" class="text-xl text-black-alpha-90">Name</label>
              <InputText
                  id="name"
                  type="text"
                  v-model="form.name"
                  placeholder="Name"
                  style="width: 20lvw;"
                  class="my-2"
              />
              <div class="mb-2" v-if="errors.find(e => e.property == 'username')">
                <span v-if="!v$?.form?.name?.required" class="text-red-100">{{errors.find( e=> e.property == 'name')?.message}}</span>
              </div>
            </div>
            <div>
              <label for="lastname" class="text-xl text-black-alpha-90">Lastname</label>
              <InputText
                  id="lastname"
                  type="text"
                  v-model="form.lastname"
                  placeholder="Lastname"
                  style="width: 20lvw;"
                  class="my-2"
              />
              <div class="mb-2" v-if="errors.find(e => e.property == 'username')">
                <span v-if="!v$?.form?.lastname?.required" class="text-red-100">{{errors.find( e=> e.property == 'lastname')?.message}}</span>
              </div>
            </div>
          </div>
            <div>
              <div class="flex flex-column justify-content-center align-items-center">
                <label for="email" class="text-xl text-black-alpha-90">Email</label>
                <InputText
                    id="email"
                    type="email"
                    v-model="form.email"
                    placeholder="Email"
                    class="my-2"
                    style="width: 20lvw"
                />
              </div>
              <div class="mb-2" v-if="errors.find(e => e.property == 'username')">
                <span v-if="!v$?.form?.email?.required" class="text-red-100">{{errors.find( e=> e.property == 'email')?.message}}</span>
              </div>
            </div>
          <b class="text-2xl text-right">User Info</b>
          <Divider></Divider>
          <div>
            <div class="field flex flex-column justify-content-center align-items-center">
              <label for="username" class="text-xl text-black-alpha-90">Username</label>
              <InputText
                  id="username"
                  type="text"
                  v-model="form.username"
                  placeholder="Username"
                  class="my-2"
                  style="width: 20lvw"
              />
            </div>
            <div class="mb-2" v-if="errors.find(e => e.property == 'username')">
              <span v-if="!v$?.form?.username?.required" class="text-red-100">{{errors.find( e=> e.property == 'username')?.message}}</span>
            </div>
          </div>
          <div class="field flex justify-content-center align-items-center gap-3 text-center">
            <div>
              <label for="pass" class="text-xl text-black-alpha-90">Password</label>
              <Password
                  v-model="form.password"
                  placeholder="Password"
                  inputClass="my-2"
                  inputStyle="width: 20lvw;"
              />
            </div>
            <div>
              <label for="confpass" class="text-xl text-black-alpha-90">Confirm Password</label>
              <Password
                  v-model="form.confirmPassword"
                  placeholder="Confirm Password"
                  inputClass="my-2"
                  inputStyle="width: 20lvw;"
              />
              <div class="flex flex-column mb-4" v-if="errors.find(e => e.property == 'confirmPassword')">
                <span class="text-red-100">{{errors.find( e=> e.property == 'confirmPassword')?.message}}</span>
              </div>
            </div>
          </div>

          <div class="field text-center" style="margin-top: 3lvh">
            <Button severity="contrast" type="submit" size="large" outlined rounded class="w-30rem text-black-alpha-90 hover:text-white-alpha-90">Register me!</Button>
          </div>
        </form>
    </div>
  </div>
</template>

<style scoped>
</style>