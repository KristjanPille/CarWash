using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class CarType : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid NameId { get; set; }
        
        public virtual string Name { get; set; } = default!;
    }
}