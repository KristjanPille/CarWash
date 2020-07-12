import { autoinject, LogManager } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { Router } from 'aurelia-router';
import {CarService} from "../../service/car-service";
import {ModelMarkservice} from "../../service/modelMark-service";
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {IMark} from "../../domain/IMark";
import {IModel} from "../../domain/IModel";


@autoinject
export class AccountRegister {
    private _alert: IAlertData | null = null;
    private _marks: IMark[] = [];
    private _models: IModel[] = [];
    private _email: string = "";
    private password: string = "";
    private _firstname: string = "";
    private _lastname: string = "";
    private _phoneNr: string = "";
    private _model: string = "";
    private _mark: string = "";
    private confirmPassword: string = "";
    private _errorMessage: string | null = null;
    private emailError = "";
    private firstNameError = "";
    private lastNameError = "";
    private phoneNrError = "";
    private markError = "";
    private modelError = "";
    private passwordError = "";
    private passwordConfirmError = "";


    constructor(
        private accountService: AccountService,
        private carService: CarService,
        private modelMarkService: ModelMarkservice,
        private appState: AppState,
        private router: Router
      ) {

      }
    attached() {
        this.modelMarkService.getMarks().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._marks = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        )
    }

    getModels(mark: string){
        this._mark = mark;
        this.modelMarkService.getModels(mark).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._models = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        )
    }

    createModel(model: string){
        this._model = model;
    }
    
    submit(): void {
        if(this._email == ""){
            this.emailError = "Please enter email";
            // @ts-ignore
            document.getElementById('popupEmail').style.display = 'block';
            return
        }
        if(this._firstname == ""){
            this.firstNameError = "Please enter first name";
            // @ts-ignore
            document.getElementById('popupFirstName').style.display = 'block';
            return
        }
        if(this._lastname == ""){
            this.lastNameError = "Please enter last name";
            // @ts-ignore
            document.getElementById('popupLastName').style.display = 'block';

            return
        }
        if(this._phoneNr=="" || this._phoneNr.length > 20){
            this.phoneNrError = "Please enter valid phone number";
            // @ts-ignore
            document.getElementById('popupPhoneNr').style.display = 'block';

            return
        }
        if(this._mark==""){
            this.markError = "Please select mark";
            // @ts-ignore
            document.getElementById('popupMark').style.display = 'block';

            return
        }
        if(this._model==""){
            this.modelError = "Please select model";
            // @ts-ignore
            document.getElementById('popupModel').style.display = 'block';

            return
        }
        if(this.confirmPassword==""){
            this.passwordConfirmError = "Please confirm your password";
            // @ts-ignore
            document.getElementById('popupPasswordConfirm').style.display = 'block';

            return
        }
        if( this.password != this.confirmPassword){
            this.passwordError = "Passwords do not match";
            // @ts-ignore
            document.getElementById('popupPassword').style.display = 'block';

            this.passwordConfirmError = "Passwords do not match";
            // @ts-ignore
            document.getElementById('popupPassword').style.display = 'block';
            return
        }
        if(this.password == ""){
            this.passwordError = "Please enter password";
            // @ts-ignore
            document.getElementById('popupPassword').style.display = 'block';

            return
        }


      this.accountService.register(this._email, this.password, this._firstname, this._lastname, this._phoneNr, this._mark, this._model)
      .then(response => {
        if (response !== undefined && response.status >= 200 && response.status < 300) {
        this.appState.jwt = response.token;
            this.accountService.login(this._email, this.password).then(
                response => {
                    if (response.statusCode == 200) {
                        this.appState.jwt = response.data!.token;
                        this.router!.navigateToRoute('Services');
                    } else {
                        this._errorMessage = response.statusCode.toString() + ' ' + response.errorMessage!
                    }
                }
            );

        }
      });
    }
}
