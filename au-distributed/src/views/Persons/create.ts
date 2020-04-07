import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaign-service';
import { ICampaign } from 'domain/ICampaign';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CampaignsCreate {
    private _alert: IAlertData | null = null;


    _NameOfCampaign = "";

    constructor(private campaignService: CampaignService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.campaignService
            .createCampaign({ NameOfCampaign: this._NameOfCampaign })
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
