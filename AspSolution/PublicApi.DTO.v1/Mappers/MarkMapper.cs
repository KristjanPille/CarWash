using AutoMapper;
using AutoMapper.Configuration;

namespace PublicApi.DTO.v1.Mappers
{
    public class MarkMapper : BaseMapper<DAL.App.DTO.ModelMark, MarkDTO>
    {
        public MarkMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ModelMark, MarkDTO>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}