using System.Collections.Generic;

namespace Domain
{
    public class Wash
    {
        public int washID { get; set; }
        
        public int checkID { get; set; }
        public ICollection<Check>? Check { get; set; }
        
        public int washTypeID { get; set; }
        public WashType washType { get; set; }
        
        public int orderID { get; set; }
        public Order order { get; set; }
        //nameOfWashType implies to exterior or interior wash
        public string nameOfWashType { get; set; }
    }
}