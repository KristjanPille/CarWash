import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {connectTo} from "aurelia-store";
import {ISubjectReview} from "../../domain/IQuestion";
import {QuizService} from "../../service/quiz-service";

@connectTo()
@autoinject
export class SubjectReviewEdit {
    private _alert: IAlertData | null = null;

    _subjectReview: ISubjectReview;
    _nameOfSubject = "";
    _rating = 0;
    _comment = "";

    constructor(private subjectReviewService: QuizService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.subjectReviewService.getSubjectReview(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._subjectReview = response.data!;
                        this._nameOfSubject = this._subjectReview.nameOfSubject;
                        this._rating = this._subjectReview.rating;
                        this._comment = this._subjectReview.comment;
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
      this._subjectReview.nameOfSubject = this._nameOfSubject
      this._subjectReview.rating = this._rating
      this._subjectReview.comment = this._comment
        this.subjectReviewService
            .updateSubjectReview(this._subjectReview!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('subjectReview-index', {});
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
