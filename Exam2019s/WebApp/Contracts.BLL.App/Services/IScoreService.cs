using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using ScoreT = DAL.App.DTO.Score;

namespace Contracts.BLL.App.Services
{
    public interface IScoreService : IBaseEntityService<Score>, IScoreRepositoryCustom<Score>
    {
        
        Task<double> GetAverageScore(Guid quizId);
    }
}