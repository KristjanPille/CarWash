using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.carwash.kristjan.DAL.Base.EF.Repositories;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Repositories
{
    public class LangStrRepository :
        EFBaseRepository<AppDbContext, DomainApp.Identity.AppUser, DomainApp.LangStr, DALAppDTO.LangStr>,
        ILangStrRepository
    {
        public LangStrRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<DomainApp.LangStr, DALAppDTO.LangStr>())
        {
        }
    }
}