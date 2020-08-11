import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {NavigationInstruction, RouteConfig, Router} from "aurelia-router";
import {connectTo} from "aurelia-store";
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";
import {QuestionService} from "../../service/question-service";
import {IQuestion} from "../../domain/IQuestion";
import {IQuestionAnswer} from "../../domain/IQuestionAnswer";
import {QuestionAnswerService} from "../../service/questionAnswer-service";
import {ScoreService} from "../../service/score-service";

@connectTo()
@autoinject
export class QuizIndex {
  private _alert: IAlertData | null = null;

  private _questions: IQuestion[] = [];
  private _answers: IQuestionAnswer[] = [];
  private quiz?: IQuiz;
  currentQuestion: IQuestion;
  answered = 0;
  index = 0;
  selectedAnswer = null;
  maxScore = 0;
  score = 0;

  constructor(private appState: AppState, private quizService: QuizService, private router: Router, private questionService: QuestionService, private questionAnswerService: QuestionAnswerService, private scoreService: ScoreService){

  }
  async attached() {

  }

  async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    if (params.id && typeof (params.id) == 'string') {
      await this.quizService.getQuiz(params.id).then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
            this.quiz = response.data!;

            this.questionService.getQuizSpecificQuestions(this.quiz.id).then(
              response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                  this._alert = null;
                  this._questions = response.data!;
                  this.currentQuestion = this._questions[0];
                  this.maxScore = this._questions.length;

                  this.questionAnswerService.getQuestionSpecificAnswers(this.currentQuestion.id).then(
                    response => {
                      if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._answers = response.data!;
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

  moveToNextQuestion(){
    if (this.selectedAnswer.id == this._questions[this.index].correctAnswerId) {
      this.score += 1;
    }
    if(this._questions[this.index + 1] != null || this._questions[this.index + 1] != undefined) {
      this.index += 1;
      this.currentQuestion = this._questions[this.index];
      this.questionAnswerService.getQuestionSpecificAnswers( this._questions[this.index].id).then(
        response => {
          if (response.statusCode >= 200 && response.statusCode < 300) {
            this._alert = null;
            this._answers = response.data!;
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
    else {
      this.scoreService
        .createScore( { quizId: this.quiz.id, quizScore: this.score })
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
      this.router.navigateToRoute('quizzes-finalPage', { score: this.score, maxScore: this.maxScore, quizName: this.quiz.nameOfQuiz });
    }
  }
}
