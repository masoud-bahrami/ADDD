using System.Threading.Tasks;
using AgilePM.Core.Dispatcher;
using AgilePM.Identity.Domain.Contracts;
using AgilePM.Identity.Domain.Contracts.Contracts;

namespace AgilePM.Identity.ApplicationService.Commands.Tenant.Facade
{
    public class TenantFacade
    {
        private readonly ICommandDispatcher _commandDispatcher;
        

        public TenantFacade(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task CreateNewTenant(string tenantName, bool tenantIsActive, string description)
        {
            var createNewTenantCommand = TenantContracts.Commands.CreateNewTenant(tenantName, tenantIsActive, description);
            await _commandDispatcher.Dispatch<CreateNewTenantCommand>(createNewTenantCommand);
            
        }

        public async Task CreateUser(string tenantId, string userName, string password, PersonInformationDto personInformation)
        {
            var command = UserContracts.Commands.CreateUserCommand(tenantId, userName, password, personInformation);
            await _commandDispatcher.Dispatch<RegisterNewUserCommand>(command);
        }
    }
}