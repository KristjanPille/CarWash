using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class ModelMark : ModelMark<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class ModelMark<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}