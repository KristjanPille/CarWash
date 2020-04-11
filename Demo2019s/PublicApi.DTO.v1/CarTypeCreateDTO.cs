using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CarTypeCreateDTO
    {
        public Guid Id { get; set; }

        [MaxLength(64)] 
        public string Name { get; set; } = default!;
    }
}