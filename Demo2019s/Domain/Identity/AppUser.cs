using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser: IdentityUser
    {
        [MaxLength(64)] public override string Id { get; set; } = default!;

        public ICollection<Person>? Persons { get; set; }
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

    }
}