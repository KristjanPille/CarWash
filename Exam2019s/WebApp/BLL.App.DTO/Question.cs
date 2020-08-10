﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Question : IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string NameOfQuestion { get; set; } = default!;
        
        [Required]
        public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
        
        public QuestionAnswer? CorrectAnswer { get; set; }

        public Guid Id { get; set; }
    }
}