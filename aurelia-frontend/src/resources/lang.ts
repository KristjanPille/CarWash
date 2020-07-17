import { autoinject } from 'aurelia-framework';
import { AppState } from "state/app-state";

interface ILangStrings {
    name: string;
    description: string;
    language: string;
    login: string;
    logout: string;
    register: string;
    choose: string;
    createNew: string;
    from: string;
    to: string;
    edit: string;
    save: string;
    delete: string;
    backToList: string;
    duration: string;
    nameOfCampaign: string;
}

interface ILangResources {
    'en-GB': ILangStrings;
    'et-EE': ILangStrings;
    [propName: string]: ILangStrings;
}

const LangResources: ILangResources = {
    'en-GB': {
        name: 'Name',
        nameOfCampaign: 'Name',
        description: 'Description',
        language: 'Language',
        login: 'Login',
        logout: 'Logout',
        register: 'Register',
        choose: 'Choose',
        createNew: 'Create New',
        from: 'From',
        to: 'To',
        edit: 'Edit',
        save: 'Save',
        delete: 'Delete',
        backToList: 'Back to List',
        duration: 'Duration',
    },
    'et-EE': {
        name: 'Nimi',
        nameOfCampaign: 'Nimi',
        description: 'Kirjeldus',
        language: 'Keel',
        login: 'Logi sisse',
        logout: 'Logi välja',
        register: 'Registreeri',
        choose: 'Vali',
        createNew: 'Loo uus',
        from: 'Alates',
        to: 'Kuni',
        edit: 'Muuda',
        save: 'Salvesta',
        delete: 'Kustuta',
        backToList: 'Tagasi nimekirja',
        duration: 'Kestvus',
    },
}

@autoinject
export default class LangStrings {
    /*
    selected: ILangStrings;
    constructor(private appState: AppState) {
        this.selected = LangResources[this.appState.culture?.code || 'en-GB'];
    }

    setLang(lang = 'en-GB'): void {
        this.selected = LangResources[lang];
    }
    */
}
