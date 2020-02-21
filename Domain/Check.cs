using System;

namespace Domain
{
    public class Check
    {
        public int checkID { get; set; }
        
        public int personID { get; set; }
        public Person Person { get; set; }

        public int washID { get; set; }
        public Wash Wash { get; set; }
        
        public DateTime dateTimeCheck { get; set; }
        public int amountExcludeVat { get; set; }
        public int amountWithVat { get; set; }
        public int vat { get; set; }
        public string comment { get; set; }
    }
}