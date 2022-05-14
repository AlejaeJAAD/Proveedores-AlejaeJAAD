<template>
    <v-card :disabled="!product.stock>0" class="fill-height">
        <v-list>
            <v-list-item>
                <v-list-item-avatar color="grey" size="90" left>
                    <v-img src="https://d500.epimg.net/cincodias/imagenes/2018/11/13/lifestyle/1542113135_776401_1542116070_noticia_normal.jpg"></v-img>
                </v-list-item-avatar>
                <div>
                    <v-card-title style="word-break: break-word">
                        {{product.nombre}}
                    </v-card-title>
                    <v-spacer></v-spacer>
                    <v-card-subtitle style="word-break: break-word">
                        {{product.descripcion}}asdasdasdadadsasdasdasdasdasda a das da da dad ad ad ad ad ada da da dad
                        a dad a da da
                    </v-card-subtitle>
                </div>
            </v-list-item>
            <v-divider></v-divider>
            <v-card-text>
                <v-row>
                    <v-col cols="12" xl="6">
                        <v-row>
                            <v-col cols="12" xl="5">
                                <span class="txt-t">Precio: </span>
                            </v-col>
                            <v-col cols="12" xl="7">
                                <span class="txt-d">${{product.precioClienteU}} MXN.</span>
                            </v-col>
                        </v-row>
                    </v-col>
                    <v-col cols="12" xl="5" offset-xl="1">
                        <v-row>
                            <v-col cols="12" xl="6">
                                <span class="txt-t">Stock: </span>
                            </v-col>
                            <v-col cols="12" xl="6">
                                <span class="txt-d">{{product.stock}}</span>
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
            </v-card-text>
            <v-divider></v-divider>
        </v-list>
        <v-card-actions v-if="product.stock>0">
            <span class="txt-t" style="margin-right: 10px">Cantidad: </span>
            <v-text-field single-line
                          :max="product.stock"
                          min="0"
                          type="number"
                          :rules="[(v)=>v<=product.stock||'No hay stock suficiente.']"
                          @keypress="checkValue(product.stock,$event)"
                          @keyup="checkValue(product.stock,$event)"
                          @keydown="checkValue(product.stock,$event)"
                          v-model.number="qty">
            </v-text-field>
            <v-spacer></v-spacer>
            <v-btn color="orange" :disabled="qty<=0" class="white--text" @click="agregar(qty,product)">Agregar <v-icon>mdi-cart-plus</v-icon> </v-btn>
        </v-card-actions>
        <v-card-actions v-if="product.stock>0">
            <div style="position: absolute; right: 10px; bottom: 10px">
                <span class="txt-t">Subtotal: ${{product.precioClienteU*qty}}</span>
            </div>
        </v-card-actions>
        <h2 style="color: red"
            class="text-center"
            v-if="!product.stock>0">No disponible</h2>
    </v-card>
</template>

<script>
export default {
    props: ['product','index'],
    name: "PrCartCard",
    data() {
        return {
            qty: 0,
        }
    },
    methods: {
        checkValue(a, b) {
            if (b.key === '-' || b.key === '.') {
                this.qty = 0;
                b.preventDefault();
            }
            if(this.qty===''){
                this.qty = 0;
            }
            if (this.qty > a) {
                this.qty = a;
                b.preventDefault();
            }
        },
        agregar(qty,product){
            this.qty=0;
            this.dialog=true;
            this.$emit('agProd',{indice:this.index,cantidad:qty,product});
        }
    },
    watch: {}
}
</script>

<style scoped>

.txt-t {
    font-size: 1.1rem;
    font-weight: bold;
}

.txt-d {
    font-size: 1.1rem;
}
/deep/ .v-text-field{
    width: 15%;
}
</style>
