using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using Newtonsoft.Json;

namespace PublicApi.DTO.v1
{
    public class Score : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Required]
        public double QuizScore { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default;
        
        public Guid? AppUserId { get; set; }
    }
}