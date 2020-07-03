using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }
        
        /* 1-3
         1=> Small hatchback
         2=> Mid Sized car
         3=> Bigger suv
        */
    }
}