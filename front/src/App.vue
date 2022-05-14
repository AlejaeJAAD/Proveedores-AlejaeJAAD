<template>
  <v-app>
    <router-view name="navbar"></router-view>
    <main>
      <router-view></router-view>
    </main>
  </v-app>
</template>

<script>
export default {
  name: "App",
  data() {
    return {};
  },
  created() {
    this.$store.dispatch("tryAutoLogin").then(res => {
      if (res) {
        this.$store.dispatch("generateNotificaciones");
        this.$store.dispatch("fetchUser");
        this.$store.dispatch("fetchRoles");
      } else {
        console.log("No hay autologin");
      }
    });
  }
};
</script>

<style scoped>
main {
  margin-top: 3rem;
}
</style>
