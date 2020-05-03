import Vue from 'vue'
import Vuex, { Store } from 'vuex'
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';
import { PersonsApi } from '@/services/PersonApi';
import { IPerson } from './../domain/IPerson';
import { CarsApi } from '@/services/CarApi';
import { PersonCarApi } from '@/services/PersonCarApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        persons: [] as IPerson[]
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
        setPersons(state, persons: IPerson[]) {
            state.persons = persons;
        }
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
        async getPersons(context): Promise<void> {
            const persons = await PersonsApi.getAll();
            context.commit('setPersons', persons);
        },
        async deletePerson(context, id: string): Promise<void> {
            console.log('deletePerson', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PersonsApi.delete(id, context.state.jwt);
                await context.dispatch('getPersons');
            }
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
        async getPersonCars(context): Promise<void> {
            const personCars = await PersonCarApi.getAll();
            context.commit('setPersons', personCars);
        },
        async deletePersonCars(context, id: string): Promise<void> {
            console.log('deletePersonCar', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PersonCarApi.delete(id, context.state.jwt);
                await context.dispatch('getPersonCars');
            }
        }
    },
    modules: {
    }
})
