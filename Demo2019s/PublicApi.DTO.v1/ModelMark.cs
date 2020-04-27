using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ModelMark : ModelMarkEdit
    {
  
    }
    
    public class ModelMarkCreate
    {
        public string Mark { get; set; } = default!;
        public string Model { get; set; } = default!;
    }
    
    public class ModelMarkEdit : ModelMarkCreate
    {
        public Guid Id { get; set; }
    }
    
}