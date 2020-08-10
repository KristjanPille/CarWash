using ee.itcollege.carwash.kristjan.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionAnswerRepository : IBaseRepository<QuestionAnswer>, IQuestionAnswerRepositoryCustom
    {
        
    }
    
}