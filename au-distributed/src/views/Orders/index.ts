import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CarService } from 'service/car-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {ModelMarkservice} from "../../service/modelMark-service";
import {IMark} from "../../domain/IMark";
import {IModel} from "../../domain/IModel";
import {OrderService} from "../../service/order-service";

@autoinject
export class OrdersIndex {
    private _alert: IAlertData | null = null;

    constructor(private orderService: OrderService, private modelMarkService: ModelMarkservice, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    onSubmit(event: Event) {
        this.orderService
            .createOrder({})
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Cars-index', {});
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

        event.preventDefault();
    }


}
