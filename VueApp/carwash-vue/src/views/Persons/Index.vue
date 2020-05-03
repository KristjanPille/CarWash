<template>
    <div>
        <h1>Persons Index</h1>
        <ul>
            <li v-for="person in persons" :key="person.id">
                 {{session.userFirstLastName}} - {{person.name}} - {{person.id}}
                <button
                    @click="deleteOnClick(person)"
                    type="button"
                    class="btn btn-danger"
                >Delete</button>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import router from '../../router'
import { IPerson } from '../../domain/IPerson';
import store from "../../store";

@Component
export default class PersonIndex extends Vue {
    get sessions(): IPerson[] {
        return store.state.persons;
    }

    deleteOnClick(person: IPerson): void {
        store.dispatch('deletePerson', person.id);
    }

  beforeCreate (): void {
    console.log('beforeCreate')
  }

  created (): void {
    console.log('created')
  }

  beforeMount (): void {
    console.log('beforeMount')
  }

  mounted (): void {
    console.log('mounted')
    store.dispatch("getPersons");
  }

  beforeUpdate (): void {
    console.log('beforeUpdate')
  }

  updated (): void {
    console.log('updated')
  }

  beforeDestroy (): void {
    console.log('beforeDestroy')
  }

  destroyed (): void {
    console.log('destroyed')
  }
}
</script>
