using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Question : IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string NameOfQuestion { get; set; } = default!;
        
        public Guid? CorrectAnswerId { get; set; }

        public Quiz? Quiz { get; set; }
        public Guid? QuizId { get; set; }
        
        public Guid Id { get; set; }
    }
}