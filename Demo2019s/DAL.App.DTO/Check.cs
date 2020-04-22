using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Check : Check<Guid>, IDomainBaseEntity
    {
    }
    
    public class Check<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey PersonId { get; set; }
        public Person? Person { get; set; }

        public TKey WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public int AmountExcludeVat { get; set; }
        public int AmountWithVat { get; set; }
        public int Vat { get; set; }

        public string Comment { get; set; }  = default!;

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}