﻿using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class ModelMarkService : BaseEntityService<IModelMarkRepository, IAppUnitOfWork, DAL.App.DTO.ModelMark, ModelMark>, IModelMarkService
    {
        public ModelMarkService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ModelMark, ModelMark>(), unitOfWork.ModelMarks)
        {
        }
    }
}