using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain.App;
using ee.itcollege.carwash.kristjan.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }
        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UOW));
        
        public ISubjectReviewService SubjectReviews =>
            GetService<ISubjectReviewService>(() => new SubjectReviewService(UOW));
    }
}