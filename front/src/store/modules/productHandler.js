import Axios from "axios";

const state= {
  products: [],
};
const mutations= {
  setProducts(state,products){
    state.products = products;

  }
};
const actions= {
  createProduct({dispatch},product){
    Axios.post('/product/',product,{
      headers:{
        Authorization: `Bearer ${localStorage.token}`,
      }
    }).then(()=>dispatch('fetchProducts'));
  },
  fetchProducts({commit}){
    return Axios.get('/product/byProvider/'+localStorage.userId,{
      headers: {
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then((res)=> {
      console.log(res.data)
      commit('setProducts', res.data)
    });

  },
  updateProduct({dispatch},product){
    console.log(product)
    return  Axios.put('/product/'+product.id,product,{
      headers:{
        Authorization: `Bearer ${localStorage.token}`
      }
    }).then((res)=>{
      dispatch('fetchProducts');
      return res.data;
    })
  }
};
const getters={
  getProducts(state){
    return state.products;
  }

};

export default {
  state,
  mutations,
  actions,
  getters
}
