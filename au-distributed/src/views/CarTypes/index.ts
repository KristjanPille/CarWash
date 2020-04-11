import { ICarType } from 'domain/ICarType';
import { autoinject } from 'aurelia-framework';
import { CarTypeService } from 'service/carType-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CarTypesIndex{
    private _carTypes: ICarType[] = [];
   private _alert: IAlertData | null = null;


    constructor(private carTypeService: CarTypeService){

    }

    attached() {
        this.carTypeService.getCarTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    console.log(response.data)
                    this._carTypes = response.data!;
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