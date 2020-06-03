﻿using System;
using System.Collections.Generic;
 using Contracts.DAL.App;
 using Contracts.DAL.App.Repositories;
 using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public ILangStrRepository LangStrs =>
            GetRepository<ILangStrRepository>(() => new LangStrRepository(UOWDbContext));

        public ILangStrTranslationRepository LangStrTranslations =>
            GetRepository<ILangStrTranslationRepository>(() => new LangStrTranslationRepository(UOWDbContext));
        public ICampaignRepository Campaigns =>
            GetRepository<ICampaignRepository>(() => new CampaignRepository(UOWDbContext));
        public ICarRepository Cars => GetRepository<ICarRepository>(() => new CarRepository(UOWDbContext));
        public ICheckRepository Checks => GetRepository<ICheckRepository>(() => new CheckRepository(UOWDbContext));
        public IIsInServiceRepository IsInServices =>
            GetRepository<IIsInServiceRepository>(() => new IsInServiceRepository(UOWDbContext));
        public IModelMarkRepository ModelMarks =>
            GetRepository<IModelMarkRepository>(() => new ModelMarkRepository(UOWDbContext));
        public IOrderRepository Orders =>
            GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));
        public IPaymentRepository Payments =>
            GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));
        public IPaymentMethodRepository PaymentMethods =>
            GetRepository<IPaymentMethodRepository>(() => new PaymentMethodRepository(UOWDbContext));
        public IServiceRepository Services =>
            GetRepository<IServiceRepository>(() => new ServiceRepository(UOWDbContext));
    }
}