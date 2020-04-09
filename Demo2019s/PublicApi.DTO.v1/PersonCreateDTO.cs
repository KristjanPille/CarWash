using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonCreateDTO
    {
        [MinLength(1)] [MaxLength(64)] public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] public string LastName { get; set; } = default!;
    }
}