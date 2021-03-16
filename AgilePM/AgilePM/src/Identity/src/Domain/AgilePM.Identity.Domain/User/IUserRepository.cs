using System.Threading.Tasks;

namespace AgilePM.Identity.Domain.User
{
    public interface IUserRepository
    {
        Task Add(User newUser);
    }
}