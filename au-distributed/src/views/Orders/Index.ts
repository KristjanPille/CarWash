import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {PaymentService} from "../../service/payment-service";
import {OrderService} from "../../service/order-service";
import {IModel} from "../../domain/IModel";
import {IOrder} from "../../domain/IOrder";
import {IIsInService} from "../../domain/IIsInService";
import {CarService} from "../../service/car-service";
import {IsInServiceService} from "../../service/isInService-service";
import {ServiceService} from "../../service/service-service";
import {IService} from "../../domain/IService";
import {ICar} from "../../domain/ICar";

@autoinject
export class OrdersIndex {
    private _alert: IAlertData | null = null;
    private _orders: IOrder[] = [];
    private _isInServices: IIsInService[] = [];
    private oldOrders: IOrder[] = [];
    private _services: IService[] = [];
    private newOrders: IOrder[] = [];
    private weekdays: any;
    private price = 0;
    private model = '';
    private mark = '';

    constructor(private orderService: OrderService, private carService: CarService, private serviceService: ServiceService, private router: Router) {
        this.weekdays =new Array(7);
        this.weekdays[0]="Sunday";
        this.weekdays[1]="Monday";
        this.weekdays[2]="Tuesday";
        this.weekdays[3]="Wednesday";
        this.weekdays[4]="Thursday";
        this.weekdays[5]="Friday";
        this.weekdays[6]="Saturday";
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    attached() {
        this.orderService.getOrders().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._orders = response.data!;
                    this.separateOldOrders();
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
        this.serviceService.getServices().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._services = response.data!;
                    this.separateOldOrders();
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

    separateOldOrders(){
        let currentDate = new Date();
        currentDate.setDate(new Date().getDate());
        for (let i = 0; i < this._orders.length; i++) {
            let date = Date.parse(this._orders[i].from)
            let newDate = new Date(date);
            if (newDate < currentDate) {
                this._orders[i].from = this.stringToDate(this._orders[i].from);
                this._orders[i].to = this.stringToDate(this._orders[i].to);
                this.oldOrders.push(this._orders[i])
            }
            else{
                this._orders[i].from = this.stringToDate(this._orders[i].from);
                this._orders[i].to = this.stringToDate(this._orders[i].to);
                this.newOrders.push(this._orders[i])
            }
        }
    }

    stringToDate(strDate: string){
        let date = Date.parse(strDate)
        let newDate = new Date(date);
        newDate.setDate(newDate.getDate());
        let month = newDate.getMonth() + 1;

        let options = {hour: "numeric", minute: "numeric"};
        let test = Intl.DateTimeFormat("en-GB", options).format(newDate);

        let time =  test + '/' + this.weekdays[newDate.getDay()] + ' ' + newDate.getDate() + '/' + month + '/' + newDate.getFullYear()
        return time;
    }

    getServiceName(serviceId: string){
        let service = this._services.find(service => service.id == serviceId)
        return service!.nameOfService
    }

    getCar(carId: string){
        this.carService.getCar(carId).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.mark = response.data!.mark;
                    this.model = response.data!.model;
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


    getServicePrice(serviceId: string, carId: string) {
        let priceOfService = 0;
        let service = this._services.find(service => service.id == serviceId)

        this.carService.getCar(carId).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.serviceService.getPriceOfService(response.data!, serviceId).then(
                        response => {
                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                this._alert = null;
                                priceOfService = response.data!;
                                this.price = response.data!;
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
}
