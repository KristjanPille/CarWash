﻿using System;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1;
using ModelMark = BLL.App.DTO.ModelMark;

namespace Contracts.BLL.App.Services
{
    public interface IModelMarkService : IBaseEntityService<ModelMark>, IModelMarkRepositoryCustom<ModelMark>
    {
        Task<Guid> GetModelMarkId(Car gpsLocation);
    }
}