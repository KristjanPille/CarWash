﻿using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionAnswerService : IBaseEntityService<QuestionAnswer>, IQuestionAnswerRepositoryCustom<QuestionAnswer>
    {
    }
}