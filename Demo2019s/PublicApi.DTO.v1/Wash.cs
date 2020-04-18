using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Wash : WashEdit
    {
        [MinLength(1)] [MaxLength(64)] 
        public string NameOfWashType { get; set; } = default!;
    }
    
    public class WashCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string NameOfWashType { get; set; } = default!;
    }
    
    public class WashEdit : WashCreate
    {
        public Guid Id { get; set; }
    }
}