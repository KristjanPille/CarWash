using ee.itcollege.carwash.kristjan.Domain.Base;


namespace Domain.App
{
    public class SubjectReview : DomainEntityIdMetadata
    {
        public string NameOfSubject { get; set; } = default!;
        
        // From 1-5
        public int Rating { get; set; } = default!;
        
        public string Comment { get; set; } = default!;
    }
}