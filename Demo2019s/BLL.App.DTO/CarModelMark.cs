﻿using System;

namespace BLL.App.DTO
{
    public class CarModelMark
    {
        public Guid Id { get; set; }
        
        public Guid CarId { get; set; }
        
        public Guid ModelMarkId { get; set; }
        
        public int CarSize { get; set; } = default!;
        
        public string Mark { get; set; } = default!;
        
        public string Model { get; set; } = default!;
    }
}