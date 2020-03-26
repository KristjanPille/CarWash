using DAL.Base;

namespace Domain
{
    public class WashType : DomainEntity
    {
        public int WashTypeId { get; set; }
        
        public int WashId { get; set; }

        public string NameOfWash { get; set; } = default!;
    }
}