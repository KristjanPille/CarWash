using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Check : Check<Guid>, IDomainBaseEntity
    {
    }
    
    public class Check<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public virtual string NameOfCheck { get; set; } = default!;
        
        public int AmountExcludeVat { get; set; }
        
        public int AmountWithVat { get; set; }
        
        public int Vat { get; set; }
        
        public string Comment { get; set; }  = default!;
        

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
    
}