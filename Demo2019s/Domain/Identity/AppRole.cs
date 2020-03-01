using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppRole: IdentityRole
    {
        [MaxLength(64)] public override string Id { get; set; } = default!;
    }
}