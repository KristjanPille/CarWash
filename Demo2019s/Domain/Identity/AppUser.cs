using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : AppUser<Guid>
    {
        
    }
    
    public class AppUser<TKey>: IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        //public ICollection<Person>? Persons { get; set; }
        [MaxLength(128)] [MinLength(1)] public virtual string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public virtual string LastName { get; set; } = default!;

    }
}