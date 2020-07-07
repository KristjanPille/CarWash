using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; } = default!;

        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
        
        public ICollection<Car>? Cars { get; set; }
    }
}