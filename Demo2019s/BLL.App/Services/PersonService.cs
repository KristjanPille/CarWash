using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace BLL.App.Services
{
    public class OwnerService : BaseEntityService<IPersonRepository, IAppUnitOfWork, Person, Person>, IPersonRepository
    {
        public OwnerService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new IdentityMapper<Person, Person>(), unitOfWork.Persons)
        {
        }


        public async Task<IEnumerable<Person>> AllAsync(Guid? userId = null) =>
            await ServiceRepository.AllAsync(userId);

        public async Task<Person> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.FirstOrDefaultAsync(id, userId);

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);

        public async Task<IEnumerable<PersonDTO>> DTOAllAsync(Guid? userId = null) =>
            await ServiceRepository.DTOAllAsync(userId);

        public async Task<PersonDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
    }
}