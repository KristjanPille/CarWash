import { autoinject, PLATFORM } from 'aurelia-framework';
import {NavigationInstruction, RouteConfig, Router, RouterConfiguration} from 'aurelia-router';
import { AppState } from 'state/app-state';

import {AccountService} from "./service/account-service";
import {IAccount} from "./domain/IAccount";
import {ICar} from "./domain/ICar";
import {IFetchResponse} from "./types/IFetchResponse";
import {AlertType} from "./types/AlertType";
import {IAlertData} from "./types/IAlertData";
import {AdminSection} from "./views/AdminSection";

@autoinject
export class App {
  private _alert: IAlertData | null = null;
  router?: Router;
  private userId: string = "";
  private _account?: IAccount;
    private isAdmin = false;

  constructor(private appState: AppState, private accountService: AccountService) {
  }

    activate(){

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

    { route: ['account/login'], name: 'account-login', moduleId: 
    PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
    { route: ['account/register'], name: 'account-register', moduleId: 
    PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },
    { route: ['account/account'], name: 'account-index', moduleId:
    PLATFORM.moduleName('views/account/account'), nav: false, title: 'Your Account' },


    { route: ['campaigns', 'Campaigns/index'], name: 'campaigns-index', moduleId:
    PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns' },

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

    { route: ['Admin-Orders', 'Orders/AdminIndex'], name: 'Admin-Index', moduleId:
    PLATFORM.moduleName('views/Orders/AdminIndex'), nav: true, title: 'AdminOrders' },

    { route: ['Admin-Section', 'AdminSection/index'], name: 'Admin-Section', moduleId:
    PLATFORM.moduleName('views/AdminSection/index'), nav: true, title: 'AdminSection' },

    { route: ['AdminSection/serviceEdit/:id?'], name: 'service-edit', moduleId:
            PLATFORM.moduleName('views/AdminSection/serviceEdit'), nav: false, title: 'Services Edit' },
    { route: ['AdminSection/serviceCreate'], name: 'service-create', moduleId:
            PLATFORM.moduleName('views/AdminSection/serviceCreate'), nav: false, title: 'Services Create' },

    { route: ['AdminSection/campaignEdit/:id?'], name: 'campaign-edit', moduleId:
            PLATFORM.moduleName('views/AdminSection/campaignEdit'), nav: false, title: 'Campaigns Edit' },
      { route: ['AdminSection/campaignCreate'], name: 'campaign-create', moduleId:
              PLATFORM.moduleName('views/AdminSection/campaignCreate'), nav: false, title: 'Campaigns Create' },

  ]);
  config.mapUnknownRoutes('views/IsInServices/index');
  }

  logoutOnClick(){
    this.isAdmin = false;
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
