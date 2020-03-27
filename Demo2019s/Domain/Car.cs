using DAL.Base;

namespace Domain
{
    //1
    public class Car : DomainEntity
    {
        //car
        public int CarId { get; set; }
        
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public int PersonId { get; set; }

        public Person? Person { get; set; }
        
        public string LicenceNr { get; set; }
    }
}