﻿using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class QuestionAnswer : IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Answer { get; set; } = default!;

        public Guid Id { get; set; }
    }
}