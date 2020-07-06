using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain;
using Domain.App.Identity;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Check : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public Guid CarId { get; set; }
        public Guid AppUserId { get; set; }

        public DateTime DateTimeCheck { get; set; }
        
        public double PaymentAmount { get; set; }
    }

}