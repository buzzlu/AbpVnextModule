﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    public class EfCoreOrganizationUnitUserRepository : EfCoreRepository<IOrganizationUnitDbContext, OrganizationUnitUser, Guid>, IOrganizationUnitUserRepository
    {
        public EfCoreOrganizationUnitUserRepository(IDbContextProvider<IOrganizationUnitDbContext> dbContextProvider)
              : base(dbContextProvider)
        {
        }

        public async Task<OrganizationUnitUser> FindByOrganizationUnitIdAndUserIdAsync(Guid organizationUnitId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .FirstOrDefaultAsync(t => t.OrganizationUnitId == organizationUnitId && t.UserId == userId);
        }


        public async Task<List<OrganizationUnitUser>> FindOrganizationUnitUsersAsync(Guid? organizationUnitId, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(t => t.OrganizationUnitId == organizationUnitId)
                .PageBy(skipCount,maxResultCount)
               .ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(Guid organizationUnitId, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(t => t.OrganizationUnitId == organizationUnitId)
               .CountAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }
    }
}
