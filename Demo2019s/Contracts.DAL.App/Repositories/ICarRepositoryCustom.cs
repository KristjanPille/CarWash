using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICarRepositoryCustom: ICarRepositoryCustom<Car>
    {
    }

    public interface ICarRepositoryCustom<TCarView>
    {
    }
}