using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;

namespace BLL.App.Services
{
    public class QuestionService : BaseEntityService<IAppUnitOfWork, IQuestionRepository, IQuestionServiceMapper,
        DAL.App.DTO.Question, BLL.App.DTO.Question>, IQuestionService
    {
        public QuestionService(IAppUnitOfWork uow) : base(uow, uow.Questions,
            new QuestionServiceMapper())
        {
        }
    }
}