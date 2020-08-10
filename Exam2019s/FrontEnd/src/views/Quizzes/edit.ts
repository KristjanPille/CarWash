import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {connectTo} from "aurelia-store";
import {CampaignService} from "../../service/question-service";
import {ICampaign} from "../../domain/ICampaign";

@connectTo()
@autoinject
export class CampaignsEdit {
    private _alert: IAlertData | null = null;

    _campaign: ICampaign;
    _nameOfCampaign = "";
    _description = "";
    _discountAmount = 0;

    constructor(private campaignService: CampaignService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.campaignService.getCampaign(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._campaign = response.data!;
                        this._nameOfCampaign = this._campaign.nameOfCampaign;
                        this._description = this._campaign.description;
                        this._discountAmount = this._campaign.discountAmount;
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
      this._campaign.nameOfCampaign = this._nameOfCampaign,
      this._campaign.description = this._description,
      this._campaign.discountAmount = this._discountAmount
        this.campaignService
            .updateCampaign(this._campaign!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Quizzes-index', {});
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
