using System.Threading.Tasks;
using AgilePM.Core;
using AgilePM.Core.Dispatcher;
using AgilePM.Identity.DataBase.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AgilePM.Identity.ApplicationService.Query.Tenant.QueryHandlers
{
    public class GetTenantQueryHandler
        : IWantToHandleThisQuery<GetTenantByNameQuery, TenantViewModel>
    {
        private readonly IdentityDbContext _dbContext;

        public GetTenantQueryHandler(IdentityDbContext dbContext) => _dbContext = dbContext;

        public async Task<TenantViewModel> RunQuery(GetTenantByNameQuery query) => ToViewModel(await GetTenant(query.TenantName));

        private TenantViewModel ToViewModel(Domain.Tenant.Tenant tenant) =>
            new TenantViewModel
            {
                Id = tenant.TenantId.Id,
                Name = tenant.Name,
                IsActive = tenant.IsActive,
                Description = tenant.Description
            };

        private async Task<Domain.Tenant.Tenant> GetTenant(string name)
        {
            var tenant =await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Name == name);
            Check.That().ArgumentNotEmpty("Tenant" , tenant , "Tenant is null or empty");

            return tenant;
        }
    }
}