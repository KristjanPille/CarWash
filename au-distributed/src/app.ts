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
    PLATFORM.moduleName('views/Cars/index'), nav: true, title: 'Cars' }, 
    { route: ['Cars/details/:id'], name: 'Cars-details', moduleId:
    PLATFORM.moduleName('views/Cars/details'), nav: false, title: 'Car Details' }, 
    { route: ['Cars/create'], name: 'Cars-create', moduleId: 
    PLATFORM.moduleName('views/Cars/create'), nav: false, title: 'Cars Create' },
    { route: ['Cars/delete/:id?'], name: 'Cars-delete', moduleId:
    PLATFORM.moduleName('views/Cars/delete'), nav: false, title: 'Cars Delete' },
    { route: ['Cars/edit/:id?'], name: 'Cars-edit', moduleId:
    PLATFORM.moduleName('views/Cars/edit'), nav: false, title: 'Cars Edit' },


    { route: ['Washes', 'Washes/index'], name: 'Washes-index', moduleId: 
    PLATFORM.moduleName('views/Washes/index'), nav: true, title: 'Washes' },
    { route: ['Washes/details/:id?'], name: 'Washes-details', moduleId: 
    PLATFORM.moduleName('views/Washes/details'), nav: false, title: 'Washes Details' },
    { route: ['Washes/edit/:id?'], name: 'Washes-edit', moduleId: 
    PLATFORM.moduleName('views/Washes/edit'), nav: false, title: 'Washes Edit' },
    { route: ['Washes/delete/:id?'], name: 'Washes-delete', moduleId: 
    PLATFORM.moduleName('views/Washes/delete'), nav: false, title: 'Washes Delete' },
    { route: ['Washes/create'], name: 'Washes-create', moduleId: 
    PLATFORM.moduleName('views/Washes/create'), nav: false, title: 'Washes Create' },


    { route: ['Discounts', 'Discounts/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Discounts/index'), nav: true, title: 'Discounts' }, 

    { route: ['IsInWashes', 'IsInWashes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/IsInWashes/index'), nav: true, title: 'IsInWashes' }, 

    { route: ['Orders', 'Orders/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Orders/index'), nav: true, title: 'Orders' }, 

    { route: ['PaymentMethods', 'PaymentMethods/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/PaymentMethods/index'), nav: true, title: 'PaymentMethods' }, 

    { route: ['Payments', 'Payments/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Payments/index'), nav: true, title: 'Payments' }, 

    { route: ['Shared', 'Shared/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Shared/index'), nav: true, title: 'Shared' }, 

    { route: ['WashTypes', 'WashTypes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/WashTypes/index'), nav: true, title: 'WashTypes' }, 
  ]);
  config.mapUnknownRoutes('views/home/index');
  }

  logoutOnClick(){
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
