import { ICampaign } from 'domain/ICampaign';
import {autoinject, observable} from 'aurelia-framework';
import { CampaignService } from 'service/campaign-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {CultureService} from "../../service/culture-service";
import {ICulture} from "../../domain/ICulture";
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";
import {LayoutResources} from "../../lang/LayoutResources";

@connectTo()
@autoinject
export class CampaignsIndex{
   private _campaigns: ICampaign[] = [];
   private _alert: IAlertData | null = null;
    private enGB: ICulture[] = [];
    private etEE: ICulture[] = [];
    private currentLang: ICulture[] = [];
    private campaign = "";
    private description = "";
    private discountAmount = "";
    private langResources = LayoutResources;
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private campaignService: CampaignService, private appState: AppState,  private cultureService: CultureService){
        this.cultureService.getLanguageResources("Campaign", "en-GB").then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.enGB = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        )
        this.cultureService.getLanguageResources("Campaign","et-EE").then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.etEE = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        )

    }

    private stateChanged(newValue: IState): void {
        if(newValue.selectedCulture.code == "en-GB"){
            this.currentLang = this.enGB;
        }
        else if(newValue.selectedCulture.code == "et-EE"){
            this.currentLang = this.etEE;
        }
        else if(this.state.selectedCulture.code == "et-EE"){
            this.currentLang = this.etEE;
        }
        else if(this.state.selectedCulture.code == "en-GB"){
            this.currentLang = this.enGB;
        }
        if(this.currentLang.length > 1){
            // @ts-ignore
            this.campaign = this.currentLang.find(l => l.code == "NameOfCampaign").name
            // @ts-ignore
            this.description = this.currentLang.find(l => l.code == "Description").name
            // @ts-ignore
            this.discountAmount = this.currentLang.find(l => l.code == "DiscountAmount").name
        }
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

        if(this.state.selectedCulture.code == "et-EE"){
            this.currentLang = this.etEE;
        }
        else if(this.state.selectedCulture.code == "en-GB"){
            this.currentLang = this.enGB;
        }

        // @ts-ignore
        this.campaign = this.currentLang.find(l => l.code == "NameOfCampaign").name
        // @ts-ignore
        this.description = this.currentLang.find(l => l.code == "Description").name
        // @ts-ignore
        this.discountAmount = this.currentLang.find(l => l.code == "DiscountAmount").name
    }


}
