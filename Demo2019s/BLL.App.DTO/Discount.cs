using System;
using BLL.App.DTO.Identity;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Discount : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }

        public int DiscountAmount { get; set; } = default!;
    }
}