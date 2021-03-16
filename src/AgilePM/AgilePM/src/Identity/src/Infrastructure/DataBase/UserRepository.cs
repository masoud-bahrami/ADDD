using System.Threading.Tasks;
using AgilePM.Identity.DataBase.DbContext;
using AgilePM.Identity.Domain.User;

namespace AgilePM.Identity.DataBase
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public UserRepository(IdentityDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user.TakeSnapshot());
        }
    }
}
