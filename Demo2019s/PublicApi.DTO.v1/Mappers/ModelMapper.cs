using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ModelMapper : BaseMapper<DAL.App.DTO.ModelMark, ModelDTO>
    { 
        public ModelMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ModelMark, ModelDTO>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}