import { WashesCreate } from './create';
import { IWash } from 'domain/IWash';
import { autoinject } from 'aurelia-framework';
import { WashService } from 'service/wash-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class WashesIndex{
    private _washes: IWash[] = [];
   private _alert: IAlertData | null = null;


    constructor(private washService: WashService){

    }

    attached() {
        this.washService.getWashes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._washes = response.data!;
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