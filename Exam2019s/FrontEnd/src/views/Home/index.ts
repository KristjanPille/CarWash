import { ICampaign } from 'domain/ICampaign';
import {autoinject, observable} from 'aurelia-framework';
import { CampaignService } from 'service/question-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";

@connectTo()
@autoinject
export class CampaignsIndex{
   private _campaigns: ICampaign[] = [];
   private _alert: IAlertData | null = null;
    private campaign = "";
    private description = "";
    private discountAmount = "";
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private campaignService: CampaignService, private appState: AppState){

    }

    async attached() {
        await this.campaignService.getAll().then(
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


}
