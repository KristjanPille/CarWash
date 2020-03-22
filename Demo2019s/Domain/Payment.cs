using System;

namespace Domain
{
    public class Payment
    {
        public int PaymentId { get; set; }
        
        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
        public int CheckId { get; set; }
        public Check Check { get; set; }
        
        public int PaymentAmount { get; set; }
        public DateTime TimeOfPayment { get; set; }
    }
}