import {IAlertData} from "../../types/IAlertData";
import {IService} from "../../domain/IService";
import {ServiceService} from "../../service/service-service";
import {AlertType} from "../../types/AlertType";
import {autoinject} from "aurelia-framework";
import {CarService} from "../../service/car-service";
import {ICar} from "../../domain/ICar";
import {IIsInService} from "../../domain/IIsInService";
import {Router} from "aurelia-router";
import {IsInServiceService} from "../../service/isInService-service";

@autoinject
export class IsInServicesIndex{
    private _services: IService[] = [];
    private _cars: ICar[] = [];
    private _isInServices: IIsInService[] = [];
    private _alert: IAlertData | null = null;
    private e: any;
    test = "nice"
    private _car?: ICar;
    private _isInService?: IIsInService;
    private _selectedDate?: Date;
    private _toDate?: Date;
    private _service?: IService;
    private priceOfService = 0;
    private FirstDay: any;
    private SecondDay: any;
    private ThirdDay: any;
    private FourthDay: any;
    private FifthDay: any;
    private weekdays: any;
    private clickedCell = 0;

    constructor(private serviceService: ServiceService, private carService: CarService, private isInServiceService: IsInServiceService, private router: Router){
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
        this.getDates();
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
        this.isInServiceService.getIsInServices().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._isInServices = response.data!;
                    this.makeActiveServicesRed();
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

    bindCar(car: ICar){
        this._car = car;
        for (let i = 0; i < this._services.length; i++) {
            this.getServicePrice(this._services[i].id, i)
        }
    }

    makeActiveServicesRed(){
        for (let i = 0; i < this._isInServices.length; i++) {
            let hour = 0;
            let ID = 0;
            let fromDate = new Date(this._isInServices[i].from)
            let toDate = new Date(this._isInServices[i].to)
            let difference = toDate.getTime() - fromDate.getTime();
            let resultInMinutes = Math.round(difference / 60000);
            if (fromDate.getDay() == this.SecondDay.getDay()) {
                ID += 1
            } else if (fromDate.getDay() == this.ThirdDay.getDay()) {
                ID += 2
            } else if (fromDate.getDay() == this.FourthDay.getDay()) {
                ID += 3
            } else if (fromDate.getDay() == this.FifthDay.getDay()) {
                ID += 4
            }
            //Now add to ID hours minutes
            hour = fromDate.getHours() - 9
            if (fromDate.getMinutes() == 30) {
                ID += 5
            }
            ID += hour * 10;
            if(document.getElementById(ID.toString())){
                // @ts-ignore
                document.getElementById(ID.toString()).style.backgroundColor = 'red';
                // @ts-ignore
                document.getElementById(ID.toString()).style.opacity = 0.7;
            }
            for (let i = 0; i < resultInMinutes / 30; i++) {
                ID += 5;
                if(document.getElementById(ID.toString())){
                    // @ts-ignore
                    document.getElementById(ID.toString()).style.backgroundColor = 'red';
                    // @ts-ignore
                    document.getElementById(ID.toString()).style.opacity = 0.7;
                }
            }
        }
    }


    getDates(){
        this.FirstDay = new Date();
        this.SecondDay = new Date();
        this.ThirdDay = new Date();
        this.FourthDay = new Date();
        this.FifthDay = new Date();

        this.FirstDay.setDate(new Date().getDate());
        this.SecondDay.setDate(new Date().getDate() + 1);
        this.ThirdDay.setDate(new Date().getDate() + 2);
        this.FourthDay.setDate(new Date().getDate() + 3);
        this.FifthDay.setDate(new Date().getDate() + 4);

    }

    serviceSelect(service: IService){
        this._service = service;
    }

    getServiceTime(date: Date, hour: number, minute: number, clickedCell: number){
        let clickedCellsID = [];
        for (let i = 0; i < 73; i++) {
            // @ts-ignore
            if(document.getElementById(i.toString()).style.backgroundColor == 'red'){
                clickedCellsID.push(i)
            }
        }
        if(!clickedCellsID.includes(clickedCell)){
            this.clickedCell = clickedCell;
            if(minute != 0){
                date.setHours(hour, minute,0,0 );
                this._selectedDate = date;
            }
            else {
                date.setHours(hour, 0, 0, 0);
                this._selectedDate = date;
            }

            if (confirm("Did you select correct date?")) {
                if(this._service){
                    // @ts-ignore
                    document.getElementById(this.clickedCell).style.backgroundColor = 'red';
                    this.createIsInService();
                }
            } else {
            }
        }
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

    createIsInService(){
        //TO: "2020-01-01T00:00:00"
        let copiedDate = new Date(this._selectedDate!.getTime());
        copiedDate.setMinutes(this._selectedDate!.getMinutes() + this._service!.duration + 180)
        this._selectedDate!.setMinutes(this._selectedDate!.getMinutes() + 180)
        // @ts-ignore
        this._isInService = {
            carId: this._car!.id,
            serviceId: this._service!.id,
            from: this._selectedDate!.toISOString(),
            to: copiedDate.toISOString(),
        }
        this.sendIsInService();
    }

    sendIsInService(){
        if (this._isInService){
            this.serviceService
                .createNewIsInService(this._isInService!)
                .then(
                    response => {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            this.router.navigateToRoute('orders-index', {});
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


    colorChanger(id: string){
        if(this._car){
            //Selects current service also
            for (let i = 0; i < this._services.length; i++) {
                // @ts-ignore
                document.getElementById(this._services[i].id).style.backgroundColor = 'white';
            }
            // @ts-ignore
            document.getElementById(id).style.backgroundColor = 'red';
            // @ts-ignore
            document.getElementById(id).style.transition = 'all 1s';
            this.serviceService.getService(id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._service = response.data!;
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
        else{
            confirm("Please Select Car First")
        }
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

    modalCaller(){

    }
}
