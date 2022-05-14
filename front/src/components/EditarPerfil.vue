<template>
  <v-card class="cont" width="95%" elevation="20">
    <v-row style="margin: 0">
      <v-col cols="12" md="3">
        <v-card elevation="12" height="100%">
          <v-card-title
            class="option"
            :class="{ activa: opcion === 1 }"
            @click="opcion = 1"
            >Informacion Personal
            <v-spacer></v-spacer>
            <v-icon v-if="opcion === 1" color="primary" size="50"
              >mdi-chevron-right</v-icon
            >
          </v-card-title>

          <v-divider></v-divider>
          <v-card-title
          v-if="rol==proveedor"
            class="option"
            :class="{ activa: opcion === 2 }"
            @click="opcion = 2"
            >Informacion Empresa
            <v-spacer></v-spacer>
            <v-icon v-if="opcion === 2" color="primary" size="50"
              >mdi-chevron-right</v-icon
            >
          </v-card-title>
          <v-divider></v-divider>
          <v-card-title
            class="option"
            :class="{ activa: opcion === 3 }"
            @click="opcion = 3"
            >Seguridad
            <v-spacer></v-spacer>
            <v-icon v-if="opcion === 3" color="primary" size="50"
              >mdi-chevron-right</v-icon
            >
          </v-card-title>
          <v-divider></v-divider>
        </v-card>
      </v-col>
      <v-col cols="12" md="9">
        <personal v-if="opcion === 1" :my-user="myUser" />
        <informacion-empresa v-if="opcion === 2" :my-user="myUser" />
        <seguridad v-if="opcion === 3" :estatus="myUser.estatus" />
      </v-col>
    </v-row>
  </v-card>
</template>

<script>
import Personal from "./EditarPerfil/Personal";
import InformacionEmpresa from "./EditarPerfil/InformacionEmpresa";
import Seguridad from "./EditarPerfil/Seguridad";
import { proveedor, cliente, admin } from '../main';
export default {
  name: "EditarPerfil",
  components: { Seguridad, InformacionEmpresa, Personal },
  data() {
    return {
      proveedor,
      cliente,
      admin,
      opcion: 1
    };
  },
  computed: {
    myUser() {
      const myUser = this.$store.getters.getUser;
      return myUser ? myUser : {};
    },
    rol(){
      return this.$store.getters.getRol;
    }
  }
};
</script>

<style scoped>
.cont {
  margin: 10px auto;
}
.option:hover {
  background-color: lightgrey;
  cursor: pointer;
  font-weight: bolder;
}
.activa {
  background-color: lightgrey;
  cursor: pointer;
  font-weight: bolder;
}
</style>
