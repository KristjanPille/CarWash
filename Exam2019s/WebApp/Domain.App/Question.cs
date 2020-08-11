using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Question : DomainEntityIdMetadata
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string NameOfQuestion { get; set; } = default!;
        
        public Guid? CorrectAnswerId { get; set; }

        public Quiz? Quiz { get; set; }
        public Guid? QuizId { get; set; }
    }
}