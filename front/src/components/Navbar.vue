<template>
  <div>
    <v-app-bar class="color" dense dark app>
      <v-btn @click.stop="drawer = !drawer" outlined>
        <v-icon v-if="!drawer">mdi-menu</v-icon>
        <v-icon v-else>mdi-menu-open</v-icon>
      </v-btn>

      <v-spacer></v-spacer>

      <v-menu :disabled="!notificaciones.length > 0" rounded="b-lg" offset-y>
        <template v-slot:activator="{ attrs, on }">
          <v-btn text class="ma-2" color="dark" v-on="on" v-bind="attrs">
            <v-icon
              v-if="notificaciones.length > 0"
              class="white--text"
              color="red lighten-1"
              >mdi-bell-ring</v-icon
            >
            <v-icon v-else class="white--text">mdi-bell</v-icon>
            <span style="margin-left: 10px" v-if="notificaciones.length > 0">{{
              notificaciones.length
            }}</span>
          </v-btn>
        </template>
        <v-list>
          <v-list-item-group>
            <v-list-item
              v-for="(notificacion, value) in notificaciones"
              :key="value"
              class="option"
            >
              <v-list-item-title @click="pop(notificacion)">
                {{ notificacion.texto }}
              </v-list-item-title>
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-menu>

      <span class="nombre">{{ nombre }}</span>
      <v-avatar size="45" color="grey">
        <v-img :src="userImage"></v-img>
      </v-avatar>

      <v-menu left bottom min-width="1%">
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>

        <v-list>
          <v-list-item>
            <div style="margin: 0 auto;">
              <v-list-item-title style="font-weight: bolder;">{{
                nombre
              }}</v-list-item-title>
            </div>
          </v-list-item>
          <v-divider></v-divider>
          <v-list-item
            class="option"
            v-for="(item, index) in menu"
            :key="index"
          >
            <v-list-item-title @click="go(item.to)">
              <div>
                <v-icon>{{ item.icon }} </v-icon> {{ item.nombre }}
              </div>
            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" disable-resize-watcher app>
      <template>
        <v-list>
          <div @click="drawer = false">
            <v-list-item-group class="menutext" v-model="newitem">
              <v-list-item
                v-for="(item, i) in opciones"
                :key="i"
                @click="$router.push(item.to)"
              >
                <v-list-item-icon>
                  <v-icon v-text="item.icon"></v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title v-text="item.text"></v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </div>
        </v-list>
      </template>
    </v-navigation-drawer>
    <v-dialog v-model="shNot" width="40%" persistent>
      <v-card>
        <v-card-title>
          {{ noti.texto }}
        </v-card-title>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer> </v-spacer>
          <v-btn v-if="noti.tipo === 1" color="red" class="white--text" @click="cancelar"
            >Cancelar</v-btn
          >
          <v-btn v-if="noti.tipo ===1" color="green" class="white--text" @click="aceptar(noti)">Aceptar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { proveedor, cliente, admin } from "../main";

export default {
  name: "Navbar",
  data() {
    return {
      newitem: false,
      noti: {},
      shNot: false,
      opciones: [],
      menu: [
        { nombre: "Editar Perfil", to: "perfil", icon: "mdi-account-edit" },
        { nombre: "Cerrar Sesión", to: "login", icon: "mdi-logout" },
        {
          nombre: "Centro de Ayuda",
          to: "ayuda",
          icon: "mdi-help-circle-outline"
        }
      ],
      drawer: false
    };
  },
  computed: {
    notificaciones() {
      return this.$store.getters.getNotificaciones;
    },
    nombre() {
      const user = this.$store.getters.getUser;
      return user ? user.username : "";
    },
    userImage() {
      const user = this.$store.getters.getUser;
      return user.imagen
        ? user.imagen
        : "https://www.dovercourt.org/wp-content/uploads/2019/11/610-6104451_image-placeholder-png-user-profile-placeholder-image-png.jpg";
    },
    roles() {
      const roles = this.$store.getters.getRoles;
      return roles ? roles : "";
    },
    rol() {
      const rol = this.$store.getters.getRol;
      return rol ? rol : "";
    }
  },
  methods: {
    pop(not) {
      this.shNot = true;
      this.noti = not;
    },
    go(to) {
      console.log(to,this.$route.path)
      if (to.toString() === "login") {
        this.$store.dispatch("logout");
        this.$router.push("/login");
        return;
      }
      if (to.toString() === "ayuda") {
        this.$router.push("/ayuda");
        return;
      }
      if (this.rol === proveedor) {
        this.$router.push("/proveedor/" + to.toString()).catch((err)=> err);
      }
      if (this.rol === cliente) {
        this.$router.push("/cliente/" + to.toString()).catch((err)=> err);
      }
      if (this.rol === admin) {
        this.$router.push("/admin/" + to.toString()).catch((err)=> err);
      }
    },
    cancelar(){

    },
    aceptar(noti){
      this.$store.dispatch('aceptarAfiliacion',noti.id);
      this.shNot = false;
    }
  },
  created() {
  
    
      if (this.rol === proveedor) {
        this.opciones = [
          {
            icon: "mdi-account-multiple",
            text: "Clientes",
            to: "/proveedor"
          },
          {
            icon: "mdi-package-variant-closed",
            text: "Inventario",
            to: "/proveedor/inventario"
          },
          {
            icon: "mdi-truck",
            text: "Pedidos"
          },
          {
            icon: "mdi-file-chart",
            text: "Reportes"
          },
          {
            icon: "mdi-map-check-outline",
            text: "Mapa de entregas"
          }
        ];
      }
      if (this.rol === cliente) {
        this.opciones = [
          {
            icon: "mdi-account-multiple",
            text: "Mis Provedores",
            to: '/clientes'
          },
          {
            icon: "mdi-truck",
            text: "Pedidos"
          },
          {
            icon: "mdi-magnify",
            text: "Buscar Provedores"
          }
        ];
      }
      if (this.rol === admin) {
        this.opciones = [
          {
            icon: "mdi-account-multiple",
            text: "gestionar Proveedores"
          },
          {
            icon: "mdi-truck",
            text: "Gestionar Clientes"
          },
          {
            icon: "mdi-blur",
            text: "Red neuronal"
          },
          {
            icon: "mdi-chart-bar",
            text: "graficas"
          }
        ];
      }
  }
};
</script>

<style scoped>
nav {
  margin-top: 2rem !important;
}
header {
  position: absolute !important;
  top: 0 !important;
  left: 0 !important;
  z-index: 99999 !important;
}
.nombre {
  margin: 0 10px;
  font-size: 1.1rem;
}
.color {
  background-color: #1f1e1e !important;
}
.menutext {
  font-weight: bold;
  font-size: 1.1rem;
}
.option:hover {
  background-color: lightgrey;
  cursor: pointer;
}
</style>
