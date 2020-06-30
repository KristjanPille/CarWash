import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarService } from 'service/car-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {ModelMarkservice} from "../../service/modelMark-service";
import {IMark} from "../../domain/IMark";
import {IModel} from "../../domain/IModel";

@autoinject
export class CarsCreate {
    private _alert: IAlertData | null = null;
    private _marks: IMark[] = [];
    private _models: IModel[] = [];
    _Mark = "";
    _Model = "";

    constructor(private carService: CarService, private modelMarkService: ModelMarkservice, private router: Router) {

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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    getModels(mark: string){
        this._Mark = mark;
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
    }

    onSubmit(event: Event) {
        console.log(event);
        this.carService
            .createCar({mark: this._Mark, model: this._Model })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Cars-index', {});
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
