﻿using AutoMapper;
using DAL.Base.Mappers;
 using PublicApi.DTO.v1;

 namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Domain.App.Campaign, DAL.App.DTO.Campaign>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.ModelMark, DAL.App.DTO.ModelMark>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ModelMark, Domain.App.ModelMark>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.IsInService, DAL.App.DTO.IsInService>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.IsInService, Domain.App.IsInService>();
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ModelMark, MarkDTO>();
            MapperConfigurationExpression.CreateMap<MarkDTO, BLL.App.DTO.ModelMark>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}