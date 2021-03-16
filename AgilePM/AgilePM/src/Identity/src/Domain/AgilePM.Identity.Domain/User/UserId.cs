using System.Collections.Generic;
using AgilePM.Core.Domain;
using AgilePM.Identity.Domain.Tenant;

namespace AgilePM.Identity.Domain.User
{
    public class UserId : Identity<UserId>
    {
        public UserId(TenantId tenantId, string userName)
        {
            TenantId = tenantId;
            UserName = userName;
        }

        public TenantId TenantId { get; }
        public string UserName { get; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return TenantId.Id;
            yield return UserName;
        }
    }
}