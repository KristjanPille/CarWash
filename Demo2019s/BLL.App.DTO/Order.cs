using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public string Comment { get; set; } = default!;
        public DateTime DateAndTime { get; set; }
    }
}