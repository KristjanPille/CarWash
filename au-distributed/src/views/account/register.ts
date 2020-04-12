import { autoinject, LogManager } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { Router } from 'aurelia-router';

@autoinject
export class AccountRegister {
    private _email: string = "";
    private password: string = "";
    private _firstname: string = "";
    private _lastname: string = "";
    private confirmPassword: string = "";


    constructor(
        private accountService: AccountService,
        private appState: AppState,
        private router: Router
      ) {

      }
    
    submit(): void {
        if(this.password==null){
            alert('1');
            return
        }
        if(this.confirmPassword==null){
            alert('2');
            return
        }
        if(this._email == null){
            alert('3');
            return
        }
        if( this.password != this.confirmPassword){
            alert('4');
            return
        }
        if(this.password.length < 4){
            alert(this.confirmPassword)
            alert('5');
            return
        }
        if(this._email.length == 0){
            alert('6');
            return
        }

      this.accountService.register(this._email, this.password, this._firstname, this._lastname)
      .then(response => {
        if (response.statusCode !== undefined) {
        this.appState.jwt = response.data!.token;
          this.router.navigateToRoute('home');
        }
      });
    }
}
