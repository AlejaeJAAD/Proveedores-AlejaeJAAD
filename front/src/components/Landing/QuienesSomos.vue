<template>
  <v-row>
    <v-col cols="12" class="nopad">
      <v-card min-height="50vh">
        <v-row class="fill-height">
          <v-col cols="12" md="4" class="nopad">
            <v-card class="transparent center" flat>
              <v-card-actions>
                <v-spacer></v-spacer>
                <img src="../../assets/logo.png" alt="Logo" />
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-col>
          <v-col cols="12" md="7">
            <v-card>
              <v-card-title class="txt-xl">
                Quienes somos?
              </v-card-title>
              <v-card-text class="txt-l">
                Suppliers es una aplicacion web, que busca facilitar el manejo
                de inventarios, ventas,alcance,clientes y prospecciones de
                productos para proveeedores pequenos y medianos
              </v-card-text>
            </v-card>
          </v-col>
          <v-col cols="1"></v-col>
        </v-row>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "QuienesSomos"
};
</script>

<style scoped>
.nopad {
  padding-top: 0 !important;
  padding-bottom: 0 !important;
}
.center {
  margin: 0 auto;
}
.txt-l {
  font-size: 1.5rem;
  word-break: break-word;
}
.txt-xl {
  font-size: 2rem;
  word-break: break-word;
}
.txt-xxl {
  font-size: 7rem;
  word-break: break-word;
}
</style>
