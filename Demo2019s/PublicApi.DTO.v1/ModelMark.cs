using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class ModelMark : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Mark { get; set; } = default!;
        
        public string Model { get; set; } = default!;
        
        public int ModelMarkSize{ get; set; } = default!;
    }
}