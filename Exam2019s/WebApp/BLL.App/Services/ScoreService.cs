using System;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
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
            var markModels = await UOW.Scores.GetAverage(quizId);

            return markModels;
        }
    }
}