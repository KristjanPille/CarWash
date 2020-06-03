using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using ModelMark = BLL.App.DTO.ModelMark;

namespace Contracts.BLL.App.Services
{
    public interface IModelMarkService : IBaseEntityService<ModelMark>, IModelMarkRepositoryCustom<ModelMark>
    {
    }
}