import {IAlertData} from "../../types/IAlertData";
import {IService} from "../../domain/IService";
import {ServiceService} from "../../service/service-service";
import {AlertType} from "../../types/AlertType";
import {autoinject} from "aurelia-framework";
import {CarService} from "../../service/car-service";
import {ICar} from "../../domain/ICar";
import {IIsInService} from "../../domain/IIsInService";
import 'bootstrap';

@autoinject
export class IsInServicesIndex{
    private _services: IService[] = [];
    private _cars: ICar[] = [];
    private _alert: IAlertData | null = null;
    private e: any;
    test = "nice"
    private _car?: ICar;
    private _isInService?: IIsInService;
    private _selectedDate?: Date;
    private _service?: IService;
    private priceOfService = 0;
    private FirstDay: any;
    private SecondDay: any;
    private ThirdDay: any;
    private FourthDay: any;
    private FifthDay: any;
    private weekdays: any;

    constructor(private serviceService: ServiceService, private carService: CarService){
        this.weekdays =new Array(7);
        this.weekdays[0]="Monday";
        this.weekdays[1]="Tuesday";
        this.weekdays[2]="Wednesday";
        this.weekdays[3]="Thursday";
        this.weekdays[4]="Friday";
        this.weekdays[5]="Saturday";
        this.weekdays[6]="Sunday";
    }

    attached() {
        this.getDate();
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
        this.colorChanger();
    }

    bindCar(car: ICar){
        this._car = car;
        for (let i = 0; i < this._services.length; i++) {
            this.getServicePrice(this._services[i].id, i)
        }
    }


    getDate(){
        this.FirstDay = new Date();
        this.SecondDay = new Date();
        this.ThirdDay = new Date();
        this.FourthDay = new Date();
        this.FifthDay = new Date();

        this.FirstDay.setDate(new Date().getDate());
        this.SecondDay.setDate(new Date().getDate() + 1);
        this.ThirdDay.setDate(this.SecondDay.getDate());
        this.FourthDay.setDate(new Date().getDate() + 3);
        this.FifthDay.setDate(new Date().getDate() + 4);
    }

    serviceSelect(service: IService){
        this._service = service;
    }

    getServiceTime(date: Date, hour: number, minute: number){
        if(minute != 0){
            date.setHours( hour,minute,0,0 );
            this._selectedDate = date;
        }
        else
        date.setHours( hour,0,0,0 );
        this._selectedDate = date;
    }

    getServicePrice(id: string, serviceIndex: number){
        if (this._car) {
            this.serviceService.getPriceOfService(this._car, id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._services[serviceIndex].priceOfService = response.data!;
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

    colorChanger(){

    }

    domouseover(service: IService) {
        this.test = service.description;
        // @ts-ignore
        document.getElementById('popup').style.display = 'block';
    }

    domouseout() {
        // @ts-ignore
        document.getElementById('popup').style.display = 'none';
    }
}
