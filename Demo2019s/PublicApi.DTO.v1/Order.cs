using System;

namespace PublicApi.DTO.v1
{
    public class Order : OrderEdit
    {
  
    }
    
    public class OrderCreate
    {
        public DateTime DateAndTime { get; set; }

        public string Comment { get; set; } = default!;
    }
    
    public class OrderEdit : OrderCreate
    {
        public Guid Id { get; set; }
    }
    
}