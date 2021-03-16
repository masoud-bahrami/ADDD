using AgilePM.Core.Dispatcher;

namespace AgilePM.Identity.Domain.Contracts.Contracts
{
    public class CreateNewTenantCommand : ICommand
    {
        public string TenantName { get; private set; }
        public bool TenantIsActive { get; private set; }
        public string Description { get; }

        public CreateNewTenantCommand(string name, bool tenantIsActive, string description)
        {
            TenantName = name;
            TenantIsActive = tenantIsActive;
            Description = description;
        }
    }


}