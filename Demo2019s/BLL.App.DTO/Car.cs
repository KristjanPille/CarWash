using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Car : Car<Guid>, IDomainBaseEntity
    {
    }
    
    public class Car<TKey> : IDomainBaseEntity<TKey>
    where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public virtual string LiceneNr { get; set; } = default!;
        

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public virtual ICollection<PersonCar>? Person { get; set; }        
    }
    
}