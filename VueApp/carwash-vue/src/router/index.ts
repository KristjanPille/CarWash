import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import PersonIndex from '../views/Persons/Index.vue'
import PersonDetails from '../views/Persons/Details.vue'
import CarIndex from '../views/Cars/Index.vue'
import CarDetails from '../views/Cars/Details.vue'
import PersonCarIndex from '../views/PersonCars/Index.vue'
import PersonCarDetails from '../views/PersonCars/Details.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  { path: '/', name: 'Home', component: Home },
  { path: '/persons', name: 'Persons', component: PersonIndex },
  { path: '/persons/details/:id?', name: 'Person Details', component: PersonDetails, props: true },
  { path: '/cars', name: 'cars', component: CarIndex },
  { path: '/cars/details/:id?', name: 'car Details', component: CarDetails, props: true },
  { path: '/personcars', name: 'Personcars', component: PersonCarIndex },
  { path: '/personcars/details/:id?', name: 'PersoncarDetails', component: PersonCarDetails, props: true }
]

const router = new VueRouter({
  routes
})

export default router
