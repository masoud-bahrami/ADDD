using AgilePM.Core.Dispatcher;

namespace AgilePM.Identity.ApplicationService.Query.Tenant
{
    public class GetTenantByNameQuery : IQuery
    {
        public string TenantName { get; }

        public GetTenantByNameQuery(string tenantName)
        {
            TenantName = tenantName;
        }
    }
}