using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using PersonCar = Domain.PersonCar;

namespace BLL.App.Services
{
    public class PersonCarService : BaseEntityService<IPersonCarRepository, IAppUnitOfWork, DAL.App.DTO.PersonCar, PersonCar>, IPersonCarService
    {
        public PersonCarService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PersonCar, PersonCar>(), unitOfWork.PersonCars)
        {
        }
    }
}