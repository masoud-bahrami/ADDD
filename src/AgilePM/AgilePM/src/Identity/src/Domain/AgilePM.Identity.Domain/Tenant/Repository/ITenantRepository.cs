using System.Threading.Tasks;

namespace AgilePM.Identity.Domain.Tenant.Repository
{
    public interface ITenantRepository
    {
        TenantId NextId();
        Task AddTenantAsync(Tenant tenant);
        Task<Tenant> GetTenant(TenantId tenantId);
    }
}