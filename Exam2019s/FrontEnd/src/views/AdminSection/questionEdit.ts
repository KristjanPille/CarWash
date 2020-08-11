import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";
import {IQuestion} from "../../domain/IQuestion";
import {QuestionService} from "../../service/question-service";
import {IQuestionAnswer} from "../../domain/IQuestionAnswer";
import {QuestionAnswerService} from "../../service/questionAnswer-service";

@autoinject
export class QuestionEdit {
  private _alert: IAlertData | null = null;
  private _question?: IQuestion;
  private answers: IQuestionAnswer[] = [];
  selectedAnswerId = null

  constructor(private quizService: QuizService, private router: Router, private questionService: QuestionService, private questionAnswerService: QuestionAnswerService) {
  }

  attached() {
  }

  activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    if (params.id && typeof (params.id) == 'string') {
      this.questionService.getQuestion(params.id).then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
            this._question = response.data!;
            this.selectedAnswerId = this._question.correctAnswerId;
            this.questionAnswerService.getQuestionSpecificAnswers(this._question.id).then(
              response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                  this._alert = null;
                  this.answers = response.data!;
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
    this._question.correctAnswerId = this.selectedAnswerId;
    this.questionService
      .updateQuestion(this._question!)
      .then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
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
