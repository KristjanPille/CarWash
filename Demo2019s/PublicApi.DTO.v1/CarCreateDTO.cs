using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CarCreateDTO
    {
        public Guid Id { get; set; }
        
        public int CarTypeId { get; set; }
        
        [MinLength(1)] [MaxLength(64)] 
        public string LicenceNr { get; set; }
    }
}