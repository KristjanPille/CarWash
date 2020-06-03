using System;
using Domain.Base;

namespace Domain.App
{
    public class IsInService : DomainEntityIdMetadata
    {
        public Guid CarId { get; set; } = default!;
        public Car? Car { get; set; }

        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
