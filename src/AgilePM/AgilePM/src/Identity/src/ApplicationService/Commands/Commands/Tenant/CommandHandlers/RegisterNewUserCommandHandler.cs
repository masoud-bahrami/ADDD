using System.Threading.Tasks;
using AgilePM.Core.Dispatcher;
using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Tenant;
using AgilePM.Identity.Domain.Tenant.Repository;
using AgilePM.Identity.Domain.User;
using AgilePM.Identity.Domain.User.DomainService;

namespace AgilePM.Identity.ApplicationService.Commands.Tenant.CommandHandlers
{
    public class RegisterNewUserCommandHandler : IWantToHandleThisCommand<RegisterNewUserCommand>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IPasswordDomainService _passwordDomainService;
        private readonly IUserRepository _userRepository;

        public RegisterNewUserCommandHandler(ITenantRepository tenantRepository, IPasswordDomainService passwordDomainService, IUserRepository userRepository)
        {
            _tenantRepository = tenantRepository;
            _passwordDomainService = passwordDomainService;
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterNewUserCommand command)
        {
            var tenant = await _tenantRepository.GetTenant(new TenantId(command.TenantId));
            var newUser = tenant.RegisterNewUser(command, _passwordDomainService);
            
            await _userRepository.Add(newUser);
        }
    }
}
