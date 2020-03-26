using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PersonType : DomainEntity
    {
        public int PersonTypeId { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = default!;
    }
}