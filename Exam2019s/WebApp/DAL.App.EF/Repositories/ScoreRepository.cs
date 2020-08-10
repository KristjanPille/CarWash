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
    public class ScoreRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Score, DAL.App.DTO.Score>,
        IScoreRepository
    {
        public ScoreRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Score, DTO.Score>())
        {
        }
        
        public override async Task<IEnumerable<Score>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(g => g.AppUser);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<double> GetAverage(Guid quizId)
        {
            double averageScore = 0;
            var query = PrepareQuery();

            query = query.Where(e => e.QuizId == quizId);
            var domainEntities = await query.ToListAsync();

            List<double> initialList = new List<double>();

            domainEntities.ForEach(e => initialList.Add(e.QuizScore));

            if (initialList.Count > 0)
            {
                averageScore = initialList.Average();
            }

            return averageScore;
        }
    }
}