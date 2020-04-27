using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class IsInWash : IsInWashEdit
    {
    }
    
    public class IsInWashCreate
    {
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
    
    public class IsInWashEdit : IsInWashCreate
    {
        public Guid Id { get; set; }
    }
    
}