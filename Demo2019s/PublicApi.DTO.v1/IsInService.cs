using System;
using System.ComponentModel.DataAnnotations;
using carwash.kristjan.Contracts.Domain;
using Domain.App.Identity;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class IsInService : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; } = default!;

        public Guid ServiceId { get; set; } = default!;

        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
    }
}