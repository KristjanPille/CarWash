import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {QuizService} from "../../service/quiz-service";
import {IQuiz} from "../../domain/IQuiz";
import {QuestionService} from "../../service/question-service";
import {QuestionAnswerService} from "../../service/questionAnswer-service";
import {IQuestion} from "../../domain/IQuestion";

@autoinject
export class QuestionsCreate {
  private _alert: IAlertData | null = null;

  _isQuestionSubmitted = false;
  _nameOfQuestion = "";
  _quizId = "";
  selectedQuiz: IQuiz;
  selectedQuestionId = "";
  _questionAnswer = "";
  _questionAnswerId = "";
  _answer = "";
  _checked = false;
  selectedQuestion: IQuestion;


  private _quizzes: IQuiz[] = [];

  constructor(private quizService: QuizService, private router: Router, private questionService: QuestionService, private questionAnswerService: QuestionAnswerService) {

  }

  async attached() {
  }

  activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    if (params.id && typeof (params.id) == 'string') {
      this._quizId = params.id
    }
  }

  reload(){
    this._isQuestionSubmitted = false;
    this._nameOfQuestion = "";
  }

  async getQuestion(){
    await this.questionService.getQuestion(this.selectedQuestionId).then(
      response => {
        if (response.statusCode >= 200 && response.statusCode < 300) {
          this._alert = null;
          this.selectedQuestion = response.data!;
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

  onSubmitQuestion(event: Event) {
    this.questionService
      .createQuestion({ nameOfQuestion: this._nameOfQuestion, quizId: this._quizId })
      .then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
            this.selectedQuestionId = response.data;
            this._isQuestionSubmitted = true;
            this.getQuestion();
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

  onSubmitAnswer(event: Event) {
    this.questionAnswerService
      .createQuestionAnswer({ answer: this._answer, questionId: this.selectedQuestionId })
      .then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
            this._questionAnswerId = response.data;

            if (this._checked){
              console.log(this._questionAnswerId)
              this.selectedQuestion.correctAnswerId = this._questionAnswerId
              console.log(this.selectedQuestion)
              this.questionService
                .updateQuestion(this.selectedQuestion!)
                .then(
                  response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                      this._alert = null;
                      this._isQuestionSubmitted = true;
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
    event.preventDefault();
  }

}
