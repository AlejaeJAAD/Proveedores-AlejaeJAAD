import Axios from "axios";
//import {cliente,proveedor} from '../../main'

const state = {
  user: {},
  roles: [],
  admin: '',
  proveedor: '',
  cliente: '',
  notificaciones: [],
  supps:[],
};
const mutations = {
  setUser(state, userData) {
    state.user = userData;
  },
  setRoles(state, roles) {
    state.roles = roles;

    if(roles){
      for (let i = 0; i < roles.length; i++) {
        if(roles[i].rol.toString().toLowerCase() === 'admin'){
          state.admin = roles[i].id;
        }
        if(roles[i].rol.toString().toLowerCase() === 'cliente'){
          state.cliente = roles[i].id;
        }
        if(roles[i].rol.toString().toLowerCase() === 'proveedor'){
          state.proveedor = roles[i].id;
        }
      }
    }
  },
  setNotificaciones(state,notificaciones){
    state.notificaciones = notificaciones;
  },
  async setSupp(state,supps){
    state.supps = supps;
  }
};
const actions = {
  fetchUser({ commit }) {
    return Axios.get("user/" + localStorage.userId, {
      headers: {
        Authorization: "Bearer " + localStorage.token
      }
    }).then(res => commit("setUser", res.data));
  },
  fetchRoles({ commit }) {
    return Axios.get("user/roles/", {
      headers: {
        Authorization: "Bearer " + localStorage.token
      }
    }).then(res => {
      commit("setRoles", res.data);
    });
  },
  updateUser({dispatch},userIn){
    console.log(JSON.stringify(userIn));
    return Axios.put('/user/'+userIn.id,userIn,{
      headers: {
        Authorization: `Bearer ${localStorage.token}`
      },
    }).then((res)=>{
      console.log(res);
      dispatch('fetchUser');
    })
  },
  //eslint-disable-next-line
  updatePassword({dispatch},pwModel){
    console.log(JSON.stringify(pwModel))
    return Axios.put(`user/updatepw/${pwModel.id}`,{
      oldPassword: pwModel.oldPassword,
      newPassword: pwModel.newPassword,
    }, {
      headers:{
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then(res => {
      return res;
    }).catch(err=>console.log(err));
  },
  updateStatus({dispatch},estatusModel){
    return Axios.put('user/estatus/'+localStorage.userId,estatusModel,{
      headers:{
        Authorization: 'Bearer '+localStorage.token,
      },
    }).then((res)=>{

      dispatch('fetchUser');
      return res.data
    });
  },
  //eslint-disable-next-line
  fetchByEmail({},email){
    return Axios.get(`user/byEmail/${email}`,{
      headers:{
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then(res=>res.data);
  },
  //eslint-disable-next-line
  afiliar({dispatch},solicitudModel){
    return Axios.post('afiliacion/'+localStorage.userId,solicitudModel,{
      headers:{
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then(res=>res.data)
  },
  fetchAfiliaciones({commit}){
    return new Promise((res,err)=>{
      Axios.get('afiliacion/'+localStorage.userId,{
        headers:{
          Authorization: `Bearer ${localStorage.token}`
        }
      }).then(response=>{
        console.log(response);
       // const afiliaciones = response.data;
        let afi=[];
        /*if(localStorage.rol === cliente){
          afi = afiliaciones.filter((elm) => {
            return elm.idCliente === localStorage.userId && elm.aceptado === true;
          })
        }else if(localStorage.rol === proveedor){
          afi = afiliaciones.filter((elm) => {
            return elm.idProveedor === localStorage.userId && elm.aceptado === true;
          })
        }*/
        commit('setSupp',afi)
        console.log(afi);
        res(afi);

      }).catch(error=>{
        err(error);
        console.log(error)
      })
    })
  },
  updateAfiliacion(){
  },
  deleteAfiliacion(){

  },
  //eslint-disable-next-line
  generateNotificaciones({commit}){

    let notificaciones = [];
    function Notificacion(texto,tipo,id){
      this.texto = texto;
      this.tipo = tipo;
      this.id = id;
    }
    Axios.get('afiliacion/notificaciones/'+localStorage.userId,{
      headers:{
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then(res=>{
      const temp = res.data;
      console.log(temp);
     if(temp.lNotificaciones){
       console.log(temp);
       for (let i = 0; i < temp.lNotificaciones.length ; i++) {
         notificaciones.push(
             new Notificacion(
                 temp.lNotificaciones[i].id+' quiere que te unas a sus clientes',
                 1,
                 temp.lNotificaciones[i].id+'')
         )
       }
     }
    }).finally(()=>commit('setNotificaciones',notificaciones));
  },
  aceptarAfiliacion(context,id){
    Axios.put('/afiliacion/aceptar/'+localStorage.userId+'/'+id,{},
        {
          Authorization: `Bearer ${localStorage.token}`
        }).then(res=>console.log(res));
  }
};
const getters = {
  getUser(state) {
    return state.user;
  },
  getRoles(state) {
    return state.roles;
  },
  getNotificaciones(state){
    return state.notificaciones;
  },
  getAfiliaciones(state){
    return state.supps;
  }
};
export default {
  state,
  mutations,
  actions,
  getters
};
