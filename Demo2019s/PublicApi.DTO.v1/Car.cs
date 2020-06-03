using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;
using AppUser = Domain.App.Identity.AppUser;

namespace PublicApi.DTO.v1
{
    public class Car : IDomainEntityId
    { 
       public Guid Id { get; set; }
       
       public Guid ModelMarkId { get; set; }
       
       public int? CarSize{ get; set; } = default!;
       
       public Guid AppUserId { get; set; }
    }
}