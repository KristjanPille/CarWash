import { autoinject, PLATFORM } from 'aurelia-framework';
import {NavigationInstruction, RouteConfig, Router, RouterConfiguration} from 'aurelia-router';
import { AppState } from 'state/app-state';

import {AccountService} from "./service/account-service";
import {IAccount} from "./domain/IAccount";
import {ICar} from "./domain/ICar";
import {IFetchResponse} from "./types/IFetchResponse";
import {AlertType} from "./types/AlertType";
import {IAlertData} from "./types/IAlertData";

@autoinject
export class App {
  private _alert: IAlertData | null = null;
  router?: Router;
  private userId: string = "";
  private _account?: IAccount;

  constructor(private appState: AppState, private accountService: AccountService) {
  }

    attached() {
        if (this.appState.jwt != null){
            this.accountService.getUser().then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._account = response.data!;
                        this.userId = this._account.id
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

  configureRouter(config: RouterConfiguration, router: Router): void { 
  this.router = router;

  config.title = 'Car Wash'; 

  config.map([
    { route: ['', 'home', 'home/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' }, 

    { route: ['account/login'], name: 'account-login', moduleId: 
    PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
    { route: ['account/register'], name: 'account-register', moduleId: 
    PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },
    { route: ['account/account'], name: 'account-index', moduleId:
    PLATFORM.moduleName('views/account/account'), nav: false, title: 'Your Account' },


    { route: ['campaigns', 'Campaigns/index'], name: 'campaigns-index', moduleId:
    PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns' },
    { route: ['campaigns/details/:id'], name: 'campaigns-details', moduleId:
    PLATFORM.moduleName('views/campaigns/details'), nav: false, title: 'Campaign Details' }, 
    { route: ['campaigns/create'], name: 'campaigns-create', moduleId: 
    PLATFORM.moduleName('views/campaigns/create'), nav: false, title: 'Campaigns Create' },
    { route: ['campaigns/delete/:id?'], name: 'campaigns-delete', moduleId:
    PLATFORM.moduleName('views/campaigns/delete'), nav: false, title: 'Campaigns Delete' },
    { route: ['campaigns/edit/:id?'], name: 'campaigns-edit', moduleId:
    PLATFORM.moduleName('views/campaigns/edit'), nav: false, title: 'Campaigns Edit' },


    { route: ['Cars', 'Cars/index'], name: 'Cars-index', moduleId:
    PLATFORM.moduleName('views/Cars/index'), nav: true, title: 'Your Cars'},
    { route: ['Cars/create'], name: 'Cars-create', moduleId:
    PLATFORM.moduleName('views/Cars/create'), nav: false, title: 'Add new Car' },
    { route: ['Cars/delete/:id?'], name: 'Cars-delete', moduleId:
    PLATFORM.moduleName('views/Cars/delete'), nav: false, title: 'Cars Delete' },
    { route: ['Cars/edit/:id?'], name: 'Cars-edit', moduleId:
    PLATFORM.moduleName('views/Cars/edit'), nav: false, title: 'Edit Car' },

    { route: ['IsInServices', 'IsInServices/index'], name: 'Services', moduleId:
    PLATFORM.moduleName('views/IsInServices/index'), nav: false, title: 'IsInServices' },

    { route: ['Payments', 'Payments/index/:carId/:serviceId/:PaymentAmount'], name: 'payments-index', moduleId:
    PLATFORM.moduleName('views/Payments/index'), nav: false, title: 'Payments' },

    { route: ['Orders', 'Orders/index'], name: 'Orders-Index', moduleId:
    PLATFORM.moduleName('views/Orders/index'), nav: false, title: 'Orders' },

  ]);
  config.mapUnknownRoutes('views/home/index');
  }

  logoutOnClick(){
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
