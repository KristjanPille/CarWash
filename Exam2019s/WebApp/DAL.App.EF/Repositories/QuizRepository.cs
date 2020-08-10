﻿using System;
 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;

 using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuizRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Quiz, DAL.App.DTO.Quiz>,
        IQuizRepository
    {
        public QuizRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Quiz, DTO.Quiz>())
        {
        }
        
    }
}