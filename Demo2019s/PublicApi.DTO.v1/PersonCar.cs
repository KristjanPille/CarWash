using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace PublicApi.DTO.v1
{
    // for display only
    public class PersonCar : PersonCarEdit
    {
        public ICollection<Person> Persons { get; set; } = default!;
        public ICollection<Car> Cars { get; set; } = default!;
    }

    // for display only
    public class PersonCarDetail : PersonCarEdit
    {
        public Person Person { get; set; } = default!;
        public Car Car { get; set; } = default!;
    }

    // from client to server
    public class PersonCarEdit: PersonCarCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class PersonCarCreate
    {
        public Guid PersonId { get; set; }
        public Guid CarId { get; set; }
    }
}