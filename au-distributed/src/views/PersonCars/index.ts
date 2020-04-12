import { PersonService } from 'service/person-service';
import { PersonCarService } from './../../service/PersonCar-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPersonCar } from 'domain/IPersonCar';

@autoinject
export class PersonCarsIndex {
    private _alert: IAlertData | null = null;
   
    private _personCars: IPersonCar[] = [];

    constructor(private personCarService:PersonCarService, private router: Router) {

    }

    attached() {
        this.personCarService.getPersonCars().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._personCars = response.data!;
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
}
