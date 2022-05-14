<template>
    <v-card flat class="pa-2" >
        <v-row>
            <v-col cols="12">
                <v-row>
                    <v-col cols="12" md="4">
                        <v-avatar tile size="90%">
                            <v-img contain src="https://www.getlivewire.com/wp-content/uploads/2018/10/Your-Logo-here.png" ></v-img>
                        </v-avatar>
                    </v-col>
                    <v-col cols="12" md="6">
                        <p style="font-size: x-large; font-weight: bold">
                            {{cartItem.product.nombre}}asdiasjdjlkjsalkdjalskjdlasjdlajsldjsaldkjdl
                        </p>
                        <p style="font-size: small;color: darkgrey">{{cartItem.product.descripcion}}asdlhaslkjdjhlkasijdlkasjdljasldjasldjlasjdlajdljaslkd</p>
                        <p>Disponibles: {{cartItem.product.stock+cartItem.cantidad}}</p>
                        <div>
                            <v-select v-if="cartItem.product.stock+cartItem.cantidad<=50"
                                      v-model="select"
                                      :items="Array.from({length:(cartItem.product.stock+cartItem.cantidad)},(_,index)=> index+1)"></v-select>
                            <div v-else>
                                <v-text-field
                                        type="number"
                                        :max="cartItem.product.stock+cartItem.cantidad"
                                        min="0"
                                        maxlength="1"
                                        v-model.number.lazy="qty"
                                        @change="checkValue(cartItem.product.stock+cartItem.cantidad,$event)"
                                        @keypress="checkValue(cartItem.product.stock+cartItem.cantidad,$event)"
                                        @keyup="checkValue(cartItem.product.stock+cartItem.cantidad,$event)"
                                        @keydown="checkValue(cartItem.product.stock+cartItem.cantidad,$event)"
                                >
                                </v-text-field>
                            </div>
                        </div>
                    </v-col>
                    <v-col cols="12" md="2">
                        <v-btn @click="$store.dispatch('removeFromCart',cartItem)" icon color="red lighten-2" class="white--text">
                            <v-icon>mdi-delete</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <div style="position: absolute !important; right: 10%; bottom: 10px;">
                    <span style="font-weight: bold">Subtotal: </span>
                    <span>{{cartItem.cantidad*cartItem.product.precioClienteU}} </span>
                    <span style="font-size: small"> MXN.</span>
                </div>
            </v-col>
        </v-row>
        <v-divider></v-divider>
    </v-card>
</template>

<script>
export default {
    props:['cartItem'],
    name: "CartItem",
    data(){
        return{
            select:this.cartItem.cantidad,
            qty:this.cartItem.cantidad,
            mtf:this.cartItem.cantidad>=50,
        }
    },
    methods: {
        getMyArray(cartItem){
            if(cartItem.product.stock+cartItem.cantidad>50){
                let myArray = Array.from({length:49},(_,index)=> (index+1).toString())
                myArray.push('50+');
                return myArray;
            }
        },
        checkValue(a, b) {
            if (b.key === '-' || b.key === '.') {
                b.stopImmediatePropagation();
                b.preventDefault();
                this.qty=0
                return;
            }
            if (this.qty === '' ) {
                b.stopImmediatePropagation();
                b.preventDefault();
                this.qty=0
            }
            if (this.qty > a) {
                b.stopImmediatePropagation();
                b.preventDefault();
                this.qty=a;
                return;
            }
            if(this.qty<0){
                b.stopImmediatePropagation();
                b.preventDefault();
            }
        },
    },
    watch:{
        qty(value){
            if(value<=this.cartItem.product.stock+this.cartItem.cantidad){
                this.$store.dispatch('updateCantidad',{product:this.cartItem,newQty:value});
            }
        },
        select(value){
            this.$store.dispatch('updateCantidad',{product:this.cartItem,newQty:value});
        }
    }
}
</script>

<style scoped>

/deep/ .v-text-field{
    width: 20%;
}
</style>
