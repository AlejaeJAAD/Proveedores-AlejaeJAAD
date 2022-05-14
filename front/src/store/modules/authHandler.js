import Axios from "axios";

const state = {
  token: localStorage.token || null,
  userId: localStorage.userId || null,
  rol: localStorage.rol || null
};
const mutations = {
  authUser(state, userData) {
    //state.userId = userData.id;
    state.token = userData.token;
    state.rol = userData.rol;
  },
  clearAuthData(state) {
    state.token = null;
    state.userId = null;
    state.rol = null;
    state.user = null;
  }
};
const actions = {
  setLogoutTime({ dispatch }, expiration) {
    setTimeout(() => {
      console.log("Logout");
      dispatch("logout");
    }, expiration);
  },
  logIn({ commit, dispatch }, authData) {
    return new Promise((resolve, error)=> {
    Axios.post("user/auth/", authData)
      .then(res => {
        if(typeof res.data.error !== 'undefined'){
          error(res);
          return;
        }
        console.log("Login: ", res);
        const now = new Date();
        const expiration = new Date().setDate(now.getDate() + 2);
        localStorage.setItem("expiration", new Date(expiration).toISOString());
        localStorage.setItem("token", res.data.token);
        localStorage.setItem("userId", res.data.id);
        localStorage.setItem("rol", res.data.rol);

        commit("authUser", res.data);
        dispatch("setLogoutTime", expiration - now.getTime());
        dispatch("fetchRoles");
        resolve(res);
      }).catch((err)=>error(err));
    })
  },
  tryAutoLogin({ commit, dispatch }) {
    const token = localStorage.token;
    if (!token) {
      return false;
    }
    const expiration = new Date(localStorage.expiration);
    const now = new Date();
    console.log(now, expiration);
    if (now >= expiration) {
      dispatch("logout");
      return false;
    }
    const userId = localStorage.userId;
    const rol = localStorage.rol;
    commit("authUser", {
      token,
      userId,
      rol
    });
    return true;
  },
  logout({ commit }) {
    commit("clearAuthData");
    localStorage.removeItem("token");
    localStorage.removeItem("expiration");
    localStorage.removeItem("userId");
    localStorage.removeItem("rol");
  }
};
const getters = {
  isLogged(state) {
    return state.token !== null;
  },
  getRol(state) {
    return state.rol;
  },
  getUserId(state) {
    return state.userId;
  },
  getToken(state) {
    return state.token;
  }
};
export default {
  state,
  mutations,
  actions,
  getters
};
