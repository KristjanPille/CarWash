using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Score : DomainEntityIdMetadataUser<AppUser>
    {
        [Required]
        public double QuizScore { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default;
    }
}