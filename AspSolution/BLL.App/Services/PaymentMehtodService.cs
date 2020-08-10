﻿using BLL.App.Mappers;
using ee.itcollege.carwash.kristjan.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PaymentMethodService :
        BaseEntityService<IAppUnitOfWork, IPaymentMethodRepository, IPaymentMethodServiceMapper,
            DAL.App.DTO.PaymentMethod, BLL.App.DTO.PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IAppUnitOfWork uow) : base(uow, uow.PaymentMethods,
            new PaymentMethodServiceMapper())
        {
        }
    }
}