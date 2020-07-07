using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; } = default!;
        
        public Guid ServiceId { get; set; } = default!;
        
        public Guid CarId { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}