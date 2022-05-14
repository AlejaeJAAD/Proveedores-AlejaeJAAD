<template>
  <div>
    <v-card width="90%" class="center">
      <v-card-actions>
        <v-row v-if="!$store.getters.getIsMobile">
          <v-col cols="12" md="3">
            <v-avatar class="bS1" size="35"> </v-avatar>
            <v-card-text>Stock menor a 10</v-card-text>
          </v-col>
          <v-col cols="12" md="3">
            <v-avatar class="bS2" size="35"> </v-avatar>
            <v-card-text>Stock menor a 20</v-card-text>
          </v-col>
          <v-col cols="12" md="3">
            <v-avatar class="bS3" size="35"> </v-avatar>
            <v-card-text>Stock menor a 30</v-card-text>
          </v-col>
          <v-spacer></v-spacer>
          <v-col cols="12" md="3">
            <v-btn @click="agProd = true" color="teal darken-3">
              <v-icon>mdi-plus</v-icon>
              Agregar Producto
            </v-btn>
          </v-col>
        </v-row>
        <v-expansion-panels v-else>
          <v-expansion-panel>
            <v-expansion-panel-header>Referencia</v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-row>
                <v-col cols="12" md="3">
                  <v-avatar class="bS1" size="35"> </v-avatar>
                  <v-card-text>Stock menor a 10</v-card-text>
                </v-col>
                <v-col cols="12" md="3">
                  <v-avatar class="bS2" size="35"> </v-avatar>
                  <v-card-text>Stock menor a 20</v-card-text>
                </v-col>
                <v-col cols="12" md="3">
                  <v-avatar class="bS3" size="35"> </v-avatar>
                  <v-card-text>Stock menor a 30</v-card-text>
                </v-col>
                <v-spacer></v-spacer>
                <v-col cols="12" md="3">
                  <v-btn @click="agProd = true" color="teal darken-3">
                    <v-icon>mdi-plus</v-icon>
                    Agregar Producto
                  </v-btn>
                </v-col>
              </v-row>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-card-actions>
      <v-divider></v-divider>
      <v-data-table
        locale="es"
        :headers="headers"
        :items="products"
        :search="busqueda"
        :items-per-page="10"
        multi-sort
        class="elevation-1 scroll"

      >
        <template v-slot:body="{ items }">
          <tbody>
            <tr
              v-for="(item, value) in items"
              :key="value"
              :class="[
                { bS1: item.stock <= 30 },
                { bS2: item.stock <= 20 },
                { bS3: item.stock <= 10 }
              ]"
            >
              <td>
                {{ item.nombre }}
              </td>
              <td>
                {{ item.descripcion }}
              </td>
              <td>
                {{ item.costoUnitario }}
              </td>
              <td>
                {{ item.stock }}
              </td>
              <td>
                {{ item.costoTotal }}
              </td>
              <td>
                {{ item.precioClienteU }}
              </td>
              <td>
                {{ item.ganancia }}
              </td>
              <td>
                <v-btn @click="edit(item)" color="orange darken-1" small>
                  <span v-if="false">{{ on }}{{ attrs }}</span>
                  <v-icon class="white--text">mdi-pencil</v-icon>
                </v-btn>
              </td>
              <td>
                <v-switch
                  @change="actualizaEstatus(item)"
                  v-model="item.estatus"
                  color="success"
                >
                  <span v-if="false">{{ on }}{{ attrs }}</span>
                </v-switch>
              </td>
            </tr>
          </tbody>
        </template>

        <template v-slot:top>
          <v-text-field v-model="busqueda" label="Buscar" class="mx-4">
          </v-text-field>
        </template>
      </v-data-table>
    </v-card>

    !-
    <v-dialog width="unset">
      /* v-model="confirmar" */
      <v-card>
        <v-card-title class="headline" style="word-break: break-word"
          >Desea dar de baja a este usuario de sus clientes?</v-card-title
        >
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="red" outlined>
            /* @click="confirmar = false" */ Cancelar
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" outlined @click="darDeBaja()">
            Aceptar
          </v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="agProd" width="750px" persistent>
      <v-card style="padding: 20px">
        <v-card-title v-if="!editar" style="font-size: x-large"
          >Nuevo Producto</v-card-title
        >
        <v-card-title v-else style="font-size: x-large"
          >Editar Producto</v-card-title
        >
        <v-divider></v-divider>
        <v-card-text>
          <v-form v-model="valido" ref="form">
            <v-row>
              <v-col cols="12" md="2">


                <v-avatar  color="grey lighten" size="120">
                  <v-img :src="imagen ? imagen : 'https://cdn1.neolith.com/wp-content/uploads/wp-imk/colecciones/colorfeel/arctic-white/presentaciones/arctic-white-500x500.jpg' "></v-img>
                </v-avatar>

              </v-col>
              <v-col cols="12"  offset-md="1"  md="9">
                <v-file-input
                        style="margin-top: 20px"
                        outlined
                        v-model="file"
                        placeholder="Imagen"
                        solo

                ></v-file-input>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  outlined
                  :readonly="editar"
                  :clearable="!editar"
                  label="Nombre de producto:"
                  :rules="[rules.noVacio]"
                  v-model="nombre"
                >
                </v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  outlined
                  clearable
                  label="Descripcion de producto:"
                  v-model="descripcion"
                  :rules="[rules.noVacio]"
                >
                </v-text-field>
              </v-col>
              <v-col cols="12" md="6">
                <v-text-field
                  outlined
                  label="Costo por unidad"
                  :rules="[rules.max, rules.min]"
                  v-model.number="cu"
                >
                </v-text-field>
              </v-col>
              <v-col cols="12" md="6">
                <v-text-field
                  outlined
                  label="Stock"
                  :rules="[rules.max, rules.min]"
                  v-model.number="stock"
                >
                </v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  outlined
                  label="Precio al Cliente"
                  placeholder="x Unidad"
                  type="number"
                  :rules="[rules.max, rules.min]"
                  v-model.number="precio"
                >
                </v-text-field>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions v-if="!editar">
          <v-spacer></v-spacer>
          <v-btn color="red" class="white--text" @click="cancelar"
            >Cancelar</v-btn
          >
          <v-btn color="green" class="white--text" @click="agregar"
            >Agregar</v-btn
          >
        </v-card-actions>
        <v-card-actions v-else>
          <v-spacer></v-spacer>
          <v-btn color="red" class="white--text" @click="cancelar"
            >Cancelar</v-btn
          >
          <v-btn color="green" class="white--text" @click="actualizar"
            >Actualizar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-snackbar v-model="snackbar" timeout="5000" bottom>
      Estatus Actualizado
    </v-snackbar>
  </div>
