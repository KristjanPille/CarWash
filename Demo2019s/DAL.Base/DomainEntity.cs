using System;
using DAL.Contracts.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : IDomainEntity
    {
        public int Id { get; set; }
    }
}