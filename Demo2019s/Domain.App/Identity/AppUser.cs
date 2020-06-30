using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(20)]
        [Required]
        public string PhoneNumber { get; set; } = default!;

        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
        
        public ICollection<Car>? Cars { get; set; }
    }
}