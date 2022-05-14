<template>
  <v-form>
    <v-card style="padding: 10px" elevation="12">
      <v-row>
        <v-col cols="4" offset="4" offset-md="0" md="2">
          <v-avatar color="grey lighten" size="120">
            <v-img :src="imagen"></v-img>
          </v-avatar>
        </v-col>
        <v-col cols="12" md="4">
          <v-file-input
            style="margin-top: 20px"
            solo
            v-model="file"
            placeholder="Logo"
            :disabled="!editable"
          ></v-file-input>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            style="margin-top: 20px"
            solo
            label="Sitio Web"
            v-model="sitioWeb"
            :readonly="!editable"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="3">
          <v-text-field
            solo
            label="Nombre Empresa"
            v-model="nombreEmpresa"
            :readonly="!editable"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field
            solo
            label="Telefono Empresa"
            v-model="telefonoEmpresa"
            :readonly="!editable"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field
            solo
            label="RFC"
            v-model="RFC"
            :readonly="!editable"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="2">
          <v-btn color="green" class="white-text">Verificar</v-btn>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <v-textarea
            style="word-break: break-word"
            label="Acerca de:"
            no-resize
            solo
            rows="3"
            v-model="bio"
            counter
            :readonly="!editable"
          ></v-textarea>
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <v-row>
        <v-col cols="12">
          <v-row>
            <v-col cols="12" md="2">
              <v-text-field
                @click:append="verificarCP"
                type="number"
                solo
                :readonly="!editable"
                label="Codigo Postal"
                :append-icon="icono"
                v-model.number="cp"
                hint="Podras editar tu domicilio al verficiar el codigo postal!"
                persistent-hint
                :color="color"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            :readonly="sinCP"
            label="Estado"
            v-model="estado"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            :readonly="sinCP"
            label="Ciudad"
            v-model="ciudad"
          ></v-text-field>
        </v-col>

        <v-col cols="12" md="5">
          <v-text-field
            :readonly="sinCP"
            label="Calle"
            v-model="calle"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="2">
          <v-text-field
            :readonly="sinCP"
            label="Numero"
            v-model.number="numero"
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="5">
          <v-select
            :readonly="sinCP"
            :items="colonias"
            :value="colonia"
            label="Colonia"
            v-model="colonia"
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
          La informacion de la empresa ha sido actualizada
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
  name: "InformacionEmpresa",
  data() {
    return {
      editable: false,
      sinCP: true,
      imagen: "",
      file: null,
      estados: [],
      estado: "",
      ciudad: "",
      colonia: "",
      calle: "",
      numero: "",
      bio: "",
      RFC: "",
      telefonoEmpresa: "",
      nombreEmpresa: "",
      sitioWeb: "",
      colonias: [],
      cp: "",
      color: "primmary",
      icono: "mdi-check",
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
      } catch (e) {
        console.log(e);
      }
    },
    verificarCP() {
      console.log("Hola");
      return Axios.post(
        "https://api.copomex.com/query/info_cp/" + this.cp + "?token=47b8b142-c0dc-4767-b207-662d6e6064fa"
      ).then(res => {
        console.log(res.data);
        this.colonias = [];
        for (let i = 0; i < res.data.length; i++) {
          const info = res.data[i].response;
          this.ciudad = info.ciudad;
          this.estado = info.estado;
          this.colonia = res.data[0].response.asentamiento;
          this.colonias.push(info.asentamiento);
        }
        this.sinCP = false;
        this.colonia = this.myUser.colonia;
      });
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
      if (this.myUser.logo != null) {
        this.imagen = this.myUser.logo;
      }
      this.sitioWeb = this.myUser.sitioWeb;
      this.nombreEmpresa = this.myUser.nombreEmpresa;
      this.telefonoEmpresa = this.myUser.telefonoEmpresa;
      this.RFC = this.myUser.rfc;
      this.bio = this.myUser.bio;
      this.cp = this.myUser.cp;
      this.estado = this.myUser.estado;
      this.ciudad = this.myUser.ciudad;
      this.calle = this.myUser.calle;
      this.numero = this.myUser.numero;
      this.verificarCP().then(() => {
        this.sinCP = true;
      });
      this.sinCP = true;
    },
    cancelar() {
      this.editable = !this.editable;
      this.castUser();
    },
    actualizar() {
      const userIn = this.myUser;
      userIn.nombreEmpresa = this.nombreEmpresa;
      userIn.sitioWeb = this.sitioWeb;
      userIn.telefonoEmpresa = this.telefonoEmpresa;
      userIn.rfc = this.RFC;
      userIn.bio = this.bio;
      userIn.cp = this.cp;
      userIn.estado = this.estado;
      userIn.ciudad = this.ciudad;
      userIn.calle = this.calle;
      userIn.numero = this.numero;
      userIn.colonia = this.colonia;

      userIn.password = "aji3djdkl";
      if (this.myUser.logo !== this.imagen) {
        this.imgToImgur(this.file)
          .then(res => {
            userIn.logo = res.toString();
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
  },
  created() {
    Axios.get("https://api.copomex.com/query/get_estados?token=47b8b142-c0dc-4767-b207-662d6e6064fa").then(res => {
      this.estados = res.data.response.estado;
    });
  }
};
</script>

<style scoped></style>
