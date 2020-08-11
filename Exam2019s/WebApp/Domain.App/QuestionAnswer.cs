using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class QuestionAnswer : DomainEntityIdMetadata
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Answer { get; set; } = default!;
        
        public Question? Question { get; set; }
        public Guid? QuestionId { get; set; }
    }
}