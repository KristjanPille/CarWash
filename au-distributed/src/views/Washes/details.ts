import { WashService } from './../../service/wash-service';
import { IWash } from 'domain/IWash';
import { autoinject } from 'aurelia-framework';
import { CampaignService } from 'service/campaign-service';
import { NavigationInstruction, RouteConfig } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CampaignsDetails{
    private _wash?: IWash;    
    private _alert: IAlertData | null = null;

    constructor(private WashService: WashService){

    }

    attached() {
       
    }
    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.WashService.getWash(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._wash = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._wash = undefined;
                    }
                }                
            );
        }
    }

}