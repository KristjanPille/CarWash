﻿using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        private Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();
        public ICampaignRepository Campaigns => GetRepository<ICampaignRepository>(() => new CampaignRepository(UOWDbContext));
        public ICarRepository Cars => GetRepository<ICarRepository>(() => new CarRepository(UOWDbContext));
        public ICarTypeRepository CarTypes => GetRepository<ICarTypeRepository>(() => new CarTypeRepository(UOWDbContext));
        public ICheckRepository Checks => GetRepository<ICheckRepository>(() => new CheckRepository(UOWDbContext));
        public IDiscountRepository Discounts => GetRepository<IDiscountRepository>(() => new DiscountRepository(UOWDbContext));
        public IIsInWashRepository IsInWashes => GetRepository<IIsInWashRepository>(() => new IsInWashRepository(UOWDbContext));
        public IModelMarkRepository ModelMarks => GetRepository<IModelMarkRepository>(() => new ModelMarkRepository(UOWDbContext));
        public IOrderRepository Orders => GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));
        public IPaymentRepository Payments => GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));
        public IPaymentMethodRepository PaymentMethods => GetRepository<IPaymentMethodRepository>(() => new PaymentMethodRepository(UOWDbContext));
        public IPersonRepository Persons => GetRepository<IPersonRepository>(() => new PersonRepository(UOWDbContext));
        public IPersonTypeRepository PersonTypes => GetRepository<IPersonTypeRepository>(() => new PersonTypeRepository(UOWDbContext));
        public IServiceRepository Services => GetRepository<IServiceRepository>(() => new ServiceRepository(UOWDbContext));
        public IWashRepository Washes => GetRepository<IWashRepository>(() => new WashRepository(UOWDbContext));
        public IWashTypeRepository WashTypes => GetRepository<IWashTypeRepository>(() => new WashTypeRepository(UOWDbContext));


        private TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            repo = repoCreationMethod();
            _repoCache.Add(typeof(TRepository), repo);
            return (TRepository)repo;
        }
    }
}