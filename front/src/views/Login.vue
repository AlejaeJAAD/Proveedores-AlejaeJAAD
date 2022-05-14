<template>
  <v-row class="nomar">
    <v-img src="../assets/login-background.jpg" height="100vh">
      <div class="dg">
        <v-col cols="10">
          <v-form @submit.prevent="login" v-model="valido">
            <v-card
              class="cont text-center"
              height="500px"
              width="500"
              elevation="25"
            >
              <v-progress-circular
                style="margin-top: 20% "
                v-if="loading"
                :size="50"
                color="grey"
                indeterminate
              ></v-progress-circular>
              <div v-else>
                <v-img
                  class="logo"
                  src="../assets/logo.png"
                  width="30%"
                  @click="$router.push('/')"
                >
                </v-img>
                <v-card-title>Inicio de Sesión</v-card-title>
                <v-divider></v-divider>
                <v-row>
                  <v-col cols="12" class="cc">
                    <v-text-field
                      v-model="username"
                      :rules="[rules.novacio]"
                      label="Usuario"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" class="cc">
                    <v-text-field
                      :append-icon="mPW ? 'mdi-eye' : 'mdi-eye-off'"
                      v-model="password"
                      :type="mPW ? 'text' : 'password'"
                      :rules="[rules.novacio, rules.longitudPW]"
                      label="Contraseña"
                      counter
                      @click:append="mPW = !mPW"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12">
                    <v-divider></v-divider>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn type="submit" class="txt-green" @click="login">
                        Iniciar Sesión
                        <v-icon>mdi-login</v-icon>
                      </v-btn>
                      <v-spacer></v-spacer>
                    </v-card-actions>
                  </v-col>
                </v-row>
                <v-alert v-model="alert" type="error">
                  {{errorMsg}}
                </v-alert>
              </div>
            </v-card>
          </v-form>
        </v-col>
      </div>
    </v-img>
  </v-row>
</template>

<script>
export default {
  name: "Login.vue",
  data() {
    return {
      errorMsg: "",
      alert: false,
      loading: false,
      username: "",
      password: "",
      valido: false,
      novacio: [],
      rules: {
        novacio: v => !!v || "No se permiten campos vacios",
        longitudPW: v =>
          v.length >= 8 || "La contraseña debe tener mas de 7 caracteres"
      },

      mPW: false
    };
  },
  methods: {
    login() {
      if (this.valido) {
        this.loading = true;
        //eslint-disable-next-line
          let username = null;
        let email = null;
        if (this.username.includes("@")) {
          email = this.username;
        } else {
          username = this.username;
        }
        const data = { username, email, password: this.password };

        this.$store.dispatch("logIn", data).then(() => {
          this.$store.dispatch("fetchUser").then(() => {
            const rol = this.$store.getters.getRol;
            const roles = this.$store.getters.getRoles;
            for (let i = 0; i < roles.length; i++) {
              if (rol === roles[i].id) {
                this.$router.replace("/" + roles[i].rol.toLowerCase());
              }
            }
          });
        }).catch((error) => {
          if(error.data){
              console.log('login catch')
              this.alert=true;
              this.errorMsg=error.data.error.mensaje;
              this.loading=false;
              setTimeout(()=> {
                  this.alert=false;
              },5000)
          }else{
              this.alert=true;
              this.errorMsg='Hay un problema con nuestro servidor';
              this.loading=false;
              setTimeout(()=> {
                  this.alert=false;
              },5000)
          }
        });
      }
    }
  }
};
</script>

<style scoped>
.dg {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  background-image: linear-gradient(
    rgba(253, 221, 202, 0.82),
    rgba(0, 0, 0, 0.1)
  );
  overflow-y: auto;
  overflow-x: auto;
  margin-bottom: 0;
}
.cont {
  padding: 10px;
  margin: 5% auto;
}
.cc {
  margin-top: 5px;
  padding: 0 30px;
}
.txt-green:hover {
  color: #02a502 !important;
}
.logo {
  margin: 0 auto;
  border-radius: 5px;
  cursor: pointer;
}
.nomar {
  margin-top: -3rem;
}
</style>
