using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    
    public class PersonCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] 
        public string LastName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] 
        public string Email { get; set; } 
        [MinLength(1)] [MaxLength(32)] 
        public int PhoneNr { get; set; }
    }
    
    public class PersonEdit: PersonCreate
    {
        public Guid Id { get; set; }
    }
    
    public class Person : PersonEdit
    {
        [MinLength(1)] [MaxLength(64)] 
        public string Email { get; set; }
        [MinLength(1)] [MaxLength(64)] 
        public int PhoneNr { get; set; }
    }
}