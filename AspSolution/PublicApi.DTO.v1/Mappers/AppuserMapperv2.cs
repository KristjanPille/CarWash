using AutoMapper;
using PublicApi.DTO.v1.Identity;
using AppUser = Domain.App.Identity.AppUser;

namespace PublicApi.DTO.v1.Mappers
{
    public class AppUserMapperV2 : BaseMapper<AppUser, PublicApi.DTO.v1.Identity.AppUser>
    {
        public AppUserMapperV2()
        {
            MapperConfigurationExpression.CreateMap<AppUser, PublicApi.DTO.v1.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public PublicApi.DTO.v1.Identity.AppUser MapAppuserToDTO(AppUser inObject)
        {
            return Mapper.Map<PublicApi.DTO.v1.Identity.AppUser>(inObject);
        }
    }
}