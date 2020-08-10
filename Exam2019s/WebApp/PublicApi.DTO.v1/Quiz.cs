using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Quiz: IDomainEntityId
    {
        [Required]
        public string NameOfQuiz { get; set; } = default!;
        
        public ICollection<Question>? Questions { get; set; }
        
        public double? Score { get; set; }
        
        public Guid Id { get; set; }
    }
}