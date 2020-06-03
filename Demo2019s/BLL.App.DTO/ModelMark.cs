using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class ModelMark : IDomainEntityId
    { 
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
    }
}