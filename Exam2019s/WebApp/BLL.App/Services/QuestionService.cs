using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Question = DAL.App.DTO.Question;

namespace BLL.App.Services
{
    public class QuestionService : BaseEntityService<IAppUnitOfWork, IQuestionRepository, IQuestionServiceMapper,
        DAL.App.DTO.Question, BLL.App.DTO.Question>, IQuestionService
    {
        public QuestionService(IAppUnitOfWork uow) : base(uow, uow.Questions,
            new QuestionServiceMapper())
        {
        }

        public async Task<IEnumerable<Question>> GetQuizQuestions(Guid quizId)
        {
            var questions = await UOW.Questions.FindQuestions(quizId);

            return questions;
        }
    }
}