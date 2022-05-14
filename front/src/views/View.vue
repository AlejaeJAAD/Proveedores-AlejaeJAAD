<template>
  <v-main class="nomar scroll-cont">
    <v-app-bar app color="primary" dark elevation="0" dense>
      <v-spacer></v-spacer>
      <v-btn text elevation="0" height="100%">
        <v-icon>mdi-login</v-icon>
        Log In
      </v-btn>
    </v-app-bar>

    <v-card v-if="!cliente">
      <v-row>
        <v-col cols="12" style="padding-top: 0; padding-bottom: 0 ">
          <v-card color="orange lighten-5" height="100vh" class="">
            <v-row class=" fill-height">
              <v-col cols="12">
                <v-btn
                  v-if="!cliente"
                  @click="cliente = !cliente"
                  color="primary"
                  >Soy Cliente</v-btn
                >
              </v-col>
              <v-col cols="12" md="6">
                <v-img
                  src="../assets/logo.png"
                  max-height="25%"
                  contain
                ></v-img>
              </v-col>
              <v-col cols="12" md="6">
                <v-card class="transparent white--text" flat>
                  <v-card-title class="txt-xl"
                    >¿Quieres gestionar tus envios de una manera sencilla y con
                    multiples ventajas?
                  </v-card-title>
                  <v-card-subtitle class="white--text">
                    Unete a la plataforma y se parte de la experiencia!
                  </v-card-subtitle>
                  <v-card-actions>
                    <v-btn color="orange lighten-1" class="white--text"
                      >Registrate</v-btn
                    >
                  </v-card-actions>
                </v-card>
              </v-col>
            </v-row>
          </v-card>
        </v-col>
      </v-row>
    </v-card>

    <v-card>
      <v-row>
        <v-col cols="12" style="padding-top: 0; padding-bottom: 0 ">
          <v-card color="red lighten-4" height="200vh" class="">
            <!-- <v-img  src="../assets/suppliers1.jpg"
                    max-height="100vh"
                    max-widht="100vh"
            >
            </v-img>
            <v-btn v-if="!cliente" @click="cliente=!cliente" color="primary">
                Unete a nuestro equipo de proveedores
            </v-btn> -->
            <div class="container">
              <v-img
                src="../assets/unnamed.jpg"
                height="100vh"
                widht="100vh"
              >
              </v-img>
              <v-btn
                v-if="!cliente"
                @click="cliente = !cliente"
                color="primary"
                class="container"
              >
                Unete a nuestro equipo de proveedores
              </v-btn>
            </div>

            <v-row>
              <v-col>
                <div class="container">
                  <v-card>
                    <v-row>
                      <v-col cols="12" md="3" style="margin: 75px">
                        <v-card elevation="0" width="100%" height="100%">
                          <v-img
                            src="../assets/logo.png"
                            width="100%"
                            class="centered"
                          ></v-img>
                          <span class="center" style="word-break: break-word">
                            Controla tus pedidos siendo lo mas pro y verga del
                            mundo
                          </span>
                        </v-card>
                      </v-col>

                      <v-col cols="12" md="3" style="margin: 70px">
                        <v-card elevation="0" width="100%" height="100%">
                          <v-img
                            src="../assets/logo.png"
                            width="100%"
                            class="center"
                          ></v-img>
                          <span class="center" style="word-break: break-word">
                            Controla tus pedidos siendo lo mas pro y verga del
                            mundo
                          </span>
                        </v-card>
                      </v-col>

                      <v-col cols="12" md="3" style="margin: 70px">
                        <v-card elevation="0" width="100%" height="100%">
                          <v-img
                            src="../assets/logo.png"
                            width="100%"
                            class="center"
                          ></v-img>
                          <span class="center" style="word-break: break-word">
                            Controla tus pedidos siendo lo mas pro y verga del
                            mundo
                          </span>
                        </v-card>
                      </v-col>
                    </v-row>
                  </v-card>
                </div>
              </v-col>
            </v-row>
          </v-card>
        </v-col>

        <!-- AQUI ME APESTA LA VERGA MACIZO -->
      </v-row>
    </v-card>
  </v-main>
</template>

<script>
export default {
  name: "Landing",
  data() {
    return {
      cliente: false,
      registroCTE: false,
      registroProv: false
    };
  }
};
</script>

<style scoped>
.nomar {
  margin-top: -3rem;
}

</style>
