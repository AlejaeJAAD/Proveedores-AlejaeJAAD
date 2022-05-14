<template>
  <div>
    <v-card flat class="scroll" height="70vh">
      <v-row>
        <v-col
          v-for="(product, index) in products"
          v-bind:key="product.id"
          cols="12"
          sm="6"
          md="4"
          lg="3"
        >
          <pr-cart-card
            :product="product"
            :index="index"
            @agProd="actualizarCarrito($event)"
          />
        </v-col>
      </v-row>
    </v-card>
    <v-divider></v-divider>
    <v-card flat>
      <v-card-actions>
        {{ carrito }}
        <v-spacer></v-spacer>
        <v-btn outlined color="secondary" @click="shCarrito = true" x-large>
            Ver Carrito
            <v-icon>mdi-cart</v-icon>
        </v-btn>

      </v-card-actions>
    </v-card>

      <cart :carrito="carrito" :sh-carrito="shCarrito" @ch-shCarrito:="shCarrito=!shCarrito" @ch-model="shCarrito=$event"/>

    <v-dialog max-width="40%" >
      <v-card>
        <div class="text-center">
          <v-row>
            <v-col cols="4" class="right-border">
              Item:
            </v-col>
            <v-col cols="4">
              Cantidad:
            </v-col>
            <v-col cols="4" class="right-border">
              Precio:
            </v-col>
          </v-row>
          <v-divider></v-divider>
          <v-row class="top-border" v-for="elm in carrito" :key="elm.indice">
            <v-col cols="4" class="right-border">
                {{ elm.product.nombre }}
            </v-col>
            <v-col cols="4" class="right-border">
                {{ elm.cantidad }}
            </v-col>
            <v-col cols="4" class="right-border">
              <div class="text-center">
                ${{ elm.product.precioClienteU * elm.cantidad }} MXN.
              </div>
            </v-col>
          </v-row>
        </div>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <span class="txt-t">Total: ${{ total }}</span>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import Axios from "axios";
import PrCartCard from "./PrCartCard";
import Cart from "@/components/Cliente/Cart";

export default {
  components: {Cart, PrCartCard },
  props: ["idProveedor"],
  name: "SuppProds",
  data() {
    return {
      products: [],
      qty: 0,
      carrito: [],
      shCarrito: false,
      total: 0
    };
  },
  methods: {
    actualizarCarrito(prod) {
      if (this.carrito.length > 0) {
        const a = this.carrito.find(v => v.indice === prod.indice);
        if (!a) {
          this.carrito.push(prod);
        } else {
          this.carrito.find(v => v.indice === prod.indice).cantidad +=
            prod.cantidad;
        }
      } else {
        this.carrito.push(prod);
      }
      this.products[prod.indice].stock =
        this.products[prod.indice].stock - prod.cantidad;
      console.log(this.products[prod.indice]);
    },
    calcularTotal(carrito) {
      let total = 0;
      for (let i = 0; i < carrito.length; i++) {
        total += carrito[i].product.precioClienteU * carrito[i].cantidad;
      }
      this.total = total;
    }
  },
  watch: {
    carrito: {
      handler() {
        this.calcularTotal(this.carrito);
      },
      deep: true
    }
  },
  created() {
    Axios.get("product/byProvider/" + this.idProveedor, {
      headers: {
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then(res => {
      this.products = res.data;
    });
  }
};
</script>

<style scoped>
.scroll {
  overflow-y: auto !important;
  overflow-x: hidden;
  padding: 10px;
}
.right-border {
  border-right: 1px solid #dedede !important;
}
.top-border {
  border-top: 1px solid #dedede !important;
}
.cent {
  margin: 0px auto !important;
}
.app-bar {
    height: 3rem;
    background-color: rgb(31,30,30) !important;
}
.drawer {
    z-index: 9998 !important;
    position: absolute !important;
}
</style>
