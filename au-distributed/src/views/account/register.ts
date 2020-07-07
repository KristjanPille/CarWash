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
    _Mark = "";
    _Model = "";
    private _email: string = "";
    private password: string = "";
    private _firstname: string = "";
    private _lastname: string = "";
    private _phoneNr: string = "";
    private _model: string = "";
    private _mark: string = "";
    private confirmPassword: string = "";
    private _errorMessage: string | null = null;


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
        this._Mark = mark;
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
        this._Model = model;
        this._model = model;
    }
    
    submit(): void {
        if(this.password==null){
            alert('Please enter the password');
            return
        }
        if(this._firstname==null){
            alert('Please enter firstname');
            return
        }
        if(this._lastname==null){
            alert('Please enter last name');
            return
        }
        if(this._phoneNr==null){
            alert('Please enter phone number');
            return
        }
        if(this._model==null){
            alert('Please select model');
            return
        }
        if(this._mark==null){
            alert('Please select mark');
            return
        }
        if(this.confirmPassword==null){
            alert('please confirm your password');
            return
        }
        if(this._email == null){
            alert('you forgot to enter your email');
            return
        }
        if( this.password != this.confirmPassword){
            alert('passwords do not match');
            return
        }
        if(this.password.length < 4){
            alert(this.confirmPassword)
            alert('password must be 4 or more characters long');
            return
        }
        if(this._email.length == 0){
            alert('you forgot to enter your email');
            return
        }


      this.accountService.register(this._email, this.password, this._firstname, this._lastname, this._phoneNr, this._mark, this._model)
      .then(response => {
        console.log(response.token);
        if (response !== undefined) {
        this.appState.jwt = response.token;

            this.accountService.login(this._email, this.password).then(
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
      });
    }
}
