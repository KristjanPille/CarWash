using System;
using ee.itcollege.carwash.kristjan.Contracts.Domain;


namespace PublicApi.DTO.v1
{
    public class Car : IDomainEntityId
    { 
       public Guid Id { get; set; }

       //Create ModelMark from these in bll layer
       public string Mark { get; set; } = default!;
       public string Model { get; set; } = default!;
       
       public Guid ModelMarkId { get; set; } = default!;
       public Guid AppUserId { get; set; }
    }
}