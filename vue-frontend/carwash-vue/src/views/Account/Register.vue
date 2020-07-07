<template>
<div class="row">
    <div class="col-md-4">
        <form method="post" submit.trigger="submit()">
            <h4>Create a new account - Car wash</h4>
            <hr />
            <div class="form-group">
                <label for="Input_Email">Email</label>
                <input v-model="loginInfo.email" class="form-control" type="email" id="Input_Email" />
            </div>
            <div class="form-group">
                <label for="email">First name</label>
                <input class="form-control" type="firstname" id="firstname" name="firstname" value.bind="_firstname" />
            </div>
            <div class="form-group">
                <label for="email">Last name</label>
                <input class="form-control" type="lastname" id="lastname" name="lastname" value.bind="_lastname" />
            </div>
            <div class="form-group">
                <label for="Input_Password">Password</label>
                <input v-model="loginInfo.password" class="form-control" type="password" id="Input_Password" />
            </div>
            <div class="form-group">
                <label for="Confirm_Password">Confirm password</label>
                <input class="form-control" type="password" id="confirmPassword" name="confirmPassword" value.bind="confirmPassword" />
            </div>

            <button type="submit" class="btn btn-primary">Register</button>
        </form>
    </div>
</div>
</template>

<script lang="ts">
import {
    Component,
    Prop,
    Vue
} from 'vue-property-decorator'
import {
    IRegisterDTO
} from '@/types/IRegisterDTO'
import store from '../../store'
import router from '../../router'

@Component
export default class PersonIndex extends Vue {
    private registerinfo: IRegisterDTO = {
        email: 'juss@gmail.com',
        firstName: 'juss',
        lastName: 'juss2',
        password: 'Password123+',
        passwordConfirm: 'Password123+'
    };

    private registerWasOk: boolean | null = null

    registerOnClick(): void {
        if (
            this.registerinfo.email.length > 0 &&
            this.registerinfo.password.length > 0 &&
            this.registerinfo.firstName.length > 0 &&
            this.registerinfo.lastName.length > 0
        ) {
            store
                .dispatch('authenticateUser', this.registerinfo)
                .then((isRegistered: boolean) => {
                    if (isRegistered) {
                        this.registerWasOk = true
                        router.push("/");
                    } else {
                        this.registerWasOk = false
                    }
                });
        }
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        console.log("mounted");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>
