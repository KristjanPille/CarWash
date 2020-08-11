using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;

namespace BLL.App.Services
{
    public class ScoreService : BaseEntityService<IAppUnitOfWork, IScoreRepository, IScoreServiceMapper,
        DAL.App.DTO.Score, BLL.App.DTO.Score>, IScoreService
    {
        public ScoreService(IAppUnitOfWork uow) : base(uow, uow.Scores,
            new ScoreServiceMapperMapper())
        {
        }

        public async Task<double> GetAverageScore(Guid quizId)
        {
            var score = await UOW.Scores.GetAverage(quizId);

            return score;
        }

        public async Task<IEnumerable<Score>> GetAverageScorePerUser(Guid userId)
        {
            var scorePerson = await UOW.Scores.GetAveragePerPerson(userId);

            return scorePerson;
        }
    }
}