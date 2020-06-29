using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Order : DomainEntityIdMetadataUser<AppUser>
    {
        public DateTime DateAndTime { get; set; } = default!;
        
        public Guid ServiceId { get; set; } = default!;
        public Service? Service { get; set; }
        
        public string? Comment { get; set; }
    }
}