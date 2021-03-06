import {IAlertData} from "../../types/IAlertData";
import {IService} from "../../domain/IService";
import {ServiceService} from "../../service/service-service";
import {AlertType} from "../../types/AlertType";
import {autoinject} from "aurelia-framework";
import {CarService} from "../../service/car-service";
import {ICar} from "../../domain/ICar";
import {IIsInService} from "../../domain/IIsInService";
import {NavigationInstruction, RouteConfig, Router} from "aurelia-router";
import {IsInServiceService} from "../../service/isInService-service";
import {ICampaign} from "../../domain/ICampaign";
import {CampaignService} from "../../service/campaign-service";
import {AppState} from "../../state/app-state";
import {connectTo, Store} from "aurelia-store";
import {IState} from "../../state/state";
import {CultureService} from "../../service/culture-service";
import {IndexResources} from "../../lang/IndexResources";
import {BindingEngine} from 'aurelia-binding';
import {observable} from "aurelia-framework";
import {LayoutResources} from "../../lang/LayoutResources";
import {ICulture} from "../../domain/ICulture";

@connectTo()
@autoinject
export class IsInServicesIndex{

    private _services: IService[] = [];
    private _cars: ICar[] = [];
    private _campaigns: ICampaign[] = [];
    private _isInServices: IIsInService[] = [];
    private _bookedID: number[] = [];
    private _alert: IAlertData | null = null;
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
    private Discount =  "Discount: ";
    private clickedCell = 0;
    private selectedServiceIndex = 0;
    private indexResources = IndexResources;
    private langResources = LayoutResources;
    @observable
    protected state!: IState;


