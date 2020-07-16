export interface ILayoutResourceStrings {
    language: string;
}
export interface ILayoutResources {
    'en-GB': ILayoutResourceStrings;
    'et-EE': ILayoutResourceStrings;
}
export const LayoutResources: ILayoutResources = {
    'en-GB':{
        language: 'Language',
    },
    'et-EE': {
        language: 'Keel',
    },
}
