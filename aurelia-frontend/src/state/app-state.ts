import {JwtDecode} from "aurelia-plugins-jwt-decode/dist/amd/aurelia-plugins-jwt-decode";
import {options} from "jest-cli/build/cli/args";

export class AppState {
    constructor(){
    }

    public readonly baseUrl = 'https://localhost:5001/api/v1.0/';

    // JavaScript Object Notation Web Token 
    // to keep track of logged in status
    // https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
    get jwt():string | null {
        return localStorage.getItem('jwt');
    }

    get decodedJwt():string | null {
        return JwtDecode.decode(localStorage.getItem('jwt'));
    }

    set jwt(value: string | null){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }


}
