using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class QuestionAnswer : IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Answer { get; set; } = default!;
        
        public Question? Question { get; set; }
        
        public Guid? QuestionId { get; set; }
        public Guid Id { get; set; }
    }
}