<template>
    <v-container >
        <h3>Mis Proveedores</h3>
        <v-divider></v-divider>
        <v-row>
            <v-col  v-for="afiliacion in afiliaciones" :key="afiliacion.id" cols="12" sm="6" >
                <supp-card :afiliacion="afiliacion" @v-prod="showProductos($event)">
                </supp-card>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
import SuppCard from "../../components/Cliente/SuppCard";
export default {
    name: 'Cliente',
    components: {SuppCard},
    data(){
        return{
            idProveedor:'',
        }
    },
    computed:{
        afiliaciones(){
            return this.$store.getters.getAfiliaciones;
        }
    },
    methods:{
        showProductos(idProv){
            this.productos=true;
            this.$store.dispatch('encrypt',idProv).then(res =>{
                const idP = res;
                this.$router.push({name:'c-tienda',params: {idP}})
            } )
            this.idProveedor=idProv;
        }
    },
    created(){
        this.$store.dispatch('fetchAfiliaciones');
    }
}
</script>
