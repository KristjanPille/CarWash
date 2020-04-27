﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PaymentMethodService : BaseEntityService<IPaymentMethodRepository, IAppUnitOfWork, DAL.App.DTO.PaymentMethod, BLL.App.DTO.PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PaymentMethod, BLL.App.DTO.PaymentMethod>(), unitOfWork.PaymentMethods)
        {
        }
        public async Task<IEnumerable<BLL.App.DTO.PaymentMethod>> AllAsync(Guid? userId = null) =>
            (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.PaymentMethod> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);
    }
}