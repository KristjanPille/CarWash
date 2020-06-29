using System;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateAndTime { get; set; }
        
        public Guid ServiceId { get; set; }

        //Get the name of service from bll layer
        public string NameOfTheService { get; set; } = default!;
        
        //Get the price of service from bll layer
        //Takes into consideration the campaign
        public double PriceOfService { get; set; } = default!;

        public Guid AppUserId { get; set; }
    }
}