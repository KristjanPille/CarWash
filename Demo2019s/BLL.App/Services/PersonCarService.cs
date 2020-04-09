using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;

namespace BLL.App.Services
{
    public class PersonCarService : BaseEntityService<IPersonCarRepository, IAppUnitOfWork, PersonCar, PersonCar>, IPersonCarService
    {
        public PersonCarService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<PersonCar, PersonCar>(), unitOfWork.PersonCars)
        {
        }
    }
}