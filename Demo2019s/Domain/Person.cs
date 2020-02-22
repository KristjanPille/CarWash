using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Person: DomainEntityMetadata
    {
        public int PersonId { get; set; }
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; } = default!;
        
        public int PersonTypeId { get; set; }
        public PersonType? PersonType { get; set; }

        [MaxLength(64)]
        public string Email { get; set; } = default!;
        public int PhoneNr { get; set; }
    }
}