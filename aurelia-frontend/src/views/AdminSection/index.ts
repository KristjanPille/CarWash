import { ICampaign } from 'domain/ICampaign';
import { autoinject } from 'aurelia-framework';
import { CampaignService } from 'service/campaign-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {ServiceService} from "../../service/service-service";
import {IService} from "../../domain/IService";
import {Router} from "aurelia-router";

@autoinject
export class AdminSection{
   private _alert: IAlertData | null = null;
   private _services: IService[] = [];
   private _campaigns: ICampaign[] = [];


    constructor(private serviceService: ServiceService, private appState: AppState, private campaignService: CampaignService, private router: Router){

    }

    attached() {
        this.serviceService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._services = response.data!;
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
        this.campaignService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._campaigns = response.data!;
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


    deleteCampaign(campaignId: string){
        console.log(campaignId)
        this.campaignService
            .deleteCampaign(campaignId)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Admin-Section', {});
                    }if (response.statusCode == 500){
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + "There are active services dependant on this campaign, first remove campaign from services that are using it",
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                    else {
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
    deleteService(serviceId: string){
        this.serviceService
            .deleteService(serviceId)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Admin-Section', {});
                    }
                    else {
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
