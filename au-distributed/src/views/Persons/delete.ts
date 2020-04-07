import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaign-service';
import { ICampaign } from 'domain/ICampaign';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CampaignsDelete {
    private _alert: IAlertData | null = null;

    private _campaign?: ICampaign;

    constructor(private campaignService: CampaignService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.campaignService.getCampaign(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._campaign = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        };
                        this._campaign = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.campaignService
            .deleteCampaign(this._campaign!.id)
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
