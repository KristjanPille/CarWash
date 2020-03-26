﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Order : DomainEntity
    {
        [Key]
        public int OderId { get; set; }
        public DateTime DateAndTime { get; set; }
        
        public int WashId { get; set; }
        public ICollection<Wash>? Wash { get; set; }
        
        public string Comment { get; set; } = default!;
    }
}