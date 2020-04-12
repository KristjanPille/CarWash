import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { Router } from 'aurelia-router';

@autoinject
export class AccountRegister {
    private _email: string = "";
    private _password: string = "";
    private _passwordconfirm: string = "";
    private _firstname: string = "";
    private _lastname: string = "";
    private _errorMessage: string | null = null;

    constructor(
        private accountService: AccountService,
        private appState: AppState,
        private router: Router,
        ) {

    }

    onSubmit(event: Event) {
        console.log(this._email, this._password);
        event.preventDefault();

        this.accountService.register(this._email, this._password, this._passwordconfirm, this._firstname, this._lastname).then(
            response => {
                console.log(response);
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.router!.navigateToRoute('home');
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' '
                        + response.errorMessage!
                }
            }
        );
    }
}
