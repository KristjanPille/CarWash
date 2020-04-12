using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class WashCreateDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(64)] 
        public string NameOfWashType { get; set; } = default!;
    }
}