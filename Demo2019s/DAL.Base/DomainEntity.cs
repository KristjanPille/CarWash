using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public class DomainEntity : IDomainEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}