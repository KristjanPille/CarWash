using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class QuizDummy : IDomainEntityId
    {
        [Required]
        public string NameOfQuiz { get; set; } = default!;

        public Guid Id { get; set; }
        public IEnumerable<DAL.App.DTO.Question> Questions { get; set; }
        public ICollection<Domain.App.QuestionAnswer>? QuestionAnswers { get; set; }

        public double AverageScore { get; set; }
        public double MaxScore { get; set; }
    }
}