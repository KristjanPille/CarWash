import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {PaymentService} from "../../service/payment-service";
import {ICar} from "../../domain/ICar";
import {IPaymentMethod} from "../../domain/IPaymentMethod";
import {IService} from "../../domain/IService";
import {IIsInService} from "../../domain/IIsInService";
import {ServiceService} from "../../service/service-service";
import {CarService} from "../../service/car-service";
import {CampaignService} from "../../service/campaign-service";
import {IsInServiceService} from "../../service/isInService-service";
import {ICampaign} from "../../domain/ICampaign";
import {IPayment} from "../../domain/IPayment";


@autoinject
export class PaymentsIndex {
    payPalEmail = '';
    cardNumber = '';
    expMonth = '';
    expYear = '';
    cvv = 0;
    Payment = 0;
    private _alert: IAlertData | null = null;
    private car?: ICar;
    private service?: IService;
    private isInService?: IIsInService;
    private PaymentMethod?: IPaymentMethod;
    private _paymentMethods: IPaymentMethod[] = [];
    private _campaigns: ICampaign[] = [];
    private test?: IPayment;

    constructor(private paymentService: PaymentService,private serviceService: ServiceService, private carService: CarService,  private campaignService: CampaignService, private isInServiceService: IsInServiceService, private router: Router) {

    }

    attached() {

        this.paymentService.getPaymentMethods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paymentMethods = response.data!;
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

        this.campaignService.getAll().then(
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
        )
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.car = params.car;
        this.service = params.service;
        this.isInService = params.isInService;

        this.checkForCampaign()
    }

    checkForCampaign(){
        if(this.service != null){
            console.log(this.service)
            if(this.service.campaignId != '00000000-0000-0000-0000-000000000000'){
                let campaign = this._campaigns.find(c => c.id == this.service!.campaignId)
                if (campaign) {
                    console.log(campaign)
                    this.service.priceOfService = this.service.priceOfService * (1 - campaign.discountAmount)
                }

            }
        }
    }

    paymentSelect(paymentMethod: string){
        this.PaymentMethod = this._paymentMethods.find(object => object.paymentMethodName == paymentMethod);

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

    onSubmit(event: Event) {
        let date = new Date();
        let timeOfPayment = new Date(date!.getTime());

        this.serviceService
            .createNewIsInService(this.isInService!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
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

        console.log(this.PaymentMethod!.id)
        console.log(this.car!.id)
        console.log(this.service!.id)
        console.log(this.service!.priceOfService)
        console.log(timeOfPayment.toISOString())
        console.log(this.payPalEmail)
        console.log(this.cardNumber)
        console.log(this.expMonth)
        console.log(this.expYear)
        console.log(this.cvv)
        console.log(this.isInService!.from)
        console.log(this.isInService!.to)

        this.Payment = this.service!.priceOfService

        this.test = { PaymentAmount: this.Payment,
            PaymentMethodId: this.PaymentMethod!.id, CarId: this.car!.id,
            ServiceId: this.service!.id, TimeOfPayment: timeOfPayment.toISOString(),
            PayPalEmail: this.payPalEmail, CreditCardNumber: this.cardNumber, ExpMonth: this.expMonth, ExpYear: this.expYear,
            CVV: this.cvv, from: this.isInService!.from, to: this.isInService!.to
        }
        console.log(this.test)

        this.paymentService
            .createPayment({ PaymentMethodId: this.PaymentMethod!.id, CarId: this.car!.id,
                ServiceId: this.service!.id, PaymentAmount: Number(this.Payment), TimeOfPayment: timeOfPayment.toISOString(),
                PayPalEmail: this.payPalEmail, CreditCardNumber: this.cardNumber, ExpMonth: this.expMonth, ExpYear: this.expYear,
                CVV: this.cvv, from: this.isInService!.from, to: this.isInService!.to })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Orders-Index', {});
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
