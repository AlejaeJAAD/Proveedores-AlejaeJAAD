import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import Axios from "axios";

import AOS from "aos";
import "aos/dist/aos.css";
import { isMobile } from 'mobile-device-detect'

Axios.defaults.baseURL = isMobile ? "http://192.168.1.64:5000" : "http://localhost:5000";
console.log(Axios.defaults.baseURL)
Vue.config.productionTip = false;


/* eslint-disable no-unused-vars */
export const proveedor = 'b2dcdcf5-5065-4b5f-9932-b9d4e7918914';
export const cliente = 'ff403a39-10fd-41bb-9124-18c101ce8bf1';
export const admin = '520cf522-f89a-4744-beb5-fef492770829';




new Vue({
  router,
  store,
  vuetify,
  render: h => h(App),
  created() {
    AOS.init();
  },
  isMobile,
}).$mount("#app");
