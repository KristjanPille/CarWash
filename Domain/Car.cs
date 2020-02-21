namespace Domain
{
    public class Car
    {
        //car
        public int carID { get; set; }
        
        public int carTypeID { get; set; }
        public CarType CarType { get; set; }
        
        public int personID { get; set; }
        public Person Person { get; set; }
        
        public int licenceNr { get; set; }
    }
}