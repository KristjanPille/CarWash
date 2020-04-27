using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Check : CheckEdit
    {
    }
    
    public class CheckCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string LicenceNr { get; set; } = default!;
        
        public int AmountExcludeVat { get; set; }
        public int AmountWithVat { get; set; }
        public int Vat { get; set; }
        
        public string Comment { get; set; }  = default!;
    }
    
    public class CheckEdit : CheckCreate
    {
        public Guid Id { get; set; }
    }
    
}