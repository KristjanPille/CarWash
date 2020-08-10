using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Quiz : IDomainEntityId
    {
        [Required]
        public string NameOfQuiz { get; set; } = default!;
        
        public ICollection<Question>? Questions { get; set; }
        
        public double? AverageScore { get; set; }
        
        public Guid Id { get; set; }
    }
}