<template>
    <div style="margin-top: 3rem">
        <v-card class="text-center transparent" flat v-if="cart<1" color="grey lighten-5">Tu carrito está vacio.
        </v-card>
        <v-card flat v-else>
            <cart-item v-for="cartItem in cart" :cart-item="cartItem" :key="cartItem.indice"/>
        </v-card>

    </div>
</template>

<script>
import CartItem from "./CartItem";
export default {
    components: {CartItem},
    props: ['shCarrito', 'stock'],
    name: "Cart",
    data() {
        return {
            selectedItems: [],
            show: false,
            qty: 0,
        }
    },
    methods: {
        checkValue(a, b) {
            if (b.key === '-' || b.key === '.') {
                this.qty = 0;
                b.preventDefault();
            }
            if (this.qty === '') {
                this.qty = 0;
            }
            if (this.qty > a) {
                this.qty = a;
                b.preventDefault();
            }
        },
    },
    computed:{
        cart(){
            return this.$store.getters.getCurrentCart;
        }
    },
    watch: {
        shCarrito(event) {
            this.show = event;
        },
        show(event) {
            this.$emit('ch-model', event);
            this.qty = 0;
        }
    }
}
</script>

<style scoped>
.app-bar {
    height: 3rem;
    background-color: rgb(31 30 30) !important;
}

.drawer {
    background-color: #fcfcfc !important;
    z-index: 9998 !important;
    position: absolute !important;
}
.marginBottom{
    margin-bottom: 4rem;
}
</style>
