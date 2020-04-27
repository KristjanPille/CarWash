using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ModelMark : ModelMark<Guid>, IDomainBaseEntity
    {
    }
    
    public class ModelMark<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
        

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
    
}