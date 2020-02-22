using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int personID { get; set; }
        [MaxLength(64)]
        public string name { get; set; }
        
        public int personTypeID { get; set; }
        public PersonType PersonType { get; set; }

        [MaxLength(64)]
        public string email { get; set; }
        public int phoneNr { get; set; }
    }
}