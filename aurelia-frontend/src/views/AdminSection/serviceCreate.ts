import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaign-service';
import { ICampaign } from 'domain/ICampaign';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {ServiceService} from "../../service/service-service";

@autoinject
export class ServicessCreate {
    private _alert: IAlertData | null = null;
    private nameOfService = "";
    private description = "";
    private priceOfService = 0;
    private duration = 0;

    constructor(private serviceService: ServiceService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.serviceService
            .createService({ nameOfService: this.nameOfService, description: this.description, priceOfService: this.priceOfService, duration: this.duration })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Admin-Section', {});
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
