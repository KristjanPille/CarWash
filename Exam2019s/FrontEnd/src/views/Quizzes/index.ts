import { ICampaign } from 'domain/ICampaign';
import {autoinject, observable} from 'aurelia-framework';

import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";

@connectTo()
@autoinject
export class QuizzesIndex{
   private _quizzes: ICampaign[] = [];
   private _alert: IAlertData | null = null;
    private campaign = "";
    private description = "";
    private discountAmount = "";
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private quizzeservice: quizzeservice, private appState: AppState){
    }

    async attached() {
        await this.quizzeservice.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizzes = response.data!;
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
