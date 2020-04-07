import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarTypeService } from 'service/carType-service';
import { ICarType } from 'domain/ICarType';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CarTypesCreate {
    private _alert: IAlertData | null = null;


    _Name = "";

    constructor(private carTypeService: CarTypeService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.carTypeService
            .createCarType({ Name: this._Name })
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
