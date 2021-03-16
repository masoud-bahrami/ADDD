using System.Threading.Tasks;
using AgilePM.Core;
using AgilePM.Core.Dispatcher;
using AgilePM.Identity.DataBase.DbContext;
using AgilePM.Identity.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace AgilePM.Identity.ApplicationService.Query.User.QueryHandlers
{
    public class GetUserQueryHandler
        : IWantToHandleThisQuery<GetUserQuery, UserViewModel>
    {
        private readonly IdentityDbContext _dbContext;

        public GetUserQueryHandler(IdentityDbContext dbContext) => _dbContext = dbContext;

        public async Task<UserViewModel> RunQuery(GetUserQuery query) => ToViewModel(await GetTenant(query));

        private UserViewModel ToViewModel(UserMemento userMemento) =>
            new UserViewModel
            {
                UserName = userMemento.UserName,
                //TenantId= userMemento.TenantId,
                Active = userMemento.IsActive
            };

        private async Task<UserMemento> GetTenant(GetUserQuery query)
        {
            var tenant =await _dbContext.Users.FirstOrDefaultAsync(t => t.TenantId== query.TenantId && t.UserName == query.UserName);
            Check.That().ArgumentNotEmpty("Tenant" , tenant , "Tenant is null or empty");

            return tenant;
        }
    }
}