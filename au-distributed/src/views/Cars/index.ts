import { ICar } from 'domain/ICar';
import { autoinject } from 'aurelia-framework';
import { CarService } from 'service/car-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class CarsIndex{
    private _cars: ICar[] = [];
   private _alert: IAlertData | null = null;


    constructor(private carService: CarService){

    }

    attached() {
        this.carService.getCars().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._cars = response.data!;
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