using DAL.Base;

namespace Domain
{
    public class PaymentMethod : DomainEntity
    {
        public int PaymentMethodId { get; set; }
        public int PaymentMethodName { get; set; }
    }
}