import { autoinject, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration} from 'aurelia-router';
import { AppState } from 'state/app-state';
import {AlertType} from "./types/AlertType";
import {IAlertData} from "./types/IAlertData";
import '../static/site.css';
import {IState} from "./state/state";
import { Store, connectTo } from "aurelia-store";
import * as environment from '../config/environment.json';
import {HttpClient} from "aurelia-fetch-client";
import {IAccount} from "./domain/IAccount";
import {AccountService} from "./service/account-service";

@connectTo()
@autoinject
export class App {
  private _alert: IAlertData | null = null;
  router?: Router;
  private userId: string = "";
  protected state!: IState;
  private _account?: IAccount;

  constructor(private store: Store<IState>, private appState: AppState, private accountService: AccountService, private httpClient: HttpClient) {
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

  }

  async attached(): Promise<void> {

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

    config.title = 'Aurelia exam';

    config.map([

      { route: ['Home/index'], name: 'Home', moduleId:
          PLATFORM.moduleName('views/Home/index'), nav: false, title: 'Home' },

      { route: ['account/login'], name: 'account-login', moduleId:
          PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },

      { route: ['account/register'], name: 'account-register', moduleId:
          PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

      { route: ['Quizzes', 'Campaigns/index'], name: 'Quizzes-index', moduleId:
          PLATFORM.moduleName('views/Quizzes/index'), nav: true, title: 'Campaigns' },

      { route: ['Quizzes/edit/:id?'], name: 'Quizzes-edit', moduleId:
          PLATFORM.moduleName('views/Quizzes/edit'), nav: false, title: 'Campaigns Edit' },

      { route: ['Quizzes/create'], name: 'Quizzes-create', moduleId:
          PLATFORM.moduleName('views/Quizzes/create'), nav: false, title: 'Campaigns Create' },

      { route: ['Quizzes/delete'], name: 'Quizzes-delete', moduleId:
          PLATFORM.moduleName('views/Quizzes/delete'), nav: false, title: 'Campaigns Delete' },


      { route: ['subjectReviews', 'SubjectReviews/index'], name: 'subjectReviews-index', moduleId:
          PLATFORM.moduleName('views/subjectReviews/index'), nav: true, title: 'SubjectReviews' },

      { route: ['subjectReviews/edit/:id?'], name: 'subjectReviews-edit', moduleId:
          PLATFORM.moduleName('views/subjectReviews/edit'), nav: false, title: 'SubjectReviews Edit' },

      { route: ['subjectReviews/create'], name: 'subjectReviews-create', moduleId:
          PLATFORM.moduleName('views/subjectReviews/create'), nav: false, title: 'SubjectReviews Create' },

      { route: ['subjectReviews/delete'], name: 'subjectReviews-delete', moduleId:
          PLATFORM.moduleName('views/subjectReviews/delete'), nav: false, title: 'SubjectReviews Delete' },



      { route: ['Admin-Index', 'AdminSection/index'], name: 'Admin-Index', moduleId:
          PLATFORM.moduleName('views/AdminSection/index'), nav: true, title: 'Admin Index' },





    ]);
    config.mapUnknownRoutes('views/Home/index');
  }

  logoutOnClick(){
    this.appState.email = null;
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
