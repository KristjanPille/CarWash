using System;
using Domain.App.Identity;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }
    }
}