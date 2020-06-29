import { CarsIndex } from './index';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarService } from 'service/car-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {ModelMarkservice} from "../../service/modelMark-service";
import {ICar} from "../../domain/ICar";
import {IModelMark} from "../../domain/IModelMark";

@autoinject
export class CarsCreate {
    private _alert: IAlertData | null = null;
    private _modelMarks: IModelMark[] = [];
    _Mark = "";
    _Model = "";

    constructor(private carService: CarService, private modelMarkService: ModelMarkservice, private router: Router) {

    }

    attached() {
        this.modelMarkService.getModelMarks().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._modelMarks = response.data!;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    onSubmit(event: Event) {
        console.log(event);

        this.carService
            .createCar({Mark: this._Mark, Model: this._Model })
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