    constructor(private store: Store<IState>, private cultureService: CultureService, private serviceService: ServiceService, private carService: CarService,  private campaignService: CampaignService, private isInServiceService: IsInServiceService, private router: Router,  private appState: AppState){
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    private stateChanged(newValue: IState): void {
        this.weekdays =new Array(7);
        // @ts-ignore
        this.weekdays[0]=this.indexResources[newValue.selectedCulture.code].Sunday;
        // @ts-ignore
        this.weekdays[1]=this.indexResources[newValue.selectedCulture.code].Monday;
        // @ts-ignore
        this.weekdays[2]=this.indexResources[newValue.selectedCulture.code].Tuesday;
        // @ts-ignore
        this.weekdays[3]=this.indexResources[newValue.selectedCulture.code].Wednesday;
        // @ts-ignore
        this.weekdays[4]=this.indexResources[newValue.selectedCulture.code].Thursday;
        // @ts-ignore
        this.weekdays[5]=this.indexResources[newValue.selectedCulture.code].Friday;
        // @ts-ignore
        this.weekdays[6]=this.indexResources[newValue.selectedCulture.code].Saturday;
        // @ts-ignore
        this.Discount = this.indexResources[newValue.selectedCulture.code].Discount;
    }

    async attached() {
        // @ts-ignore
        document.getElementById('overlayService').style.display = 'none';
        await this.campaignService.getAll().then(
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

        this.getDates();
        await this.serviceService.getAll().then(
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
        if(this.appState.jwt != null) {
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
            )
        }
        if(this.appState.jwt != null){
        await this.isInServiceService.getIsInServices().then(
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

        this.weekdays =new Array(7);
        // @ts-ignore
        this.weekdays[0]=this.indexResources[this.state.selectedCulture.code].Sunday;
        // @ts-ignore
        this.weekdays[1]=this.indexResources[this.state.selectedCulture.code].Monday;
        // @ts-ignore
        this.weekdays[2]=this.indexResources[this.state.selectedCulture.code].Tuesday;
        // @ts-ignore
        this.weekdays[3]=this.indexResources[this.state.selectedCulture.code].Wednesday;
        // @ts-ignore
        this.weekdays[4]=this.indexResources[this.state.selectedCulture.code].Thursday;
        // @ts-ignore
        this.weekdays[5]=this.indexResources[this.state.selectedCulture.code].Friday;
        // @ts-ignore
        this.weekdays[6]=this.indexResources[this.state.selectedCulture.code].Saturday;

    }


    bindCar(car: ICar){
        this._car = car;
        for (let i = 0; i < this._services.length; i++) {
            this.getServicePrice(this._services[i].id, i)
        }
    }

    makeActiveServicesRed(){
        this.makePastServicesRed();
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
                document.getElementById(ID.toString()).style.backgroundColor = 'orangered';
                // @ts-ignore
                document.getElementById(ID.toString()).style.opacity = 0.6;
                this._bookedID.push(ID)
            }
            for (let i = 0; i < resultInMinutes / 30; i++) {
                ID += 5;
                if(document.getElementById(ID.toString())){
                    // @ts-ignore
                    document.getElementById(ID.toString()).style.backgroundColor = 'orangered';
                    // @ts-ignore
                    document.getElementById(ID.toString()).style.opacity = 0.6;
                    this._bookedID.push(ID)
                }
            }
        }
    }


    makePastServicesRed(){
        let fromDate = new Date()
        fromDate.setDate(new Date().getDate());
        fromDate.setHours(9,0,0,0)
        let hour = 0;
        let ID = 0;
        let toDate = new Date()
        toDate.setDate(new Date().getDate())
        let difference = toDate.getTime() - fromDate.getTime();
        let resultInMinutes = Math.round(difference / 60000);
        //Now add to ID hours minutes
        hour = fromDate.getHours() - 9
        if (fromDate.getMinutes() == 30) {
            ID += 5
        }
        ID += hour * 10;
        if(document.getElementById(ID.toString())){
            // @ts-ignore
            document.getElementById(ID.toString()).style.backgroundColor = 'orangered';
            // @ts-ignore
            document.getElementById(ID.toString()).style.opacity = 0.6;
        }
        for (let i = 0; i < resultInMinutes / 30; i++) {
            ID += 5;
            if(document.getElementById(ID.toString())){
                // @ts-ignore
                document.getElementById(ID.toString()).style.backgroundColor = 'orangered';
                // @ts-ignore
                document.getElementById(ID.toString()).style.opacity = 0.6;
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

    checkForCampaign(serviceId: string){
        let service = this._services.find(m => m.id == serviceId);
        if(service != null){
            if(service.campaignId != '00000000-0000-0000-0000-000000000000'){
                let campaign = this._campaigns.find(c => c.id == service!.campaignId)
                let campaignService = this._services.find(s => s.id == service!.id)
                if (campaign) {
                    return '-' + campaign!.discountAmount * 100 + '%';
                }

            }
        }
    }


    canMakeReservation(date: Date, hour: number, minute: number, clickedCell: number, clickedCellsID: number[]){
        let serviceCells = [];

        let copiedDate = new Date(date.getTime());
        let copiedDateFrom = new Date(date.getTime());
        if (this._service){
            copiedDate.setMinutes(copiedDateFrom.getMinutes() + this._service!.duration)
            copiedDateFrom.setMinutes(copiedDateFrom.getMinutes())

            let ID = 0;
            let fromDate = copiedDateFrom;
            let toDate = copiedDate!
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
                serviceCells.push(ID);
            }
            for (let i = 0; i < resultInMinutes / 30; i++) {
                ID += 5;
                if(document.getElementById(ID.toString())){
                    serviceCells.push(ID);
                }
            }
            if (!serviceCells.some(v => clickedCellsID.includes(v))){
                return true;
            }
            else{
                return false;
            }
        }
        return false;
    }

    getServiceTime(date: Date, hour: number, minute: number, clickedCell: number){
        if(this.appState.jwt != null) {
            let clickedCellsID = [];
            for (let i = 0; i < 73; i++) {
                // @ts-ignore
                if (document.getElementById(i.toString()).style.backgroundColor == 'orangered') {
                    clickedCellsID.push(i)
                }
            }
            if (!clickedCellsID.includes(clickedCell)) {
                this.clickedCell = clickedCell;
                if (minute != 0) {
                    date.setHours(hour, minute, 0, 0);
                    this._selectedDate = date;
                } else {
                    date.setHours(hour, 0, 0, 0);
                    this._selectedDate = date;
                }

                if (this._car) {
                    if (this.canMakeReservation(date, hour, minute, clickedCell, clickedCellsID)) {
                        if (confirm("Did you select correct date?")) {
                            if (this._service) {
                                // @ts-ignore
                                document.getElementById(this.clickedCell).style.backgroundColor = 'orangered';
                                // @ts-ignore
                                document.getElementById(this.clickedCell).style.opacity = 0.6;

                                this.createIsInService();
                            }
                            else{
                                confirm("Please select service")
                            }
                        }
                    } else {
                        confirm("Selected time overlaps with another")
                    }
                } else {
                    confirm("Please select car first")
                }

            } else {
                confirm("Overlaps with another service")
            }
        }
        else {

        }
    }



    getServicePrice(id: string, serviceIndex: number){
        if (this._car) {
            this.selectedServiceIndex = serviceIndex
            this.serviceService.getPriceOfService(this._car, id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._services[serviceIndex].priceOfService = response.data!;
                        this._services[serviceIndex].priceOfService = Math.round((this._services[serviceIndex].priceOfService) * 100) / 100;
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
        let service = this._services.find(s => s.id == this._service!.id)

        if(service != null){
            if(service.campaignId != '00000000-0000-0000-0000-000000000000'){
                let campaign = this._campaigns.find(c => c.id == service!.campaignId)
                let campaignService = this._services.find(s => s.id == service!.id)
                if (campaign) {
                    service.priceOfService = service.priceOfService * (1 - campaign!.discountAmount)
                }

            }
        }

        if (this._isInService){
            this.router.navigateToRoute('payments-index', {car: this._car, service: service, isInService: this._isInService});

        }
    }


    colorChanger(id: string, priceOfService: number){
        if(this.appState.jwt != null) {
            if (this._car) {
                //Selects current service also
                for (let i = 0; i < this._services.length; i++) {
                    // @ts-ignore
                    document.getElementById(this._services[i].id).style.backgroundColor = '#99aab5';
                }
                // @ts-ignore
                document.getElementById(id).style.backgroundColor = 'red';
                // @ts-ignore
                document.getElementById(id).style.transition = 'all 1s';
                this.serviceService.getService(id, priceOfService).then(
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
            } else {
                confirm("Please Select Car First")
            }
        }
    }

    showInfo(service: IService) {
        this.test = service.description;
        // @ts-ignore
        document.getElementById('overlayService').style.display = 'block';
    }
    hideInfo() {
        // @ts-ignore
        document.getElementById('overlayService').style.display = 'none';
    }

}
