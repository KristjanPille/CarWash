import { autoinject, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';

@autoinject
export class App {
  router?: Router;

  configureRouter(config: RouterConfiguration, router: Router): void { 
  this.router = router;
  config.title = 'Car Wash'; 
  config.map([
    { route: ['', 'home', 'home/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' }, 

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

    { route: ['cars', 'Cars/index'], name: 'cars-index', moduleId:
    PLATFORM.moduleName('views/cars/index'), nav: true, title: 'Cars' }, 
    //{ route: ['cars/details/:id'], name: 'cars-details', moduleId:
    //PLATFORM.moduleName('views/cars/details'), nav: false, title: 'Car Details' }, 


    { route: ['CarTypes', 'CarTypes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/CarTypes/index'), nav: true, title: 'CarTypes' }, 

    { route: ['Checks', 'Checks/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Checks/index'), nav: true, title: 'Checks' }, 

    { route: ['Discounts', 'Discounts/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Discounts/index'), nav: true, title: 'Discounts' }, 

    { route: ['IsInWashes', 'IsInWashes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/IsInWashes/index'), nav: true, title: 'IsInWashes' }, 

    { route: ['ModelMarks', 'ModelMarks/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/ModelMarks/index'), nav: true, title: 'ModelMarks' }, 

    { route: ['Orders', 'Orders/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Orders/index'), nav: true, title: 'Orders' }, 

    { route: ['PaymentMethods', 'PaymentMethods/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/PaymentMethods/index'), nav: true, title: 'PaymentMethods' }, 

    { route: ['Payments', 'Payments/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Payments/index'), nav: true, title: 'Payments' }, 

    { route: ['Persons', 'Persons/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Persons/index'), nav: true, title: 'Persons' }, 

    { route: ['PersonTypes', 'PersonTypes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/PersonTypes/index'), nav: true, title: 'PersonTypes' }, 

    { route: ['PersonTypes', 'PersonTypes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/PersonTypes/index'), nav: true, title: 'PersonTypes' }, 

    { route: ['Shared', 'Shared/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Shared/index'), nav: true, title: 'Shared' }, 

    { route: ['Washes', 'Washes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/Washes/index'), nav: true, title: 'Washes' }, 

    { route: ['WashTypes', 'WashTypes/index'], name: 'home', moduleId:
    PLATFORM.moduleName('views/WashTypes/index'), nav: true, title: 'WashTypes' }, 
  ]);
  config.mapUnknownRoutes('views/home/index');
  }
}