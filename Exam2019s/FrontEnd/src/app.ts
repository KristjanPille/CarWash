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

      { route: ['account/index'], name: 'account-index', moduleId:
          PLATFORM.moduleName('views/account/index'), nav: false, title: 'index' },

      { route: ['account/register'], name: 'account-register', moduleId:
          PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

      { route: ['Quizzes', 'Quizzes/index'], name: 'quizzes-index', moduleId:
          PLATFORM.moduleName('views/Quizzes/index'), nav: true, title: 'Quizzes' },

      { route: ['Quiz', 'Quizzes/quiz/:id?'], name: 'quizzes-quiz', moduleId:
          PLATFORM.moduleName('views/Quizzes/quiz'), nav: true, title: 'Quiz' },

      { route: ['finalPage', 'Quizzes/finalPage'], name: 'quizzes-finalPage', moduleId:
          PLATFORM.moduleName('views/Quizzes/finalPage'), nav: false, title: 'finalPage' },

      { route: ['Admin-Index', 'AdminSection/index'], name: 'Admin-Index', moduleId:
          PLATFORM.moduleName('views/AdminSection/index'), nav: true, title: 'Admin Index' },

      { route: ['AdminSection/quizEdit/:id?'], name: 'quiz-edit', moduleId:
          PLATFORM.moduleName('views/AdminSection/quizEdit'), nav: false, title: 'Quizzes Edit' },

      { route: ['AdminSection/quizCreate'], name: 'quiz-create', moduleId:
          PLATFORM.moduleName('views/AdminSection/quizCreate'), nav: false, title: 'Quizzes Create' },

      { route: ['AdminSection/questionCreate'], name: 'question-create', moduleId:
          PLATFORM.moduleName('views/AdminSection/questionCreate'), nav: false, title: 'Questions Create' },


      { route: ['AdminSection/questionEdit/:id?'], name: 'question-edit', moduleId:
          PLATFORM.moduleName('views/AdminSection/questionEdit'), nav: false, title: 'Questiont Edit' },

    ]);
    config.mapUnknownRoutes('views/Home/index');
  }

  logoutOnClick(){
    this.appState.email = null;
    this.appState.jwt = null;
    this.router!.navigateToRoute('account-login');
  }
}
