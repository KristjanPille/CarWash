using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class SubjectReview: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string NameOfSubject { get; set; } = default!;
        
        // From 1-5
        public int Rating { get; set; } = default!;
        
        public string Comment { get; set; } = default!;
    }

}