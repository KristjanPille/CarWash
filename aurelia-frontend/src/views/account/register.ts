import {autoinject, LogManager, observable} from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import { Router } from 'aurelia-router';
import {CarService} from "../../service/car-service";
import {ModelMarkservice} from "../../service/modelMark-service";
import {AlertType} from "../../types/AlertType";
import {IAlertData} from "../../types/IAlertData";
import {IMark} from "../../domain/IMark";
import {IModel} from "../../domain/IModel";
import {connectTo, Store} from "aurelia-store";
import {LayoutResources} from "../../lang/LayoutResources";
import {IState} from "../../state/state";


@connectTo()
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
    private langResources = LayoutResources;
    protected newState!: IState;
    @observable
    protected state!: IState;


    constructor(
        private accountService: AccountService,
        private carService: CarService,
        private modelMarkService: ModelMarkservice,
        private appState: AppState,
        private router: Router,
        private store: Store<IState>
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

    private stateChanged(newValue: IState): void {
        this.newState = newValue;
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
            if (this.newState.selectedCulture.code == "en-GB"){
                this.emailError = "Palun sisestage email";
            }
            else{
                this.emailError = "Please enter email";
            }
            // @ts-ignore
            document.getElementById('popupEmail').style.display = 'block';
            return
        }
        if(this._firstname == ""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.firstNameError = "Please enter first name";
            }
            else{
                this.firstNameError = "Palun sisestage eesnimi";
            }
            // @ts-ignore
            document.getElementById('popupFirstName').style.display = 'block';
            return
        }
        if(this._lastname == ""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.lastNameError = "Please enter last name";
            }
            else{
                this.lastNameError = "Palun sisestage perekonna nimi";
            }
            // @ts-ignore
            document.getElementById('popupLastName').style.display = 'block';

            return
        }
        if(this._phoneNr=="" || this._phoneNr.length > 20 || !/^\d+$/.test(this._phoneNr)){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.phoneNrError = "Please enter valid phone number";
            }
            else{
                this.phoneNrError = "Palun sisestage õige telefoni number";
            }
            // @ts-ignore
            document.getElementById('popupPhoneNr').style.display = 'block';

            return
        }
        if(this._mark==""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.markError = "Please select mark";
            }
            else{
                this.markError = "Palun valige mark";
            }
            // @ts-ignore
            document.getElementById('popupMark').style.display = 'block';

            return
        }
        if(this._model==""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.modelError = "Please select model";
            }
            else{
                this.modelError = "Palun valige mudel";
            }
            // @ts-ignore
            document.getElementById('popupModel').style.display = 'block';

            return
        }
        if(this.confirmPassword==""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.passwordConfirmError = "Please confirm your password";
            }
            else{
                this.passwordConfirmError = "Palun kinnitage salasõna";
            }
            // @ts-ignore
            document.getElementById('popupPasswordConfirm').style.display = 'block';

            return
        }
        if( this.password != this.confirmPassword){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.passwordError = "Passwords do not match";
                this.passwordConfirmError = "Passwords do not match";
            }
            else{
                this.passwordError = "Salasõnad ei ühildu";
                this.passwordConfirmError = "Salasõnad ei ühildu";
            }
            // @ts-ignore
            document.getElementById('popupPasswordConfirm').style.display = 'block';
            // @ts-ignore
            document.getElementById('popupPassword').style.display = 'block';
            return
        }
        if(this.password == ""){
            if (this.newState.selectedCulture.code == "en-GB"){
                this.passwordError = "Please enter password";
            }
            else{
                this.passwordError = "Palun sisestage salasõna";
            }
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
