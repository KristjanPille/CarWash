import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaign-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {IServiceEdit} from "../../domain/IServiceEdit";
import {ServiceService} from "../../service/service-service";
import {ICampaign} from "../../domain/ICampaign";
import {ICar} from "../../domain/ICar";
import {IServiceWTHCampaign} from "../../domain/IServiceWTHCampaign";

@autoinject
export class CampaignsEdit {
    private _alert: IAlertData | null = null;

    private _service?: IServiceEdit;
    private _campaign?: ICampaign;
    private _serviceWTHCampaign?: IServiceWTHCampaign;
    private _emptyCampaign = {id: "00000000-0000-0000-0000-000000000000", nameOfCampaign: "None", description: "None", discountAmount: 0.0}
    private _campaigns: ICampaign[] = [];

    constructor(private serviceService: ServiceService, private router: Router, private campaignService: CampaignService) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.serviceService.getAdminService(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._service = response.data!;
                        console.log(response.data!)
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
        this.campaignService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._campaigns = response.data!;
                    this._campaigns.push(this._emptyCampaign)
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

    bindCampaign(camapaign: ICampaign){
        this._campaign = camapaign;
    }

    onSubmit(event: Event) {
        if(this._campaign && this._campaign.id != "00000000-0000-0000-0000-000000000000"){
            this._service!.duration = Number(this._service!.duration);
            this._service!.priceOfService = Number(this._service!.priceOfService);
            this._service!.campaignId = this._campaign.id;
            this.serviceService
                .updateService(this._service!)
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
        }

        else{
            this._serviceWTHCampaign = {
                id: this._service!.id,
                description: this._service!.description,
                duration: this._service!.duration,
                nameOfService: this._service!.nameOfService,
                priceOfService: this._service!.priceOfService
            };

            this._serviceWTHCampaign!.priceOfService = Number( this._serviceWTHCampaign!.priceOfService)
            this._serviceWTHCampaign!.duration = Number( this._serviceWTHCampaign!.duration)
            this.serviceService
                .updateServiceWTHCampaign(this._serviceWTHCampaign!)
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
        }

        event.preventDefault();
    }
}
