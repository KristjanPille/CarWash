import { autoinject, PLATFORM } from 'aurelia-framework';
import {NavigationInstruction, RouteConfig, Router, RouterConfiguration} from 'aurelia-router';
import { AppState } from 'state/app-state';
import {AccountService, log} from "./service/account-service";
import {IAccount} from "./domain/IAccount";
import {AlertType} from "./types/AlertType";
import {IAlertData} from "./types/IAlertData";
import {AdminSection} from "./views/AdminSection";
import { LayoutResources } from 'lang/LayoutResources';
import '../static/site.css';
import {CultureService} from "./service/culture-service";
import {IState} from "./state/state";
import {ICulture} from "./domain/ICulture";
import { Store, connectTo } from "aurelia-store";
import * as environment from '../config/environment.json';
import {HttpClient} from "aurelia-fetch-client";
import {IndexResources} from "./lang/IndexResources";

@connectTo()
@autoinject
export class App {
  private swaggerUrl = environment.swaggerUrl;
  private _alert: IAlertData | null = null;
  router?: Router;
  private userId: string = "";
  protected state!: IState;
  private _account?: IAccount;
  private isAdmin = false;
  private langResources = LayoutResources;
  private indexResources = IndexResources;

  constructor(private store: Store<IState>, private cultureService: CultureService, private appState: AppState, private accountService: AccountService, private httpClient: HttpClient) {
      this.httpClient.configure(config => {
          config
              .withBaseUrl(environment.backendUrl)
              .withDefaults({
                  credentials: 'same-origin',
                  headers: {
                      'Content-Type': 'application/json',
                      'Accept': 'application/json',
                      'X-Requested-With': 'Fetch'
                  }
              })
              .withInterceptor({
                  request(request) {
                      console.log(`Requesting ${request.method} ${request.url}`);
                      return request;
                  },
                  response(response) {
                      console.log(`Received ${response.status} ${response.url}`);
                      return response;
                  }
              });
      });

      this.store.registerAction('stateUpdateCultures', this.stateUpdateCultures);
      this.store.registerAction('stateUpdateSelectedCulture', this.stateUpdateSelectedCulture);

  }

    async attached(): Promise<void> {
        // get the languages from backend
        const result = await this.cultureService.getAll();
        if (result.statusCode >= 200 && result.statusCode < 300) {
            log.debug('data', result.data);
            if (result.data) {
                this.store.dispatch(this.stateUpdateCultures, result.data);
            }
        }

        if (this.appState.jwt != null){
            await this.accountService.getUser().then(
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

    activate(){

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

    { route: ['Orders', 'Orders/index'], name: 'Orders-index', moduleId:
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

    setCulture(culture: ICulture): void {
        this.store.dispatch(this.stateUpdateSelectedCulture, culture);
    }

    // take the old state, make shallow copy, update copy, return as new state
    stateUpdateCultures(state: IState, cultures: ICulture[]): IState {
        const newState = Object.assign({}, state);
        newState.cultures = cultures;
        return newState;
    }

    stateUpdateSelectedCulture(state: IState, culture: ICulture): IState {
        const newState = Object.assign({}, state);
        newState.selectedCulture = culture;
        return newState;
    }

  logoutOnClick(){
    this.isAdmin = false;
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
