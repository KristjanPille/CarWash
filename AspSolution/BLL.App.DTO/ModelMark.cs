using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ModelMark : IDomainEntityId
    { 
        public Guid Id { get; set; }
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
        
        public int ModelMarkSize{ get; set; } = default!;
    }
}