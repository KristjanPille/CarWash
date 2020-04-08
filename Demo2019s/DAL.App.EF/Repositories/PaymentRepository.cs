﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : EFBaseRepository<Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Payment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Person)
                .Include(a => a.PaymentMethod)
                .Include(a => a.Check)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.Person!.AppUserId == userId && o.Person!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<Payment> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Person)
                .Include(a => a.PaymentMethod)
                .Include(a => a.Check)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.Person!.AppUserId == userId && o.Person!.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var check = await FirstOrDefaultAsync(id, userId);
            base.Remove(check);
        }
    }
}