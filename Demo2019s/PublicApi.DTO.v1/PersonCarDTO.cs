using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonCarDTO
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }
        public PersonDTO Person { get; set; } = default!;
        
        public Guid CarId { get; set; }
        public CarDTO Car { get; set; } = default!;

    }
}