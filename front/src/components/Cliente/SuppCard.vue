<template>
    <div>
        <v-card  v-if="!loading"
                 :elevation="elevation"
                 @mouseenter="elevation=10"
                 @mouseleave="elevation=2"
                 dark
        >
            <v-list-item>
                <v-list-item-avatar :size="70" color="grey">
                    <v-img :src="toShow.logo ? toShow.logo : ''">
                        <span class="white--text">
                            {{ toShow.logo ? '' : 'Sin imagen' }}
                        </span>
                    </v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <v-list-item-title class="headline">
                        {{toShow.nombreEmpresa}}
                    </v-list-item-title>
                    <v-list-item-subtitle style="word-break: break-word">
                        {{ toShow.nombres+' '+toShow.apellidos  }}
                    </v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>
            <v-divider></v-divider>
            <v-card-text>

                <div>Sitio Web: <a :href="toShow.sitioWeb">{{toShow.nombreEmpresa}}</a> </div>
                <div>Telefono: <a :href="'tel:'+toShow.telefonoEmpresa">{{toShow.telefonoEmpresa}}</a> </div>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn outlined color="green darken-2">Ver usuario</v-btn>
                <v-btn outlined @click="$emit('v-prod',toShow.id)" color="orange" >Ver productos</v-btn>
            </v-card-actions>
        </v-card>
        <v-sheet v-else>
            <v-skeleton-loader class="mx-auto" type="card">
            </v-skeleton-loader>
        </v-sheet>
    </div>
</template>
<script>
import Axios from "axios";

export default {
    props:['afiliacion'],
    name: 'SuppCard',
    data(){
        return{
            dialog:false,
            loading: true,
            idAfiliacion:this.afiliacion ? this.afiliacion.id : '',
            idCliente: this.afiliacion ?  this.afiliacion.idCliente : '',
            idProveedor: this.afiliacion ?  this.afiliacion.idProveedor : '',
            toShow:'',
            elevation: 2,
        }
    },
    methods:{

    },
    created() {
        if(this.afiliacion){
            Axios.get("user/" + this.idProveedor, {
                headers: {
                    Authorization: "Bearer " + localStorage.token
                }
            }).then(res =>{ this.toShow = res.data; this.loading=false });
        }
    },
}
</script>
<style>
.transparent{
    background-color: transparent !important;
}
</style>
