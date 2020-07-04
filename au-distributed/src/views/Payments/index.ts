import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {PaymentService} from "../../service/payment-service";
import {ICar} from "../../domain/ICar";
import {IPaymentMethod} from "../../domain/IPaymentMethod";
import {IService} from "../../domain/IService";


@autoinject
export class PaymentsIndex {
    private _alert: IAlertData | null = null;
    private car?: ICar;
    private service?: IService;
    private PaymentAmountWithVAT = 0;
    private PaymentMethod: any
    private _paymentMethods: IPaymentMethod[] = [];

    constructor(private paymentService: PaymentService, private router: Router) {

    }

    attached() {
        this.paymentService.getPaymentMethods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paymentMethods = response.data!;
                    console.log(response.data!)
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.car = params.car;
        this.service = params.service;
        this.PaymentAmountWithVAT = this.service!.priceOfService * 1.2
    }

    paymentSelect(paymentMethod: string){
       if (paymentMethod == 'PayPal'){
           // @ts-ignore
           document.getElementById('card').style.display = 'none';
           // @ts-ignore
           document.getElementById('paypal').style.display = 'initial';
       }
       if (paymentMethod == 'Credit Card'){
           // @ts-ignore
           document.getElementById('paypal').style.display = 'none';
           // @ts-ignore
           document.getElementById('card').style.display = 'initial';
       }
    }

    // TODO Now payment entity is possible datetime is coming from backend, CHECK Enity also is made in backend



}
