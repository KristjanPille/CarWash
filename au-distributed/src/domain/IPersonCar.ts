import { IPerson } from "./IPerson";
import { ICar } from "./ICar";

export interface IPersonCar {
    id: string;
    
    carId: string;
    car: ICar;

    personId: string;
    person: IPerson;
}
