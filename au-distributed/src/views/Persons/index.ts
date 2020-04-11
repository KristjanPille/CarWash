import { IPerson } from './../../domain/IPerson';
import { AlertType } from './../../types/AlertType';
import { autoinject } from 'aurelia-framework';
import { PersonService } from 'service/person-service';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class PersonsIndex {
    private _persons: IPerson[] = [];

    private _alert: IAlertData | null = null;

    constructor(private personService: PersonService) {

    }

    attached() {
        this.personService.getPersons().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._persons = response.data!;
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
