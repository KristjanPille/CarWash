using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Check : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public double AmountExcludeVat { get; set; }
        public double Vat { get; set; }
        
        public string Comment { get; set; }  = default!;
    }
}