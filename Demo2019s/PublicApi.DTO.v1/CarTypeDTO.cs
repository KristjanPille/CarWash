using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CarTypeDTO
    {
        public Guid Id { get; set; }
        public int CarTypeId { get; set; }
        [MaxLength(64)] 
        public string Name { get; set; } = default!;
    }
}