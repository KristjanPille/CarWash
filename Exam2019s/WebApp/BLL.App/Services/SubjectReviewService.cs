﻿using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;

namespace BLL.App.Services
{
    public class SubjectReviewService : BaseEntityService<IAppUnitOfWork, ISubjectReviewRepository, ISubjectReviewServiceMapper,
        DAL.App.DTO.SubjectReview, BLL.App.DTO.SubjectReview>, ISubjectReviewService
    {
        public SubjectReviewService(IAppUnitOfWork uow) : base(uow, uow.SubjectReviews,
            new SubjectReviewServiceMapper())
        {
        }
    }
}