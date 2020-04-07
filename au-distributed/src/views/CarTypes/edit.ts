import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarTypeService } from 'service/carType-service';
import { ICarType } from 'domain/ICarType';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CarTypesEdit {
    private _alert: IAlertData | null = null;

    private _carType?: ICarType;

    constructor(private carTypeService: CarTypeService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.carTypeService.getCarType(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._carType = response.data!;
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
        console.log(event);
        this.carTypeService
            .updateCarType(this._carType!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('carTypes-index', {});
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
