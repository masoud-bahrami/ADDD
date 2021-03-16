using AgilePM.Core.Dispatcher;

namespace AgilePM.Identity.ApplicationService.Query.User
{
    public class GetUserQuery : IQuery
    {
        public string TenantId { get; }
        public string UserName { get; }

        public GetUserQuery(string tenantId, string userName)
        {
            TenantId = tenantId;
            UserName = userName;
        }
    }

    public class UserViewModel
    {
        public bool Active { get; set; }
        public string UserName { get; set; }
    }
}