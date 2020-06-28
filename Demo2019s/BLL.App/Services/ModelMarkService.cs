using System;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Car = PublicApi.DTO.v1.Car;

namespace BLL.App.Services
{
    public class ModelMarkService :
        BaseEntityService<IAppUnitOfWork, IModelMarkRepository, IModelMarkServiceMapper,
            DAL.App.DTO.ModelMark, BLL.App.DTO.ModelMark>, IModelMarkService
    {
        public ModelMarkService(IAppUnitOfWork uow) : base(uow, uow.ModelMarks,
            new ModelMarkServiceMapper())
        {
        }

        public virtual async Task<Guid> GetModelMarkId(Car car)
        {
            // find modelmark id from car dto model and mark names
            var modelMark = await UOW.ModelMarks.FindModelMarkFromCarDTO(car);

            return modelMark.Id;
        }
    }
}