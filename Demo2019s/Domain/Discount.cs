using DAL.Base;

namespace Domain
{
    public class Discount : DomainEntity
    {
        public int DiscountId { get; set; }
        
        public int? CheckId { get; set; }
        public Check? Check { get; set; }
        
        public int? WashId { get; set; }
        public Wash? Wash { get; set; }
    }
}