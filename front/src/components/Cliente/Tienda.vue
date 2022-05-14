<template>
    <v-container>
        <h2>Productos: </h2>
        <v-divider></v-divider>
        <v-card flat class="scroll"  height="72vh" >
            <v-row>
                <v-col v-for="(product,index) in products" v-bind:key="product.id" cols="12" sm="6" md="4" lg="3"  >
                    <pr-cart-card :product="product" :index="index" @agProd="actualizarCarrito($event)"/>
                </v-col>
            </v-row>
        </v-card>
        <v-divider></v-divider>
        <v-card flat>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="secondary" @click="shCarrito=false; shCarrito=true" x-large>
                    {{shCarrito}}
                    Ver Carrito
                    <v-icon>mdi-cart-outline</v-icon>
                </v-btn>
            </v-card-actions>
        </v-card>

        <v-navigation-drawer
                v-if="shCarrito"
                :width="$store.getters.getIsMobile ? '100%' : '40%'"
                style="height: 100vh !important;"
                v-model="shCarrito"
                class="drawer"
                app
                temporary
                floating
                right>
            <cart

            />
            <template  v-slot:append>

                <div class="pa-2">
                    <v-btn height="6vh" dark block>Comprar</v-btn>
                </div>
                <div class="pa-2" >
                    <v-btn dark block @click="shCarrito=!shCarrito">Regresar</v-btn>
                </div>
            </template>
        </v-navigation-drawer>

    </v-container >
</template>

<script>

import PrCartCard from "./PrCartCard";
import Cart from "./Cart";

export default {
    components: {Cart, PrCartCard},
    name: "SuppProds",
    data(){
        return{
            qty:0,
            carrito:[],
            shCarrito:false,
            total: 0,
        }
    },
    methods:{
        actualizarCarrito(prod){
            this.$store.dispatch('pushIntoCart',prod);
        },
        calculaTotal(carrito){
            let total=0;
            for (let i = 0; i < carrito.length ; i++) {
                total += (carrito[i].product.precioClienteU * carrito[i].cantidad)
            }
            this.total = total;
        }
    },
    computed:{
        products(){
            return this.$store.getters.getSupplierProducts || [];
        } ,
    },
    created() {
        if(this.$route.params.idP) {
            this.$store.dispatch('decrypt',this.$route.params.idP).then(res=>{
                this.$store.dispatch('fetchSupplierProducts',res);
                this.$store.dispatch('cleanCart');
            });

        }
    },
}
</script>

<style scoped>
.scroll{
    overflow-y: auto !important;
    overflow-x: hidden;
    padding: 10px;
}
.right-border{
    border-right: 1px solid #dedede !important;
}
.top-border{
    border-top: 1px solid #dedede !important;
}

.cent{
    margin: 0 auto !important;
}
.app-bar {
    height: 3rem;
    background-color: rgb(31 30 30) !important;
}

.drawer {
    background-color: #fcfcfc !important;
    z-index: 9998 !important;
    position: absolute !important;
}

</style>
