﻿using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CarTypeService : BaseEntityService<ICarTypeRepository, IAppUnitOfWork, DAL.App.DTO.CarType, CarType>, ICarTypeService
    {
        public CarTypeService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.CarType, CarType>(), unitOfWork.CarTypes)
        {
        }
    }
}