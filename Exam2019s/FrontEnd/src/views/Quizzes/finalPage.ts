import {autoinject, observable} from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AppState } from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";
import {NavigationInstruction, RouteConfig} from "aurelia-router";

@connectTo()
@autoinject
export class FinalPageIndex{
  private _quizzes: IQuiz[] = [];
  private _alert: IAlertData | null = null;
  @observable
  protected state!: IState;
  score = 0;
  maxScore = 0;
  quizName = "";

  constructor(private store: Store<IState>, private quizService: QuizService, private appState: AppState){
  }

  async attached() {

  }

  async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
      this.score = params.score;
      this.maxScore = params.maxScore;
      this.quizName = params.quizName;
  }
}
