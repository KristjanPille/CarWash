using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CarType : DomainEntity
    {
        public int CarTypeId { get; set; }
        [MaxLength(64)] public string Name { get; set; } = default!;
    }
}