using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class QuestionAnswerDummyV2 : IDomainEntityId
    {
        public Guid? CorrectAnswerId { get; set; }
        
        public Guid? QuestionId { get; set; }
        public DAL.App.DTO.Question? Question { get; set; }
        
        public string? QuizName { get; set; }
        public Guid? QuizId { get; set; }
        public int index { get; set; }
        public double score { get; set; }

        public Guid Id { get; set; }
    }
}