export interface IIndexResourceStrings {
    carWash: string;
    campaign: string;
    car: string;
    service: string;
    account: string;
    order: string;
    admin: string;
    adminSection: string;
    settings: string;
    logout: string;
    register: string;
    login: string;
    newCar: string;
    yourCar: string;
    Monday: string;
    Tuesday: string;
    Wednesday: string;
    Thursday: string;
    Friday: string;
    Saturday: string;
    Sunday: string;
    Discount: string;
    edit: string;
    save: string;
    delete: string;
}
export interface IIndexResources {
    'en-GB': IIndexResourceStrings;
    'et-EE': IIndexResourceStrings;
}
export const IndexResources: IIndexResources = {
    'en-GB':{
        carWash: 'Car Wash',
        campaign: 'Campaigns',
        service: 'Services',
        car: 'Cars',
        account: 'Your account',
        order: 'Your Orders',
        admin: 'Admin panel',
        adminSection: 'Admin Section',
        settings: 'Settings',
        logout: 'Logout',
        register: 'Register',
        login: 'Login',
        newCar: 'Add new car',
        yourCar: 'Your cars',
        Monday: 'Monday',
        Tuesday: 'Tuesday',
        Wednesday: 'Wednesday',
        Thursday: 'Thursday',
        Friday: 'Friday',
        Saturday: 'Saturday',
        Sunday: 'Sunday',
        Discount: 'Discount',
        edit: 'Edit',
        save: 'Save',
        delete: 'Delete',
    },
    'et-EE': {
        carWash: 'Auto pesula',
        campaign: 'Kampaania',
        service: 'Teenused',
        car: 'Autod',
        account: 'Kasutaja',
        order: 'Sinu tellimused',
        admin: 'Haldusliides',
        adminSection: 'Haldusliidese sektsioon',
        settings: 'sätted',
        logout: 'Logi välja',
        register: 'Registreeri',
        login: 'Logi sisse',
        newCar: 'Lisa uus auto',
        yourCar: 'Sinu autod',
        Monday: 'esmaspäev',
        Tuesday: 'teisipäev',
        Wednesday: 'kolmapäev',
        Thursday: 'neljapäev',
        Friday: 'reede',
        Saturday: 'laupäev',
        Sunday: 'pühapäev',
        Discount: 'Allahindlus',
        edit: 'Muuda',
        save: 'Salvesta',
        delete: 'Kustuta',
    },
}
