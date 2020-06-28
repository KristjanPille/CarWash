using AutoMapper;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Campaign, DAL.App.DTO.Campaign>();
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Car, DAL.App.DTO.Car>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Car, BLL.App.DTO.Car>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ModelMark, BLL.App.DTO.ModelMark>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ModelMark, DAL.App.DTO.ModelMark>();


            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();


            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}