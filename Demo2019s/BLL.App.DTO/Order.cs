using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }

        public DateTime DateAndTime { get; set; }
        
        public Guid ServiceId { get; set; } = default!;
        public Service? Service { get; set; }
    }
}