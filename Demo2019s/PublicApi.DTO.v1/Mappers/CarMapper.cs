using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CarMapper : BaseMapper<BLL.App.DTO.Car, Car>
    {
        public CarMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Car, Car>().ForMember(destination => destination.Mark, 
                options => options.MapFrom(source => source.ModelMark!.Mark)).
                ForMember(destination => destination.Model, 
                options => options.MapFrom(source => source.ModelMark!.Model));;
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}