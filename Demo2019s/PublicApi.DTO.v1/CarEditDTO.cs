using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CarEditDTO
    {
        public Guid Id { get; set; }
        
        public int CarTypeId { get; set; }
        
        [MinLength(1)] [MaxLength(64)] 
        public int LicenceNr { get; set; }
    }
}