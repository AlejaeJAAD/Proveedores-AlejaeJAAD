<template>
  <div>
    <v-card width="90%" class="center">
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn @click="nuevoC" color="teal darken-3" class="white--text"
          ><v-icon>mdi-plus</v-icon> Generar codigo de afiliación</v-btn
        >
      </v-card-actions>
      <v-divider></v-divider>
      <v-data-table
        locale="es"
        :headers="headers"
        :items="desserts"
        :items-per-page="5"
        :search="busqueda"
        class="elevation-1"
        hide-default-footer
      >
        <template v-slot:top>
          <v-text-field
            v-model="busqueda"
            label="Buscar"
            class="mx-4"
          ></v-text-field>
        </template>
        <template v-slot:item.acciones="{ item }">
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn @click="edit(item)" color="red">
                <span v-if="false">{{ on }}{{ attrs }}</span>
                <v-icon class="white--text">mdi-account-remove</v-icon>
              </v-btn>
            </template>
            <span>Tooltip</span>
          </v-tooltip>
        </template>
      </v-data-table>
    </v-card>
    <v-dialog v-model="confirmar" width="unset">
      <v-card>
        <v-card-title class="headline" style="word-break: break-word"
          >Desea dar de baja a este usuario de sus clientes?</v-card-title
        >
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red" outlined @click="confirmar = false">
            Cancelar
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" outlined @click="darDeBaja()">
            Aceptar
          </v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="showQr" width="unset">
      <v-card>
        <!--v-img style="margin: 0 auto" :src="qr"> </v-img>
        <div style="text-align: center;font-weight: bold">
          <input id="key" :value="key" disabled />
          <v-btn @click="copy" icon>
            <v-icon>mdi-content-copy</v-icon>
          </v-btn>
        </div-->
        <v-text-field solo v-model="emailAfiliacion"></v-text-field>
        <v-btn @click="afiliar">Solicitar</v-btn>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      emailAfiliacion: "",
      confirmar: false,
      confirmacion: false,
      busqueda: "",
      qr: "",
      key: "",
      showQr: false,
      headers: [
        { text: "Nombre", align: "start", value: "nombre" },
        { text: "Direccion", value: "direccion" },
        { text: "Numero", value: "numero" },
        { text: "Correo", value: "correo" },
        { text: "Acciones", value: "acciones", sortable: false }
      ],
      desserts: [
        {
          nombre: "Jose Lopez",
          direccion: "Jacarandas 99938 Las Bugabilias",
          numero: "8388383030",
          correo: "jlop@gmail.com"
        },
        {
          nombre: "Pedro Armendariz",
          direccion: "Tec de Culiacan",
          numero: "387837833",
          correo: "guillermoCRSA@gmail.com"
        }
      ]
    };
  },
  methods: {
    afiliar() {
      console.log(this.$store.getters.getUser.email);
      if (this.emailAfiliacion !== this.$store.getters.getUser.email) {
        this.$store.dispatch("fetchByEmail", this.emailAfiliacion).then(res => {
          const data = {
            recipientId: res.id,
            aceptado: false
          };
          this.$store
            .dispatch("afiliar", data)
            .then(() => (this.showQr = false));
        });
      }
    },
    edit() {
      this.confirmar = true;
    },
    darDeBaja() {
      this.confirmacion = true;
      this.confirmar = false;
    },
    nuevoC() {
      const myId = "hc6e5d8a78ahc8790s";
      const token = "zte3547xkmmd";
      this.key = myId + "-" + token;
      this.qr =
        "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" +
        this.key;
      this.showQr = true;
    },
    copy() {
      let toCopy = document.querySelector("#key");
      toCopy.setAttribute("type", "text");
      toCopy.select();
      toCopy.setSelectionRange(0, 99999);

      document.execCommand("copy");
    }
  }
};
</script>

<style scoped>
.center {
  margin: 2% auto;
}
</style>
