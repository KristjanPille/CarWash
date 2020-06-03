using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class CarService :
        BaseEntityService<IAppUnitOfWork, ICarRepository, ICarServiceMapper,
            DAL.App.DTO.Car, BLL.App.DTO.Car>, ICarService
    {
        public CarService(IAppUnitOfWork uow) : base(uow, uow.Cars,
            new CarServiceMapper())
        {
        }
    }
}