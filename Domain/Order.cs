using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        [Key]
        public int oderID { get; set; }
        public DateTime dateAndTime { get; set; }
        
        public int washID { get; set; }
        public ICollection<Wash>? Wash { get; set; }
        
        public string comment { get; set; }
    }
}