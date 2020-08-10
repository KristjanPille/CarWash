using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuestionAnswersRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.QuestionAnswer, DAL.App.DTO.QuestionAnswer>,
        IQuestionAnswerRepository
    {
        public QuestionAnswersRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.QuestionAnswer, DTO.QuestionAnswer>())
        {
        }
        
    }
}