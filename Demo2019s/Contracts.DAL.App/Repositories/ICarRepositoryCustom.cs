using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ModelMark = BLL.App.DTO.ModelMark;

namespace Contracts.DAL.App.Repositories
{
    public interface ICarRepositoryCustom: ICarRepositoryCustom<Car>
    {
    }

    public interface ICarRepositoryCustom<TCarView>
    {

    }
}