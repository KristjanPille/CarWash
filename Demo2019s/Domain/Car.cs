namespace Domain
{
    //1
    public class Car
    {
        private int _personId;

        //car
        public int CarId { get; set; }
        
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public int PersonId
        {
            get => _personId;
            set => _personId = value;
        }

        public Person? Person { get; set; }
        
        public int LicenceNr { get; set; }
    }
}