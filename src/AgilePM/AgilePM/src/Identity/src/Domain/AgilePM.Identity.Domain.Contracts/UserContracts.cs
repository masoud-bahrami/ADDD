using AgilePM.Identity.Domain.Contracts.Contracts;
using AgilePM.Identity.Domain.Contracts.Events;

namespace AgilePM.Identity.Domain.Contracts
{
    public class UserContracts
    {
        public class Commands
        {
            public static RegisterNewUserCommand CreateUserCommand(string tenantId, string userName, string password,
                PersonInformationDto personInformation)
                => new RegisterNewUserCommand(tenantId, userName, password, personInformation);
        }

        public class Events
        {
            public static NewUserRegisteredEvent NewUserRegisteredEvent()
                => new NewUserRegisteredEvent();
        }
    }
}
