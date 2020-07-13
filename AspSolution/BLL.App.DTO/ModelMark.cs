using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.carwash.kristjan.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ModelMark : IDomainEntityId
    { 
        public Guid Id { get; set; }
        [Display(Name = nameof(Mark), ResourceType = typeof(Resources.BLL.App.DTO.ModelMark))]
        public string Mark { get; set; } = default!;
        
        [Display(Name = nameof(Model), ResourceType = typeof(Resources.BLL.App.DTO.ModelMark))]
        public string Model { get; set; } = default!;
        
        public int ModelMarkSize{ get; set; } = default!;
    }
}