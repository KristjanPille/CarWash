namespace Domain
{
    public class Discount
    {
        public int discountID { get; set; }
        
        public int checkID { get; set; }
        public Check Check { get; set; }
        
        public int washID { get; set; }
        public Wash Wash { get; set; }
    }
}