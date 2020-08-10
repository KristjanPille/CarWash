using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using DAL.App.DTO.Identity;
using System.Text.Json.Serialization;


namespace DAL.App.DTO
{
    public class Score : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Required]
        public double QuizScore { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default;
        
        public Guid AppUserId { get; set; }
        
        [JsonIgnore]
        public AppUser? AppUser { get; set; }

    }
}