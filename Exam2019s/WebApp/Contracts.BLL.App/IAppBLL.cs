﻿using Contracts.BLL.App.Services;
 using Contracts.DAL.App.Repositories;
 using ee.itcollege.carwash.kristjan.Contracts.BLL.Base;

 namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICampaignService Campaigns { get; }
        ISubjectReviewService SubjectReviews { get; }
    }
}