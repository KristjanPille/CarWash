using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class QuestionAnswerDummyV2 : IDomainEntityId
    {
        [MaxLength(256)]
        public DAL.App.DTO.QuestionAnswer? Answer1 { get; set; }
        public bool? Answer1Checked { get; set; }
        
        [MaxLength(256)]
        public DAL.App.DTO.QuestionAnswer? Answer2 { get; set; }
        public bool? Answer2Checked { get; set; }
        
        [MaxLength(256)]
        public DAL.App.DTO.QuestionAnswer? Answer3 { get; set; }
        public bool? Answer3Checked { get; set; }
   
        [MaxLength(256)]
        public DAL.App.DTO.QuestionAnswer? Answer4 { get; set; }
        public bool? Answer4Checked { get; set; }
        
        [MaxLength(256)]
        public DAL.App.DTO.QuestionAnswer? Answer5 { get; set; }
        public bool? Answer5Checked { get; set; }

        public Guid? QuestionId { get; set; }
        
        public Question? Question { get; set; }
        
        public Guid Id { get; set; }
    }
}