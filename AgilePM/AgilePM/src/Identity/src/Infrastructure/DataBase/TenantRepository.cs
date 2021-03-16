using System;
using System.Threading.Tasks;
using AgilePM.Identity.DataBase.DbContext;
using AgilePM.Identity.Domain.Tenant;
using AgilePM.Identity.Domain.Tenant.Repository;
using Microsoft.EntityFrameworkCore;

namespace AgilePM.Identity.DataBase
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IdentityDbContext _dbContext;

        public TenantRepository(IdentityDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddTenantAsync(Tenant tenant)
            => await _dbContext.Tenants.AddAsync(tenant);

        public async Task<Tenant> GetTenant(TenantId tenantId)
        {
            var tenant= await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId.Id);

            return tenant;
        }

        public TenantId NextId()
            => new TenantId(Guid.NewGuid().ToString());
    }
}
