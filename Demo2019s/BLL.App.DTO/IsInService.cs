using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class IsInService : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid CarId { get; set; } = default!;

        public Guid ServiceId { get; set; } = default!;
        
        public DateTime From { get; set; }
        
        public DateTime To { get; set; }
    }
}