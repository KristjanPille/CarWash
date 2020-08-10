using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;

namespace BLL.App.Services
{
    public class QuizService : BaseEntityService<IAppUnitOfWork, IQuizRepository, IQuizServiceMapper,
            DAL.App.DTO.Quiz, BLL.App.DTO.Quiz>, IQuizService
    {
        public QuizService(IAppUnitOfWork uow) : base(uow, uow.Quizzes,
            new QuizServiceMapper())
        {
        }
    }
}