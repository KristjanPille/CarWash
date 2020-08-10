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
    public class QuestionRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Question, DAL.App.DTO.Question>,
        IQuestionRepository
    {
        public QuestionRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Question, DTO.Question>())
        {
        }

        public async Task<IEnumerable<Question>> FindQuestions(Guid quizId)
        {
            var query = PrepareQuery();

            query = query.Where(e => e.QuizId == quizId);
            var domainEntities = await query.ToListAsync();

            var result = domainEntities.Select(e => Mapper.Map(e));
            
            return result;
        }
    }
}