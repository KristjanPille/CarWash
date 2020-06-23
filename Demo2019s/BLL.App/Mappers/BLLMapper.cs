using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;
using Car = Domain.App.Car;
using ModelMark = Domain.App.ModelMark;

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
            
            MapperConfigurationExpression.CreateMap<Car, CarModelMark>();
            MapperConfigurationExpression.CreateMap<ModelMark, CarModelMark>();
            
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}