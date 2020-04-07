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
    public class ModelMarkRepository : EFBaseRepository<ModelMark, AppDbContext>, IModelMarkRepository
    {
        public ModelMarkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<ModelMark>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Mark)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        
        public async Task<ModelMark> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Mark)
                .Where(a => a.Id == id)
                .AsQueryable();

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