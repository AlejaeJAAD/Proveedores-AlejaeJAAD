<template>
  <v-form v-model="valido" @submit.prevent="guardar">
    <v-card elevation="12" style="padding: 10px">
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-alert
          :value="alerta2"
          colored-border
          color="red"
          border="top"
          transition="scale-transsition"
          >{{ estatusMensaje }}
          <v-btn @click="alerta2 = false" icon color="red">
            <v-icon>mdi-close-circle</v-icon>
          </v-btn>

          <v-btn
            style="margin-left: 15px"
            color="green"
            @click="cambiarEstatus"
            icon
            ><v-icon>mdi-check-bold</v-icon></v-btn
          >
        </v-alert>
        <v-spacer></v-spacer>

        <v-btn
          :color="estatus ? 'green' : 'red'"
          @click="alertarCambio()"
          class="white--text"
        >
          <span v-if="!estatus">Activar Cuenta</span>
          <span v-else>Desactivar Cuenta</span>
        </v-btn>
      </v-card-actions>
      <v-row>
        <v-col cols="12" md="4">
          <v-text-field
            outlined
            :append-icon="mPW1 ? 'mdi-eye' : 'mdi-eye-off'"
            v-model="passwordAct"
            :type="mPW1 ? 'text' : 'password'"
            :rules="[rules.novacio, rules.longitudPW]"
            @click:append="mPW1 = !mPW1"
            label="Contraseña Actual"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="4">
          <v-text-field
            outlined
            :append-icon="mPW2 ? 'mdi-eye' : 'mdi-eye-off'"
            v-model="passwordNvo"
            :type="mPW2 ? 'text' : 'password'"
            :rules="[rules.novacio, rules.longitudPW]"
            @click:append="mPW2 = !mPW2"
            label="Nueva Contraseña"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="4">
          <v-text-field
            outlined
            v-model="passwordConf"
            type="password"
            :rules="[rules.novacio, rules.longitudPW, rules.match]"
            label="Confirmar Contraseña"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-alert
        v-if="alerta"
        :type="alertType"
        transition="scale-transition"
        :color="msgColor"
        >{{ mensaje }}</v-alert
      >
      <v-divider></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="white--text" color="red">Cancelar</v-btn>
        <v-btn
          class="white--text"
          :disabled="!valido"
          type="submit"
          color="green"
          >Actualizar</v-btn
        >
      </v-card-actions>
    </v-card>
    <v-snackbar
      v-model="snackbar"
      timeout="3000"
      top="top"
      centered
      right="right"
      vertical
      multi-line
    >
      <span style="font-size: large;">{{ snackbarMsg }}</span>
    </v-snackbar>
  </v-form>
</template>

<script>
export default {
  props: ["estatus"],
  name: "Seguridad",
  data() {
    return {
      valido: false,
      mPW1: false,
      passwordAct: "",
      mPW2: false,
      passwordNvo: "",
      passwordConf: "",
      rules: {
        novacio: v => !!v || "No se permiten campos vacios",
        longitudPW: v => v.length >= 8 || "Minimo 8 caracteres",
        match: v => v === this.passwordNvo || "Contraseñas no son iguales"
      },
      alerta: false,
      msgColor: "blue",
      mensaje: "",
      alertType: "info",
      estatusMensaje: "",
      alerta2: false,
      snackbar: false,
      snackbarMsg: ""
    };
  },
  methods: {
    alertarCambio() {
      this.alerta2 = true;
      if (this.estatus) {
        this.estatusMensaje =
          "Esta a punto de desactivar su cuenta, desea continuar?";
      } else {
        this.estatusMensaje = "Su cuenta sera activada. desea continuar?";
      }
    },
    cambiarEstatus() {
      this.$store
        .dispatch("updateStatus", { estatus: !this.estatus })
        .then(res => {
          this.terminarCambio(res);
        });
    },
    terminarCambio(res) {
      this.snackbarMsg = res.data.message;
      this.alerta2 = false;
      this.snackbar = true;
    },
    guardar() {
      const data = {
        id: localStorage.userId,
        oldPassword: this.passwordAct,
        newPassword: this.passwordNvo
      };
      this.$store
        .dispatch("updatePassword", data)
        .then(res => {
          console.log(res);
          if (res.data.error) {
            this.mensaje = res.data.error.mensaje.toString() + ".";
            this.msgColor = "red";
            this.alerta = true;
            this.alertType = "error";
            this.passwordAct = "";
            this.passwordNvo = "";
            this.passwordConf = "";
            setTimeout(() => {
              this.alerta = false;
            }, 3000);
          } else {
            this.mensaje =
              "Se cambio la contraseña, debera iniciar sesion nuevamente";
            this.msgColor = "green";
            this.alertType = "success";
            this.alerta = true;
            setTimeout(() => {
              this.$store.dispatch("logout");
              this.$router.replace("/login");
            }, 1000);
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>

<style scoped></style>
