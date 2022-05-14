import Vue from "vue";
import Vuex from "vuex";
import authHandler from "./modules/authHandler";
import userHandler from "./modules/userHandler";
import productHandler from "./modules/productHandler";
import shoppingCart from "./modules/shoppingCart";
import { isMobile } from 'mobile-device-detect'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isMobile,
    productos:[],
  },
  mutations: {
    pushProducto(state,producto){
      let productos = state.productos;
      productos.push(producto);
      state.productos = productos;
    }
  },
  actions: {
    //eslint-disable-next-line
    encrypt({},toEncrypt){
      return new Promise(resolve => {
        var CryptoJS = require('crypto-js')
        const crypt = CryptoJS.AES.encrypt(toEncrypt,'My Secret Passphrase').toString();
        resolve(crypt);
      })
    },
    //eslint-disable-next-line
    decrypt({},toDecrypt) {
      return new Promise(resolve => {
        var CryptoJS = require('crypto-js')
        resolve( CryptoJS.AES.decrypt(toDecrypt, 'My Secret Passphrase').toString(CryptoJS.enc.Utf8));
      })
    },
    productosAOrdenar({commit},producto){
      console.log(producto)
      commit('pushProducto',producto);
    }
  },
  getters: {
    getIsMobile(state){
      return state.isMobile;
    },
    getProductos(state){
      return state.productos;
    }
  },
  modules: {
    authHandler,
    userHandler,
    productHandler,
    shoppingCart,
  }
});
