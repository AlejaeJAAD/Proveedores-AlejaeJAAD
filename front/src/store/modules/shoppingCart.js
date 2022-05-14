import Axios from "axios";

const state = {
    cart: [],
    cart_supplierId: '',
    supplierProducts:[],
}
const mutations ={
    setSupplierProducts(state,payload){
        state.supplierProducts = payload;
    },
    updateCart(state,payload){
        state.cart = payload;
    },
}
const actions={
    fetchSupplierProducts(context,supplierId){
        return new Promise((resolve,error) =>{
            Axios.get('product/byProvider/'+supplierId,{
                headers:{
                    Authorization: `Bearer ${localStorage.token}`
                }
            }).then(res=>{
                context.commit('setSupplierProducts',res.data);
                resolve(res.data)
            }).catch(err=>error(err));
        });
    },
    pushIntoCart(context,product){
        let myCurrentCart = context.state.cart;
        console.log('a',product);
        if(myCurrentCart.length>0){
            const a = myCurrentCart.find(v=>v.indice === product.indice);
            console.log('a3',a);
            if(!a){
                myCurrentCart.push(product);
            }else{
                myCurrentCart.find(v=>v.indice === product.indice).cantidad += product.cantidad;
            }
        }else {
            myCurrentCart.push(product);
        }
        console.log('a2',context.state.supplierProducts[product.indice]);
        context.state.supplierProducts[product.indice].stock -= product.cantidad;
        context.commit('updateCart',myCurrentCart);
    },
    removeFromCart(context,product){
        let myCurrentCart = context.state.cart;
        let myNewCart = myCurrentCart.filter(v=>v.indice !== product.indice);
        context.commit('updateCart',myNewCart);
        context.state.supplierProducts[product.indice].stock+= product.cantidad;
    },
    updateCantidad(context,payload){
        context.state.supplierProducts[payload.product.indice].stock+= payload.product.cantidad;
        console.log(context.state.supplierProducts[payload.product.indice].stock)
        context.state.supplierProducts[payload.product.indice].stock-= payload.newQty;
        console.log(context.state.supplierProducts[payload.product.indice].stock)
        console.log(context.state.cart)
        let myCurrentCart = context.state.cart;
        myCurrentCart.find(v=>v.indice===payload.product.indice).cantidad=payload.newQty;
        console.log(myCurrentCart.find(v=>v.indice===payload.product.indice))
        context.commit('updateCart',myCurrentCart);


    },
    cleanCart(context){
        context.commit('updateCart',[]);
    }
}
const getters={
    getSupplierProducts(state){
        return state.supplierProducts;
    },
    getCurrentCart(state){
        return state.cart;
    }
}
export default {
    state,
    mutations,
    actions,
    getters
}
