import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarService } from 'service/car-service';
import { ICar } from 'domain/ICar';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {IMark} from "../../domain/IMark";
import {IModel} from "../../domain/IModel";
import {ModelMarkservice} from "../../service/modelMark-service";
import {connectTo} from "aurelia-store";
import {LayoutResources} from "../../lang/LayoutResources";
import {IndexResources} from "../../lang/IndexResources";

@connectTo()
@autoinject
export class CarsEdit {
    private _alert: IAlertData | null = null;
    private langResources = LayoutResources;
    private indexResources = IndexResources;
    private _car?: ICar;
    private _marks: IMark[] = [];
    private _models: IModel[] = [];
    _Mark = "";
    _Model = "";
    _carMark = "";
    _carModel = "";

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

    getModels(mark: string){
        this._Mark = mark;
        this._car!.mark = mark;
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
        this._car!.model = model;
        this._Model = model;
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.carService.getCar(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._car = response.data!;
                        this._carMark = this._car.mark;
                        this._carModel = this._car.model;
                        this._Mark = this._car.mark;
                        this._Model = this._car.model;
                        this.getModels(this._Mark);
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
    }

    onSubmit(event: Event) {
        this.carService
            .updateCar(this._car!)
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
