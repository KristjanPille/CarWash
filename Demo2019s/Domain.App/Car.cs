using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Car : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }
    }
}