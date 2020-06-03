using System;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; }
        
        public Guid ServiceId { get; set; }
        
        public string? Comment { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}