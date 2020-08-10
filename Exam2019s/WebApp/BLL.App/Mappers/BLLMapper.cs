using AutoMapper;
using AutoMapper.Configuration;
using ee.itcollege.carwash.kristjan.BLL.Base.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.Mappers;
using PublicApi.DTO.v1;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : PublicApi.DTO.v1.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper()
        { 
            // add more mappings
            //MapperConfigurationExpression.CreateMap<DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>();
            //MapperConfigurationExpression.CreateMap<BLL.App.DTO.Campaign, DAL.App.DTO.Campaign>();

            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();


            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}