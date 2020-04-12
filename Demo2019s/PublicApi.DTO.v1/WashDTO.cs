using System;

namespace PublicApi.DTO.v1
{
    public class WashDTO
    {
        public Guid Id { get; set; }
        
        public int WashId { get; set; }
        
        public string NameOfWashType { get; set; } = default!;
    }
}