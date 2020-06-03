using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Domain.App.Identity;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class IsInService : IDomainEntityId
    {
        public Guid CarId { get; set; } = default!;
        public Guid Id { get; set; }
    }
}