using System;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }

        public double AmountToPay { get; set; }
        
        public DateTime? TimeOfPayment { get; set; }
    }
}