import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ServiceService } from 'service/service-service';
import { IServiceCreate } from 'domain/IServiceCreate';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class WashesCreate {
    private _alert: IAlertData | null = null;


    _NameOfService = "";
    _Description = "";
    _PriceOfService = 0;

    constructor(private ServiceService: ServiceService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.ServiceService
            .createService({ NameOfService: this._NameOfService, Description: this._Description, PriceOfService: this._PriceOfService })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Services-index', {});
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
