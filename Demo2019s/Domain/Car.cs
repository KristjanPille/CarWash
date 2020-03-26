using DAL.Base;

namespace Domain
{
    //1
    public class Car : DomainEntity
    {
        private int _personId;

        //car
        public int CarId { get; set; }
        
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }

        public int PersonId { get; set; }

        public Person? Person { get; set; }
        
        public int LicenceNr { get; set; }
    }
}