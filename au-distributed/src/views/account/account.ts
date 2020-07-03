import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {IAccount} from "../../domain/IAccount";
import {AccountService} from "../../service/account-service";
import {AppState} from "../../state/app-state";

@autoinject
export class AccountEdit {
    private _alert: IAlertData | null = null;

    _email = "";
    _firstName = "";
    _lastName = "";
    _phoneNumber = "";

    private _account?: IAccount;

    constructor(private accountService: AccountService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
       this.accountService.getUser().then(
           response => {
               if (response.statusCode >= 200 && response.statusCode < 300) {
                   this._alert = null;
                   this._account = response.data!;
                   this._email = this._account.email;
                   this._firstName = this._account.firstName;
                   this._lastName = this._account.lastName;
                   this._phoneNumber = this._account.phoneNumber;
               } else {
                   // show error message
                   this._alert = {
                       message: response.statusCode.toString() + ' - ' + response.errorMessage,
                       type: AlertType.Danger,
                       dismissable: true,
                   }
               }
           }
       );
    }

    onSubmit(event: Event) {
        this._account!.email = this._email
        this._account!.firstName = this._firstName
        this._account!.lastName = this._lastName
        this._account!.phoneNumber = this._phoneNumber
        this.accountService
            .updateAccount(this._account!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('home', {});
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );

        event.preventDefault();
    }
}
