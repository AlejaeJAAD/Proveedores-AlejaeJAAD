import Vue from "vue";
import VueRouter from "vue-router";
import store from "../store";
import { admin, proveedor, cliente } from "../main";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "landing",
    component: () => import("../views/View"),
    meta: { requiresAuth: false }
  },
  {
    path: "/login",
    name: "login",
    component: () => import("../views/Login"),
    meta: { requiresAuth: false }
  },
  /** PROVEEDOR**/
  {
    path: "/proveedor",
    name: "proveedor",
    components: {
      default: () => import("../views/Proveedor/Clientes"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "b2dcdcf5-5065-4b5f-9932-b9d4e7918914"
    }
  },
  {
    path: "/proveedor/inventario",
    name: "p-inventario",
    components: {
      default: () => import("../views/Proveedor/Inventario"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "b2dcdcf5-5065-4b5f-9932-b9d4e7918914"
    }
  },
  {
    path: "/proveedor/perfil",
    name: "p-perfil",
    components: {
      default: () => import("../components/EditarPerfil"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "b2dcdcf5-5065-4b5f-9932-b9d4e7918914"
    }
  },
  /** FIN PROVEEDOR **/

  /** CLIENTE **/
  {
    path: "/cliente",
    name: "cliente",
    components: {
      default: () => import("../views/Cliente/Cliente.vue"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "ff403a39-10fd-41bb-9124-18c101ce8bf1"
    }
  },
  {
    path: "/cliente/tienda/:idP",
    name: "c-tienda",
    components: {
      default: () => import("../components/Cliente/Tienda"),
      navbar: () => import("../components/Navbar")
    },
    meta: { requiresAuth: true, requiresRole: 'ff403a39-10fd-41bb-9124-18c101ce8bf1' }
  },
  {
    path: "/cliente/perfil",
    name: "c-perfil",
    components: {
      //default: () => import("../views/cliente/"),
      default: () => import("../components/EditarPerfil"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "ff403a39-10fd-41bb-9124-18c101ce8bf1"
    }
  },

  /** FIN CLIENTE **/

  /** ADMIN **/
  {
    path: "/admin",
    name: "admin",
    components: {
      //default: () => import("../views/cliente/"),
      navbar: () => import("../components/Navbar")
    },
    meta: {
      requiresAuth: true,
      requiresRole: "520cf522-f89a-4744-beb5-fef492770829"
    }
  },
  {
    path: "/admin/perfil",
    name: "a-perfil",
    components: {
      //default: () => import("../views/cliente/"),
      navbar: () => import("../components/Navbar")
    },
    meta: { requiresAuth: true, requiresRole: '520cf522-f89a-4744-beb5-fef492770829' }
  },
  /** FIN ADMIN **/
  {
    path: "*",
    redirect: "/login"
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  const requiresRole = to.matched.some(record => record.meta.requiresAuth);
  const rol = store.getters.getRol;
  if (requiresAuth) {
    if (store.getters.isLogged) {
      if (requiresRole) {
        if (rol === to.meta.requiresRole) {
          next();
        } else {
          if (rol === proveedor) {
            next("/proveedor");
            return;
          }
          if (rol === admin) {
            next("/admin");
            return;
          }
          if (rol === cliente) {
            next("/cliente");
            return;
          }
        }
      }
    } else {
      next("/login");
    }
  } else {
    if (rol === proveedor) {
      next("/proveedor");
      return;
    }
    if (rol === admin) {
      next("/admin");
      return;
    }
    if (rol === cliente) {
      next("/cliente");
      return;
    }
  }
  next();
});

/*
router.beforeEach(((to, from, next) => {
  console.log(to)
  store.dispatch('tryAutoLogIn').then((res)=>{
    if(res){
      store.dispatch('fetchUser').then(()=>{
        const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
        if(requiresAuth) {
          console.log('requiere')
          if(store.getters.getToken){
            console.log('requiere2')
            next();
            return;
          }
          next('/login')
        }
      })
    }

    next();
  })
  next();
}));
*/

export default router;
