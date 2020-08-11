import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";
import {IQuestion} from "../../domain/IQuestion";
import {QuestionService} from "../../service/question-service";

@autoinject
export class QuizzesEdit {
    private _alert: IAlertData | null = null;
    private _quiz?: IQuiz;
  private _questions: IQuestion[] = [];

    constructor(private quizService: QuizService, private router: Router, private questionService: QuestionService) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.quizService.getQuiz(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._quiz = response.data!;

                      this.questionService.getQuizSpecificQuestions(this._quiz.id).then(
                        response => {
                          if (response.statusCode >= 200 && response.statusCode < 300) {
                            this._alert = null;
                            this._questions = response.data;

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
    }

    onSubmit(event: Event) {
        this.quizService
            .updateQuiz(this._quiz!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        //this.router.navigateToRoute('Admin-Index', {});
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

        event.preventDefault();
    }
}
