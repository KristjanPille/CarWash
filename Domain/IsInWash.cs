using System;

namespace Domain
{
    public class IsInWash
    {
        public int isInWashID { get; set; }
        
        
        public int carID { get; set; }
        public Car Car { get; set; }
        
        public int personID { get; set; }
        public Person Person { get; set; }
        
        public int washID { get; set; }
        public Wash Wash { get; set; }
        
        public TimeSpan from { get; set; }
        public TimeSpan to { get; set; }
    }
}