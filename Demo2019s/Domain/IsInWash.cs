﻿using System;

namespace Domain
{
    public class IsInWash
    {
        public int IsInWashId { get; set; }
        
        
        public int CarId { get; set; }
        public Car? Car { get; set; }
        
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        
        public int WashId { get; set; }
        public Wash? Wash { get; set; }
        
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}