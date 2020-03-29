using System;

namespace PublicApi.DTO.v1
{
    public class CarDTO
    {
        public int Id { get; set; }
        
        public int CarId { get; set; }
        
        public int CarTypeId { get; set; }

        public int PersonId { get; set; }

        public int LicenceNr { get; set; }
    }
}