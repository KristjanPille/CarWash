import Vue from 'vue'
import Vuex, { Store } from 'vuex'
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';
import { CarsApi } from '@/services/CarApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async getCars(context): Promise<void> {
            const cars = await CarsApi.getAll();
            context.commit('setCars', cars);
        },
        async deleteCars(context, id: string): Promise<void> {
            console.log('deleteCar', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CarsApi.delete(id, context.state.jwt);
                await context.dispatch('getPersons');
            }
        },
    },
    modules: {
    }
})
