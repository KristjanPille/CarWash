﻿using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;

namespace BLL.App.Services
{
    public class QuestionAnswerService : BaseEntityService<IAppUnitOfWork, IQuestionAnswerRepository, IQuestionAnswerServiceMapper,
        DAL.App.DTO.QuestionAnswer, BLL.App.DTO.QuestionAnswer>, IQuestionAnswerService
    {
        public QuestionAnswerService(IAppUnitOfWork uow) : base(uow, uow.QuestionAnswers,
            new QuestionAnswerServiceMapper())
        {
        }
    }
}