using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using QuestionT = DAL.App.DTO.Question;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionService : IBaseEntityService<Question>, IQuestionRepositoryCustom<Question>
    {
        Task<IEnumerable<QuestionT>> GetQuizQuestions(Guid quizId);
    }
}