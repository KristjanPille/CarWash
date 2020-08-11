import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {Router} from "aurelia-router";
import {connectTo} from "aurelia-store";
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";
import {ScoreService} from "../../service/score-service";
import {IScore} from "../../domain/IScore";

@connectTo()
@autoinject
export class AdminIndex{
   private _alert: IAlertData | null = null;
   private _quizzes: IQuiz[] = [];
   private _scores: IScore[] = [];

    constructor(private appState: AppState, private scoreService: ScoreService, private router: Router){

    }

    async attached() {
        await this.scoreService.getAllUserScore().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._scores = response.data!;
                  console.log(this._scores)
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
