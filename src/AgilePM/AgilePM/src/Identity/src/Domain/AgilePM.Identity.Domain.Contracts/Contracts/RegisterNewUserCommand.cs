using AgilePM.Core.Dispatcher;

namespace AgilePM.Identity.Domain.Contracts.Contracts
{
    public class RegisterNewUserCommand : ICommand
    {
        public readonly PersonInformationDto PersonInformation;
        public string TenantId { get; }
        public string UserName { get; }
        public string Password { get; }

        public RegisterNewUserCommand(string tenantId, string userName, string password, PersonInformationDto personInformation)
        {
            PersonInformation = personInformation;
            TenantId = tenantId;
            UserName = userName;
            Password = password;
        }
    }
}