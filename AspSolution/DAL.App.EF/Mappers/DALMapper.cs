﻿using AutoMapper;
 using ee.itcollege.carwash.kristjan.DAL.Base.Mappers;
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
            
            MapperConfigurationExpression.CreateMap<Domain.App.Payment, DAL.App.DTO.Payment>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Payment, Domain.App.Payment>();

            MapperConfigurationExpression.CreateMap<Domain.App.Check, DAL.App.DTO.Check>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Check, Domain.App.Check>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Order, Domain.App.Order>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}