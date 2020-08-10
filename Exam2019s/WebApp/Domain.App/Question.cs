using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Question : DomainEntityIdMetadata
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string NameOfQuestion { get; set; } = default!;

        [Required] 
        public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; }

       
        public QuestionAnswer? CorrectAnswer { get; set; }
    }
}