using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class IsInService : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        
        public TimeSpan From { get; set; }
        
        public TimeSpan To { get; set; }
    }
}