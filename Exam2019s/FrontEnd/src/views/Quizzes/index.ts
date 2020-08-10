import {autoinject, observable} from 'aurelia-framework';

import {IAlertData} from 'types/IAlertData';
import {AlertType} from 'types/AlertType';
import {AppState} from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";
import {QuizService} from "../../service/quiz-service";
import {ScoreService} from "../../service/score-service";
import {IQuizDummy} from "../../domain/IQuizDummy";
import {IQuiz} from "../../domain/IQuiz";
import {QuestionService} from "../../service/question-service";

@connectTo()
@autoinject
export class QuizzesIndex{
   private _quizzesDummy: IQuizDummy[] = [];
   private _indexes: any;
   private weekdays: any;
   private _alert: IAlertData | null = null;
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private quizService: QuizService, private appState: AppState, private scoreService: ScoreService, private questionService: QuestionService){
    }

    attached() {
      let score = 0;
        this.quizService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizzesDummy = response.data!;
                    for (let i = 0; i < this._quizzesDummy.length; i++) {
                      this.scoreService.getQuizScore(this._quizzesDummy[i].id).then(
                        response => {
                          if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            score = response.data;
                            score = Number((Math.round(score * 100)/100).toFixed(2));
                            this._quizzesDummy[i].score = score;
                            this.questionService.getQuizSpecificQuestions(this._quizzesDummy[i].id).then(
                              response => {
                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                  this._alert = null;
                                  this._quizzesDummy[i].maxScore = response.data!.length;

                                } else {
                                  // show error message
                                  this._alert = {
                                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                                    type: AlertType.Danger,
                                    dismissable: true,
                                  }
                                }
                              }
                            );
                          } else {
                            // show error message
                            this._alert = {
                              message: response.statusCode.toString() + ' - ' + response.errorMessage,
                              type: AlertType.Danger,
                              dismissable: true,
                            }
                          }
                        }
                      );
                    }
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

    activate(){

    }


}
