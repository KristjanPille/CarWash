namespace Domain
{
    public class WashType
    {
        public int WashTypeId { get; set; }
        
        public int WashId { get; set; }

        public string NameOfWash { get; set; } = default!;
    }
}