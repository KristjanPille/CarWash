using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Discount : DiscountEdit
    {
     
    }
    
    public class DiscountCreate
    {
        public int DiscountAmount { get; set; } = default!;
    }
    
    public class DiscountEdit : DiscountCreate
    {
        public Guid Id { get; set; }
    }
    
}