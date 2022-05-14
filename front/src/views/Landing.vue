<template>
  <v-main class="nomar">
    <v-spacer></v-spacer>
    <v-app-bar app dense dark elevation="0">
      <v-btn text>Quienes somos?</v-btn>
      <v-btn text>Quienes hacemos?</v-btn>
      <v-btn text>Registrate</v-btn>
      <v-btn text>Soporte</v-btn>
      <v-spacer></v-spacer>
      <v-btn @click="$router.push('/login')" text elevation="0" height="100%">
        <v-icon>mdi-lock-open</v-icon>
        Iniciar Sesion
      </v-btn>
    </v-app-bar>

    <quienes-somos></quienes-somos>
  </v-main>
</template>

<script>
import QuienesSomos from "../components/Landing/QuienesSomos";

export default {
  name: "Landing",
  components: { QuienesSomos }
};
</script>

<style scoped>
.color {
  background-color: #1f1e1e !important;
}

.nomar {
  margin-top: -3rem;
}

#s1 {
  background-color: #ffffff;
}

#s2 {
  background-color: #ecfcec;
}

#s3 {
  background-color: #0d9bd0;
}

.center {
  margin: 0 auto;
}

.p-top {
  padding-top: 30px;
}

.img {
  position: absolute !important;
  left: 10%;
  top: 10%;
  border-radius: 8px;
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

.logo-cont {
  position: relative;
  margin: 0 auto;
  width: 40%;
  height: 40%;
  border-radius: 5px;
  border: 3px solid black;
}

.gradient {
  background: rgba(242, 0, 0, 1) !important;
  background: -moz-linear-gradient(
    0deg,
    rgba(242, 0, 0, 0.001) 0%,
    rgba(9, 9, 9, 0.5) 100%
  ) !important;
  background: -webkit-linear-gradient(
    0deg,
    rgba(242, 0, 0, 0.01) 0%,
    rgba(9, 9, 9, 0.5) 100%
  ) !important;
  background: linear-gradient(
    0deg,
    rgba(242, 0, 0, 0.01) 0%,
    rgba(9, 9, 9, 0.5) 100%
  ) !important;
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#f20000", endColorstr="#090909", GradientType=1) !important;
}
</style>
