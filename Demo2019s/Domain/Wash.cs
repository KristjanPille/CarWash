using System.Collections.Generic;
using DAL.Base;

namespace Domain
{
    public class Wash : DomainEntity
    {
        public int WashId { get; set; }
        
        public int CheckId { get; set; }
        public ICollection<Check>? Check { get; set; }
        
        public int WashTypeId { get; set; }
        public WashType? WashType { get; set; }
        
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        //nameOfWashType implies to exterior or interior wash
        public string NameOfWashType { get; set; } = default!;
    }
}