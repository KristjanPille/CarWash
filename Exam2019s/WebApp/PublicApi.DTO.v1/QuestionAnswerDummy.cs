using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class QuestionAnswerDummy : IDomainEntityId
    {
        [MaxLength(256)]
        public string? Answer1 { get; set; }
        public bool? Answer1Checked { get; set; }
        
        [MaxLength(256)]
        public string? Answer2 { get; set; }
        public bool? Answer2Checked { get; set; }
        
        [MaxLength(256)]
        public string? Answer3 { get; set; }
        public bool? Answer3Checked { get; set; }
   
        [MaxLength(256)]
        public string? Answer4 { get; set; }
        public bool? Answer4Checked { get; set; }
        
        [MaxLength(256)]
        public string? Answer5 { get; set; }
        public bool? Answer5Checked { get; set; }

        public Guid? QuestionId { get; set; }
        
        public Question? Question { get; set; }
        
        
        public Guid Id { get; set; }
    }
}