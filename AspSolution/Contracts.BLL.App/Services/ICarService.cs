﻿using ee.itcollege.carwash.kristjan.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Car = BLL.App.DTO.Car;

namespace Contracts.BLL.App.Services
{
    public interface ICarService : IBaseEntityService<Car>, ICarRepositoryCustom<Car>
    {
    }
}