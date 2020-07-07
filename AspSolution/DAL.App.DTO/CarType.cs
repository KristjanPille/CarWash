using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class CarType: IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] public string Name { get; set; } = default!;
        public ICollection<Car>? Cars{ get; set; }
    }
}