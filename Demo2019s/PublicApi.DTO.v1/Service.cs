﻿using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Service : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        public string NameOfService { get; set; } = default!;

        public double PriceOfService { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
    }
}