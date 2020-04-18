using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CarType : CarTypeEdit
    {
      [MinLength(1)] [MaxLength(64)] 
      public string Name { get; set; } = default!;
    }
    
    public class CarTypeCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string Name { get; set; } = default!;
    }
    
    public class CarTypeEdit : CarTypeCreate
    {
        public Guid Id { get; set; }
    }
    
}