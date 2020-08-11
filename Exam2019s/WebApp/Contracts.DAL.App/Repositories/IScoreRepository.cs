using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.carwash.kristjan.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IScoreRepository : IBaseRepository<Score>, IScoreRepositoryCustom
    {
        Task<double> GetAverage(Guid quizId);
        
        Task<IEnumerable<Score>> GetAveragePerPerson(Guid userId);
    }
    
}