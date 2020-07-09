import {autoinject} from 'aurelia-framework';
import {NavigationInstruction, RouteConfig, Router} from 'aurelia-router';
import {IAlertData} from 'types/IAlertData';
import {AlertType} from 'types/AlertType';
import {PaymentService} from "../../service/payment-service";
import {OrderService} from "../../service/order-service";
import {IIsInService} from "../../domain/IIsInService";
import {CarService} from "../../service/car-service";
import {ServiceService} from "../../service/service-service";
import {IService} from "../../domain/IService";
import {ICar} from "../../domain/ICar";
import {IPayment} from "../../domain/IPayment";

@autoinject
export class OrdersIndex {
    private _alert: IAlertData | null = null;
    private _payments: IPayment[] = [];
    private _cars: ICar[] = [];
    private _isInServices: IIsInService[] = [];
    private oldPayments: IPayment[] = [];
    private _services: IService[] = [];
    private newPayments: IPayment[] = [];
    private weekdays: any;
    private price = 0;
    private model = '';
    private mark = '';

    constructor(private orderService: OrderService, private carService: CarService, private serviceService: ServiceService, private router: Router, private paymentService: PaymentService) {
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
        )
        this.paymentService.getPayments().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._payments = response.data!;
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
        this.serviceService.getAll().then(
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
        )
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
        )
    }

    separateOldOrders(){
        let currentDate = new Date();
        currentDate.setDate(new Date().getDate());
        for (let i = 0; i < this._payments.length; i++) {
            let date = Date.parse(this._payments[i].from)
            let newDate = new Date(date);
            if (newDate < currentDate) {
                this._payments[i].from = this.stringToDate(this._payments[i].from);
                this._payments[i].to = this.stringToDate(this._payments[i].to);
                this.oldPayments.push(this._payments[i])
            }
            else{
                this._payments[i].from = this.stringToDate(this._payments[i].from);
                this._payments[i].to = this.stringToDate(this._payments[i].to);
                this.newPayments.push(this._payments[i])
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
        let car = this._cars.find(car => car.id == carId);
        if(car){
            return car.mark + ' ' + car.model
        }
    }
}
