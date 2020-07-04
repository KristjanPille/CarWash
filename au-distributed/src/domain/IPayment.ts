export interface IPayment {
    id: string;
    PaymentMethodId: string;
    CheckId: string;
    CarId: string;
    ServiceId: string;
    PaymentAmount: number;
    TimeOfPayment: string;
    PayPalEmail: string;
    CreditCardNumber: string;
    ExpMonth: string;
    ExpYear: string;
    CVV: number;
}
