using System;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Discount: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CheckId { get; set; }
        public Check? Check { get; set; }
        
        public double DiscountAmount { get; set; }
        
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }


    }
}