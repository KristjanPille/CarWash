using System;
using carwash.kristjan.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ModelMark: IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Mark { get; set; } = default!;
        
        public string Model { get; set; } = default!;
        
        public int ModelMarkSize{ get; set; } = default!;
    }
}