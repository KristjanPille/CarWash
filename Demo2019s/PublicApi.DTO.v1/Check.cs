using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Domain.App.Identity;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Check : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public double AmountExcludeVat { get; set; }
        
        public double Vat { get; set; }
        public Guid AppUserId { get; set; }
    }

}