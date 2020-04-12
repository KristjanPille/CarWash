using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace BLL.App.Services
{
    public class PersonCarService : BaseEntityService<IPersonCarRepository, IAppUnitOfWork, PersonCar, PersonCar>, IPersonCarService
    {
        public PersonCarService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<PersonCar, PersonCar>(), unitOfWork.PersonCars)
        {
        }
        public async Task<IEnumerable<PersonCar>> AllAsync(Guid? userId = null) =>
            await ServiceRepository.AllAsync(userId);

        public async Task<PersonCar> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.FirstOrDefaultAsync(id, userId);

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);

        public async Task<IEnumerable<PersonCarDTO>> DTOAllAsync(Guid? userId = null) =>
            await ServiceRepository.DTOAllAsync(userId);

        public async Task<PersonCarDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
    }
}