﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;

using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Payment, DAL.App.DTO.Payment>,
        IPaymentRepository
    {
        public PaymentRepository(AppDbContext repoDbContext) : base(repoDbContext,  
            new DALMapper<Domain.App.Payment, DTO.Payment>())
        {
        }

    }
}