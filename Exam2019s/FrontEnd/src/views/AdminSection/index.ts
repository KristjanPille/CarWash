import { autoinject } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {Router} from "aurelia-router";
import {connectTo} from "aurelia-store";
import {IQuiz} from "../../domain/IQuiz";
import {QuizService} from "../../service/quiz-service";

@connectTo()
@autoinject
export class AdminIndex{
   private _alert: IAlertData | null = null;
   private _quizzes: IQuiz[] = [];

    constructor(private appState: AppState, private quizService: QuizService, private router: Router){

    }

    async attached() {
        await this.quizService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizzes = response.data!;
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


    async deleteQuiz(quizId: string){
        await this.quizService
            .deleteQuiz(quizId)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('Admin-Index', {});
                    }if (response.statusCode == 500){
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + "There are active services dependant on this Quiz, first remove campaign from services that are using it",
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                    else {
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
