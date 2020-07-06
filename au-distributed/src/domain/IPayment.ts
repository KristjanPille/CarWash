export interface IPayment {
    PaymentMethodId: string;
    CarId: string;
    ServiceId: string;
    PaymentAmount: number;
    TimeOfPayment: string;
    PayPalEmail: string;
    CreditCardNumber: string;
    ExpMonth: string;
    ExpYear: string;
    CVV: number;
    from: string
    to: string
}
