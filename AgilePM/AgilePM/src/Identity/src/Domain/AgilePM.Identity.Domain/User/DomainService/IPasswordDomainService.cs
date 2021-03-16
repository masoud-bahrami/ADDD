namespace AgilePM.Identity.Domain.User.DomainService
{
    public interface IPasswordDomainService
    {
        void IsWeak(string password);
        string EncryptedPassword(string password);
    }
}