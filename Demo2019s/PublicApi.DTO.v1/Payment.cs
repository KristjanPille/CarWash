using System;

namespace PublicApi.DTO.v1
{
    public class Payment : PaymentEdit
    {
  
    }
    
    public class PaymentCreate
    {
        public int PaymentAmount { get; set; }
        
        public DateTime TimeOfPayment { get; set; }
    }
    
    public class PaymentEdit : PaymentCreate
    {
        public Guid Id { get; set; }
    }
    
}