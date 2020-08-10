using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Domain.Base;


namespace Domain.App
{
    public class Quiz : DomainEntityIdMetadata
    {
        [Required]
        public string NameOfQuiz { get; set; } = default!;
    }
}