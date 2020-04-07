import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarService } from 'service/car-service';
import { ICar } from 'domain/ICar';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CarsEdit {
    private _alert: IAlertData | null = null;

    private _car?: ICar;

    constructor(private carService: CarService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.carService.getCar(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._car = response.data!;
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
        this.carService
            .updateCar(this._car!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('campaigns-index', {});
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
