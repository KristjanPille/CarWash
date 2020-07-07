import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import AccountLogin from '../views/Account/Login.vue'
import CarIndex from '../views/Cars/Index.vue'
import CarDetails from '../views/Cars/Details.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  { path: '/', name: 'Home', component: Home },

  { path: '/account/login', name: 'AccountLogin', component: AccountLogin },

  { path: '/cars', name: 'cars', component: CarIndex },

  { path: '/cars/details/:id?', name: 'carDetails', component: CarDetails, props: true }
]

const router = new VueRouter({
  routes
})

export default router
