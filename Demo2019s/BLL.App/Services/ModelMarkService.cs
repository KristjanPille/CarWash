using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

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
    }
}