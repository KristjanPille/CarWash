﻿using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>, IPaymentMethodRepositoryCustom
    {
        
    }
}