using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using SubjectReview = BLL.App.DTO.SubjectReview;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionService : IBaseEntityService<Question>, IQuestionRepositoryCustom<Question>
    {
    }
}