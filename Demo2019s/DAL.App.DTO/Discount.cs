using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class Discount : Discount<Guid>, IDomainBaseEntity
    {
    }
    
    public class Discount<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey CheckId { get; set; }
        public Check? Check { get; set; }
        
        public TKey DiscountAmount { get; set; }
        
        public TKey WashId { get; set; }
        public Wash? Wash { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}