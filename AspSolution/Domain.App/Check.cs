﻿using System;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using ee.itcollege.carwash.kristjan.Domain.Base;

namespace Domain.App
{
    public class Check : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }
        
        public Guid CarId { get; set; }
        [JsonIgnore]
        public Car? Car { get; set; }
        
        public DateTime DateTimeCheck { get; set; }
        
        public double PaymentAmount { get; set; }
    }
}