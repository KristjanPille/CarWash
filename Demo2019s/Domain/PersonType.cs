using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PersonType
    {
        public int PersonTypeId { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = default!;
    }
}