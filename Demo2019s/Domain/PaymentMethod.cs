using DAL.Base;

namespace Domain
{
    public class PaymentMethod : DomainEntity
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }
}