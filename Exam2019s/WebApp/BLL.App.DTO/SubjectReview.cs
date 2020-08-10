using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class SubjectReview : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public string NameOfSubject { get; set; } = default!;
        
        // From 1-5
        public int Rating { get; set; } = default!;
        
        public string Comment { get; set; } = default!;
    }
}