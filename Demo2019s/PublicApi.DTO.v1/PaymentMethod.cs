using System;

namespace PublicApi.DTO.v1
{
    public class PaymentMethod : PaymentMethodEdit
    {
  
    }
    
    public class PaymentMethodCreate
    {
        public string PaymentMethodName { get; set; } = default!;
    }
    
    public class PaymentMethodEdit : PaymentMethodCreate
    {
        public Guid Id { get; set; }
    }
    
}