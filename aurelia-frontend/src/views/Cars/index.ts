import { ICar } from 'domain/ICar';
import { autoinject } from 'aurelia-framework';
import { CarService } from 'service/car-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {LayoutResources} from "../../lang/LayoutResources";
import {connectTo} from "aurelia-store";
import {IndexResources} from "../../lang/IndexResources";

@connectTo()
@autoinject
export class CarsIndex{
   private _cars: ICar[] = [];
   private _alert: IAlertData | null = null;
   private langResources = LayoutResources;
   private indexResources = IndexResources;

    constructor(private carService: CarService){

    }

    async attached() {
       await this.carService.getCars().then(
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
