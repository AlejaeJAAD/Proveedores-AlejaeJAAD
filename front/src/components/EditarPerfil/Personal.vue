<template>
  <v-form>
    <v-card style="padding: 10px" elevation="12">
      <v-row>
        <v-col cols="12" offset-md="0" md="2">
          <v-avatar color="grey lighten" size="120">
            <v-img :src="imagen"></v-img>
          </v-avatar>
        </v-col>
        <v-col cols="12" md="4">
          <v-file-input
            style="margin-top: 20px"
            :disabled="!editable"
            outlined
            v-model="file"
          ></v-file-input>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="3">
          <v-text-field
            solo
            readonly
            v-model="username"
            label="Nombre de Usuario"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <v-row>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="nombres"
            :readonly="!editable"
            solo
            label="Nombre"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="apellidos"
            :readonly="!editable"
            solo
            label="Apellido"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="4">
          <v-menu
            ref="menu"
            v-model="menu"
            :close-on-content-click="false"
            :return-value.sync="fNacimiento"
            transition="scale-transition"
            offset-y
            disabled
            min-width="290px"
          >
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="fNacimiento"
                label="Fecha De Nacimiento"
                readonly
                solo-inverted
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="fNacimiento"
              no-title
              scrollable
              locale="es"
            >
              <v-spacer></v-spacer>
              <v-btn text color="red" @click="menu = false">Cancelar</v-btn>
              <v-btn text color="green" @click="$refs.menu.save(fNacimiento)"
                >Ok</v-btn
              >
            </v-date-picker>
          </v-menu>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="telefono"
            :readonly="!editable"
            label="Telefono"
            solo
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="email"
            readonly
            label="Email"
            solo-inverted
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="4">
          <v-select
            label="Genero"
            v-model="genero"
            :readonly="!editable"
            solo
            :items="['Masculino', 'Femenino', 'Otro']"
          ></v-select>
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="orange"
          v-if="!editable"
          class="white-text"
          @click="editable = !editable"
          >Editar</v-btn
        >
        <v-btn color="red" v-if="editable" class="white-text" @click="cancelar"
          >Cancelar</v-btn
        >
        <v-btn
          color="green"
          v-if="editable"
          class="white-text"
          @click="actualizar"
          >Actualizar</v-btn
        >
      </v-card-actions>
    </v-card>
    <v-dialog v-model="dialog" max-width="290">
      <v-card>
        <v-card-title class="headline"
          ><v-icon>mdi-alert-circle</v-icon></v-card-title
        >

        <v-card-text>
          La informacion del usuario ha sido actualizada
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">
            Aceptar
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-form>
</template>

<script>
import Axios from "axios";
export default {
  props: ["myUser"],
  name: "Cuenta",
  data() {
    return {
      editable: false,
      imagen: "",
      file: null,
      fNacimiento: "",
      username: "",
      nombres: "",
      apellidos: "",
      telefono: "",
      email: "",
      genero: "",
      menu: false,
      dialog: false
    };
  },
  methods: {
    cargaImagen(file) {
      try {
        const imagen = file;
        const lector = new FileReader();
        lector.readAsDataURL(imagen);
        lector.onload = event => {
          this.imagen = event.target.result;
        };
        console.log(file);
      } catch (e) {
        console.log(e);
      }
    },
    async imgToImgur(img) {
      // eslint-disable-next-line no-unused-vars
      const CLIENT_ID = "cefbf1518d98780";
      //const KEY = "4a0fadb8ee04d77207ac192b819478a69504b9f7";
      const URL = "https://api.imgur.com/3/image";

      const data = new FormData();
      data.append("image", img);
      return Axios.post(URL, data, {
        headers: {
          Authorization: `Client-ID ${CLIENT_ID}`
        }
      }).then(res => {
        // eslint-disable-next-line no-undef
        //console.log(res.data.data.link);
        this.imagen = res.data.data.link;
        return res.data.data.link;
      });
    },
    castUser() {
      if (this.myUser.imagen != null) {
        this.imagen = this.myUser.imagen;
      }
      this.username = this.myUser.username;
      this.nombres = this.myUser.nombres;
      this.apellidos = this.myUser.apellidos;
      this.fNacimiento = new Date(this.myUser.fechaNacimiento)
        .toISOString()
        .substring(0, 10);
      this.telefono = this.myUser.telefono;
      this.email = this.myUser.email;
      this.genero = this.myUser.genero;
    },
    cancelar() {
      this.editable = !this.editable;
      this.castUser();
    },
    actualizar() {
      const userIn = this.myUser;
      userIn.nombres = this.nombres;
      userIn.apellidos = this.apellidos;
      userIn.telefono = this.telefono;
      userIn.genero = this.genero;
      userIn.email = this.email;
      userIn.password = "aji3djdkl";
      if (this.myUser.imagen !== this.imagen) {
        this.imgToImgur(this.file)
          .then(res => {
            userIn.imagen = res.toString();
            this.$store.dispatch("updateUser", userIn).then(() => {
              this.editable = !this.editable;
              this.dialog = true;
              setTimeout(() => {
                this.dialog = false;
              }, 5000);
            });
          })
          .catch(() => {
            this.cancelar();
          });
      } else {
        this.$store.dispatch("updateUser", userIn).then(() => {
          this.editable = !this.editable;
          this.dialog = true;
          setTimeout(() => {
            this.dialog = false;
          }, 5000);
        });
      }
    },
    comparador(a, b) {
      console.log("Entro");
      let arA = [];
      Object.entries(a).forEach(([, value]) => {
        arA.push(value);
      });
      let arB = [];
      Object.entries(b).forEach(([, value]) => {
        arB.push(value);
      });

      for (let i = 0; i < arA.length; i++) {
        console.log(arA[i], arB[i]);
        if (arA[i] !== arB[i]) {
          console.log("entro", arA[i], arB[i]);
          return false;
        }
      }
    }
  },
  watch: {
    file() {
      this.cargaImagen(this.file);
    },
    myUser() {
      if (this.myUser.username) {
        this.castUser();
      }
    }
  },
  mounted() {
    if (this.myUser.username) {
      this.castUser();
    }
  }
};
</script>

<style scoped></style>
