using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using QuestionAnswerT = DAL.App.DTO.QuestionAnswer;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionAnswerService : IBaseEntityService<QuestionAnswer>, IQuestionAnswerRepositoryCustom<QuestionAnswer>
    {
        Task<IEnumerable<QuestionAnswerT>> GetQuestionAnswers(Guid questionId);
    }
}