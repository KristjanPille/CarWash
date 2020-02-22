using System;

namespace Domain
{
    public class Check
    {
        public int CheckId { get; set; }
        
        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        public int AmountExcludeVat { get; set; }
        public int AmountWithVat { get; set; }
        public int Vat { get; set; }
        public string Comment { get; set; }  = default!;
    }
}