</template>

<script>
import Axios from "axios";

export default {
  name: "Inventario",
  data() {
    return {
      file:null,
      imagen: '',
      snackbar: false,
      editar: false,
      aEditar: {},
      agProd: false,
      busqueda: "",
      nombre: "",
      descripcion: "",
      cu: 0,
      ct: 0,
      stock: 0,
      precio: 0,
      valido: false,
      rules: {
        noVacio: v => !!v || "No se permiten campos vacios",
        max: v => v <= 9999 || "No se permiten valores mayor a 9999",
        min: v => v > 0 || "No se permiten valores menores a 1"
      },
      headers: [
        { text: "Nombre", align: "start", value: "nombre" },
        { text: "Descripcion", value: "descripcion" },
        { text: "Costo Unitario", value: "costoUnitario" },
        { text: "Stock", value: "stock" },
        { text: "Costo Total", value: "costoTotal" },
        { text: "Precio al Cliente x Unidad", value: "precioClienteU" },
        { text: "Ganancia esperada", value: "ganancia" },
        { text: "Acciones", value: "acciones", sortable: false },
        { text: "Estatus", value: "estatus", sortable: false }
      ]
    };
  },
  methods: {
    edit(item) {
      this.agProd = true;
      this.agProd = true
      this.imagen = item.imagen;
      this.aEditar = item;
      this.editar = true;
      this.nombre = item.nombre;
      this.descripcion = item.descripcion;
      this.cu = item.costoUnitario;
      this.stock = item.stock;
      this.precio = item.precioClienteU;
    },
    actualizar() {
      const data = this.aEditar;
      data.descripcion = this.descripcion;
      data.costoUnitario = this.cu;
      data.stock = this.stock;
      data.precioClienteU = this.precio;
      data.updated = new Date().toISOString();
      data.costoTotal = this.cu * this.stock;
      data.ganancia = this.precio * this.stock - this.cu * this.stock;

      this.imgToImgur(this.file).then((res)=>{
        data.imagen=res;
        console.log(data.imagen);
        if (this.valido) {
          this.$store.dispatch("updateProduct", data);
        }
        this.cancelar();
      })

    },
    agregar() {
      const nuevoProducto = {
        nombre: this.nombre,
        idProveedor: localStorage.userId,
        descripcion: this.descripcion,
        costoUnitario: this.cu,
        stock: this.stock,
        costoTotal: this.cu * this.stock,
        precioClienteU: this.precio,
        ganancia: this.precio * this.stock - this.cu * this.stock,
        created: new Date().toISOString(),
        updated: new Date().toISOString(),
        estatus: true
      };
      this.imgToImgur(this.file).then((res)=>{
         nuevoProducto.imagen=res.toString();
          if (this.valido) {
            this.$store.dispatch("createProduct", nuevoProducto);
          }
          this.cancelar();
        })

    },
    cancelar() {
      this.agProd = false;
      this.$refs.form.reset();
      this.editar = false;
    },
    actualizaEstatus(item) {
      const tr = true;
      const fl = false;
      item.estatus = item.estatus ? tr : fl;
      this.$store.dispatch("updateProduct", item).then(() => {
        this.snackbar = true;
      });
    },
    cargaImagen(file) {
      try {

        const lector = new FileReader();
        lector.readAsDataURL(file);
        lector.onload = event => {
          this.imagen = event.target.result;
        };

      } catch (e) {
        console.log(e);
      }
    },
    async imgToImgur(img){
      const CLIENT_ID = '2d5a7741f840465';
      //const KEY = '36426f69134dd7675084db7dec3e0e40eb5be67e';
      const URL = 'https://api.imgur.com/3/'

      const data = new FormData();
      data.append('image',img);
      return Axios.post(URL, data,{
        headers:{
          Authorization: `Client-ID ${CLIENT_ID}`
        }
      }).then((res) => {
        this.imagen=res.data.data.link;
        return res.data.data.link;
      })
    },
  },
  computed: {
    products() {
      return this.$store.getters.getProducts;
    }
  },
  watch: {},
  file() {
      this.cargaImagen(this.file);
    },
};
</script>

<style scoped>
.scroll{
    overflow-y: auto !important;
    max-height: 75vh !important;
  }

.center {
  margin: 2% auto;
}
.bS1 {
  background-color: rgba(252, 63, 40, 0.1);
}
.bS2 {
  background-color: rgba(252, 63, 40, 0.3);
}
.bS3 {
  background-color: rgba(252, 63, 40, 0.6);
}
</style>

/* products: [ { nombre: "Tomate", descripcion: "xKg", cu: 12, stock: 154, ct:
0, pcu: 20 }, { nombre: "Elote", descripcion: "xPza", cu: 500, stock: 154, ct:
0, pcu: 600 } ] */
