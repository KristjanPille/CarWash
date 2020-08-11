using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ScoreV2Mapper : BaseMapper<DAL.App.DTO.Score, Score>
    {
        public ScoreV2Mapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Score, Score>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}