using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Car : CarEdit
    {
       public string LicenceNr { get; set; } = default!;
    }
    
    public class CarCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string LicenceNr { get; set; } = default!;
    }
    
    public class CarEdit : CarCreate
    {
        public Guid Id { get; set; }
    }
    
}