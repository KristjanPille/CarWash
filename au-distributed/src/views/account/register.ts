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
            alert('1');
            return
        }
        if(this._firstname==null){
            alert('1');
            return
        }
        if(this._lastname==null){
            alert('1');
            return
        }
        if(this._phoneNr==null){
            alert('1');
            return
        }
        if(this._model==null){
            alert('1');
            return
        }
        if(this._mark==null){
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


      this.accountService.register(this._email, this.password, this._firstname, this._lastname, this._phoneNr, this._mark, this._model)
      .then(response => {
        console.log(response.token);
        if (response !== undefined) {
        this.appState.jwt = response.token;
          this.router.navigateToRoute('home');
        }
      });
    }
}
