using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;


namespace BLL.App.Services
{
    public class CarService : BaseEntityService<ICarRepository, IAppUnitOfWork, DAL.App.DTO.Car, Car>, ICarService
    {
        public CarService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Car, Car>(), unitOfWork.Cars)
        {
        }
    }
}