using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Car = BLL.App.DTO.Car;

namespace Contracts.BLL.App.Services
{
    public interface ICarService : IBaseEntityService<Car>, ICarRepositoryCustom<Car>
    {
    }
}