using System;

namespace Domain
{
    public class Payment
    {
        public int paymentID { get; set; }
        
        public int personID { get; set; }
        public Person Person { get; set; }
        
        public int paymentMethodID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
        public int checkID { get; set; }
        public Check Check { get; set; }
        
        public int paymentAmount { get; set; }
        public DateTime timeOfPayment { get; set; }
    }
}