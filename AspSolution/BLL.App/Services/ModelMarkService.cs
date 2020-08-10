﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Car = PublicApi.DTO.v1.Car;
using ModelMark = DAL.App.DTO.ModelMark;

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
        private readonly ModelMarkMapper _mapper = new ModelMarkMapper();

        public virtual async Task<Guid> GetModelMarkId(Car car)
        {
            // find modelmark id from car dto model and mark names
            var modelMark = await UOW.ModelMarks.FindModelMarkFromCarDTO(car);

            return modelMark.Id;
        }

        public async Task<IEnumerable<ModelMark>> GetMarkModels(string mark)
        {
            var markModels = await UOW.ModelMarks.FindMarkModels(mark);

            return markModels;
        }

        
        //return BLL to apicontroller
        public async Task<IEnumerable<ModelMark>> GetMarks()
        {
            var markModels = await UOW.ModelMarks.FindMarks();

            return markModels;
        }
    }
}