using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
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

        public async Task<IEnumerable<QuestionAnswer>> FindQuestionAnswers(Guid questionId)
        {
            var query = PrepareQuery();

            query = query.Where(e => e.QuestionId == questionId);
            var domainEntities = await query.ToListAsync();

            var result = domainEntities.Select(e => Mapper.Map(e));
            
            return result;
        }
    }
}