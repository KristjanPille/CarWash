import { IService } from 'domain/IService';
import { autoinject } from 'aurelia-framework';
import { ServiceService } from 'service/service-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class ServicesIndex{
    private _services: IService[] = [];
   private _alert: IAlertData | null = null;


    constructor(private serviceService: ServiceService){

    }

    attached() {
        this.serviceService.getServices().then(
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
    }


}
