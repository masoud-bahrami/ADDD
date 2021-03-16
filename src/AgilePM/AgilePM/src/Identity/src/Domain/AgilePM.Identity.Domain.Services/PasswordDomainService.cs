using System;
using AgilePM.Identity.Domain.User;
using AgilePM.Identity.Domain.User.DomainService;

namespace AgilePM.Identity.Domain.Services
{
    public class PasswordDomainService
        : IPasswordDomainService
    {
        public void IsWeak(string password)
        {
            //TODO
        }

        public string EncryptedPassword(string password)
        {
            //TODO
            return password;
        }
    }
}
