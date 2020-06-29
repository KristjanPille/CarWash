using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ModelMarkMapper : BaseMapper<BLL.App.DTO.ModelMark, ModelMark>
    {
        public ModelMarkMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ModelMark, ModelMark>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ModelDTO MapDALToDTO(DAL.App.DTO.ModelMark inObject)
        {
            return Mapper.Map<ModelDTO>(inObject);
        }
    }
}