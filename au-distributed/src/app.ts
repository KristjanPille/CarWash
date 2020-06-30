import { autoinject, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';
import { AppState } from 'state/app-state';

@autoinject
export class App {
  router?: Router;

  constructor(private appState: AppState) {

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


    { route: ['Services', 'Services/index'], name: 'Services-index', moduleId:
    PLATFORM.moduleName('views/Services/index'), nav: true, title: 'Services' },
    { route: ['Services/details/:id?'], name: 'Services-details', moduleId:
    PLATFORM.moduleName('views/Services/details'), nav: false, title: 'Services Details' },
    { route: ['Services/edit/:id?'], name: 'Services-edit', moduleId:
    PLATFORM.moduleName('views/Services/edit'), nav: false, title: 'Services Edit' },
    { route: ['Services/delete/:id?'], name: 'Services-delete', moduleId:
    PLATFORM.moduleName('views/Services/delete'), nav: false, title: 'Services Delete' },
    { route: ['Services/create'], name: 'Services-create', moduleId:
    PLATFORM.moduleName('views/Services/create'), nav: false, title: 'Services Create' },

    { route: ['IsInServices', 'IsInServices/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/IsInServices/index'), nav: false, title: 'IsInServices' },

    { route: ['Orders', 'Orders/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Orders/index'), nav: false, title: 'Orders' },

    { route: ['PaymentMethods', 'PaymentMethods/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/PaymentMethods/index'), nav: false, title: 'PaymentMethods' },

    { route: ['Payments', 'Payments/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Payments/index'), nav: false, title: 'Payments' },

  ]);
  config.mapUnknownRoutes('views/home/index');
  }

  logoutOnClick(){
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
