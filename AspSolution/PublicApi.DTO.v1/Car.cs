using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.carwash.kristjan.Contracts.Domain;
using ee.itcollege.carwash.kristjan.Domain.Base;
using AppUser = Domain.App.Identity.AppUser;

namespace PublicApi.DTO.v1
{
    public class Car : IDomainEntityId
    { 
       public Guid Id { get; set; }

       //Create ModelMark from these in bll layer
       public string Mark { get; set; } = default!;
       public string Model { get; set; } = default!;
       
       public Guid AppUserId { get; set; }
    }
}