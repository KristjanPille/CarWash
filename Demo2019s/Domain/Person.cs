using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Person: DomainEntityMetadata
    {
        public int PersonId { get; set; }
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; } = default!;

        [MaxLength(36)] public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public int PersonTypeId { get; set; }
        
        [ForeignKey(nameof(PersonTypeId))] 
        public PersonType? PersonType { get; set; }

        [MaxLength(64)]
        public string Email { get; set; } = default!;
        public int PhoneNr { get; set; }
    }
}