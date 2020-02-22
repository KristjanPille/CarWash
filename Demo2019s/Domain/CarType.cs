using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CarType
    {
        public int CarTypeId { get; set; }
        [MaxLength(64)] public string Name { get; set; } = default!;
    }
}