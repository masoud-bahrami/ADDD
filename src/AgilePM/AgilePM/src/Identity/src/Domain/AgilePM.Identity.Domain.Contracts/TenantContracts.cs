using AgilePM.Identity.Domain.Contracts.Contracts;

namespace AgilePM.Identity.Domain.Contracts
{
    public class TenantContracts
    {
        public class Commands
        {
            public static CreateNewTenantCommand CreateNewTenant(string name, bool tenantIsActive, string description)
                => new CreateNewTenantCommand(name, tenantIsActive, description);
        }

        public class  Events
        {
            
        }
    }
}
