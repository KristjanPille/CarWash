using System;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Car : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ModelMarkId { get; set; }
        public ModelMark? ModelMark { get; set; }

        /* 1-3
         1=> Small hatchback
         2=> Mid Sized car
         3=> Bigger suv
        */
        public int? CarSize{ get; set; } = default!;
    }
}