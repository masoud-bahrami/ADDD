using System.Threading.Tasks;
using AgilePM.Core.Dispatcher;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Tenant.Repository;

namespace AgilePM.Identity.ApplicationService.Commands.Tenant.CommandHandlers
{
    public class CreateNewTenantCommandHandler : IWantToHandleThisCommand<CreateNewTenantCommand>
    {
        // Driven
        private readonly ITenantRepository _repository;

        public CreateNewTenantCommandHandler(ITenantRepository repository) => _repository = repository;

        public async Task Handle(CreateNewTenantCommand command)
        {
            var tenantId = _repository.NextId();

            var tenant = new Domain.Tenant.Tenant(tenantId, command);

            await _repository.AddTenantAsync(tenant);
        }
    }
}