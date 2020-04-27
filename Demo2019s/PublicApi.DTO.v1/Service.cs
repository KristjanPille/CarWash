using System;

namespace PublicApi.DTO.v1
{
    public class Service : ServiceEdit
    {
  
    }
    
    public class ServiceCreate
    {
        public string NameOfService { get; set; } = default!;
    }
    
    public class ServiceEdit : ServiceCreate
    {
        public Guid Id { get; set; }
    }
    
}