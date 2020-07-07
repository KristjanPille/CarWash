import { IModelMark } from "./IModelMark";
export interface ICar {
    id: string;

    carSize: number

    modelMarkId: string;
    modelMark: IModelMark;
}
