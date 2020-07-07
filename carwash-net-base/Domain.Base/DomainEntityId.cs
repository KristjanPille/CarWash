using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;


namespace ee.itcollege.carwash.kristjan.Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
        
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}