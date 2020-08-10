import {autoinject, observable} from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import {IState} from "../../state/state";
import {connectTo, Store} from "aurelia-store";
import { ISubjectReview } from "../../domain/IQuestion";
import { QuizService } from "../../service/quiz-service";

@connectTo()
@autoinject
export class SubjectReviewsIndex{
   private _subjectReviews: ISubjectReview[] = [];
   private _alert: IAlertData | null = null;
    private subjectReview = "";
    @observable
    protected state!: IState;

    constructor(private store: Store<IState>, private subjectReviewService: QuizService, private appState: AppState){
    }

    async attached() {
        await this.subjectReviewService.getAll().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._subjectReviews = response.data!;
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
