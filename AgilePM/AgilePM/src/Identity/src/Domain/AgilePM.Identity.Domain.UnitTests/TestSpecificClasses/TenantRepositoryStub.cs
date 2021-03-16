using System;
using System.Threading.Tasks;
using AgilePM.Identity.Domain.Tenant;
using AgilePM.Identity.Domain.Tenant.Repository;

namespace AgilePM.Identity.Domain.UnitTests.TestSpecificClasses
{

    public class TenantRepositoryStub : ITenantRepository
    {
        //Snowflake strategy

        public TenantId NextId() => new TenantId(Guid.NewGuid().ToString());
        
        public Task AddTenantAsync(Tenant.Tenant tenant) => Task.CompletedTask;
        public Task<Tenant.Tenant> GetTenant(TenantId tenantId)
        {
            throw new NotImplementedException();
        }
    }
}