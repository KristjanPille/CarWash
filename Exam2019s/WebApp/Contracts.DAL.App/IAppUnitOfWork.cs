using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        ICampaignRepository Campaigns { get; }
        ISubjectReviewRepository SubjectReviews { get; }
    }
}