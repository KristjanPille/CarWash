import {autoinject, observable} from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";

@connectTo()
@autoinject
export class CampaignsIndex{
   private _alert: IAlertData | null = null;
    private campaign = "";
    private description = "";
    private discountAmount = "";
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private appState: AppState){

    }

    async attached() {

    }


}
