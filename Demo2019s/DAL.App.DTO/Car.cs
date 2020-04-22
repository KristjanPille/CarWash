using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Car : Car<Guid>, IDomainBaseEntity
        {
        }
    
        public class Car<TKey> : IDomainBaseEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            public TKey Id { get; set; } = default!;
        
            public TKey ModelMarkId { get; set; }
            public ModelMark? ModelMark { get; set; }
            
            public TKey CarTypeId { get; set; }
            public CarType? CarType { get; set; }
            
            public virtual string LiceneNr { get; set; } = default!;
            
            public virtual ICollection<PersonCar>? Persons { get; set; }
            
            public TKey AppUserId { get; set; } = default!;
            public AppUser<TKey>? AppUser { get; set; }
        }
}